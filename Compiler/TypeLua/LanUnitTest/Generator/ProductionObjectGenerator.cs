// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>03/02/2018</date>
// ----------------------------------------------------------------------------
namespace LanUnitTest.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProductionObjectGenerator
    {
        private static Dictionary<string, Symbol> BasisProduction = new Dictionary<string, Symbol>();
        private static List<Reduction> Reductions = new List<Reduction>();
        private static Dictionary<string, string> SymbolNames = new Dictionary<string, string>();

        private static string BasisClass = @"
namespace LanUnitTest.Production
{
    using LanUnitTest.Project;
    
    // {1}
    public class {0} : Production
    {
         
    }
}
        ";

        private static string ReductionClass = @"
namespace LanUnitTest.Production
{
    using LanUnitTest.Project;
    
    // {5}
    public class {0} : {1}
    {
{2}

        public {0}(Project project, Class @class{3})
        {
{4}
        }
    }
}
        ";

        [TestMethod]
        public void TestGenerate()
        {
            Generate("E:/Develop/TypeLua/Compiler/TestLan/TestLan/LanUnitTest/TestLan.cs", "E:/Develop/TypeLua/Compiler/TestLan/TestLan/LanUnitTest/GProduction");
        }

        public static void Generate(string syntaxFilePath,string productionClassRootPath)
        {
            BasisProduction.Clear();
            var readAllLines = File.ReadAllLines(syntaxFilePath);
            ReadSymbolName(readAllLines);
            ModifyCreateObjectFunction(readAllLines, syntaxFilePath);
            GenerateClass(readAllLines,productionClassRootPath);
        }

        private static void ReadSymbolName(string[] texts)
        {
            bool startRead = false;
            for (int i = 0; i < texts.Length; i++)
            {
                var text = texts[i];
                if (text.Contains("enum SymbolIndex"))
                {
                    startRead = true;
                }
                if(startRead)
                {
                    if (text.TrimStart().StartsWith("}"))
                    {
                        break;
                    }
                    var regex = new Regex("(@)([a-zA-Z]*)( = )([0-9]*)(.*// )(.*)");
                    
                    var match = regex.Match(text);
                    if (match.Success)
                    {
//                        Console.WriteLine("{0} is {1}", match.Groups[2].Value, match.Groups[6].Value);
                        SymbolNames.Add(match.Groups[6].Value, match.Groups[2].Value);
                    }
                }
            }
        }

        private static void GenerateClass(string[] texts, string productionClassRootPath)
        {
            foreach (var value in BasisProduction.Values)
            {
                var classText = BasisClass.Replace("{0}", value.GetGenerateClassName());
                classText = classText.Replace("{1}", value.Data);
                File.WriteAllText(string.Format("{0}/Basis/{1}.cs", productionClassRootPath, value.GetGenerateClassName()), classText);
            }
            foreach (var reduction in Reductions)
            {
                StringBuilder fields = new StringBuilder();
                Dictionary<string,int> nameCounter = new Dictionary<string, int>();
                for (int i = 0; i < reduction.Children.Count; i++)
                {
                    var symbol = reduction.Children[i];
                    var generateInfo = symbol.GetGenerateInfo();
                    if (nameCounter.ContainsKey(generateInfo))
                    {
                        nameCounter[generateInfo]++;
                    }
                    else
                    {
                        nameCounter[generateInfo] = 1;
                    }
                    fields.AppendFormat("        public Token<{0}> {1}{2};", symbol.GetGenerateClassName(), generateInfo, nameCounter[generateInfo] == 1?"":string.Format("_{0}", nameCounter[generateInfo]));
                    if (i < reduction.Children.Count - 1)
                    {
                        fields.Append("\r\n");
                    }
                }

                StringBuilder arguments = new StringBuilder();
                if (reduction.Children.Count > 0)
                {
                    arguments.Append(", ");
                }
                for (int i = 0; i < reduction.Children.Count; i++)
                {
                    arguments.AppendFormat("GOLD.Token token{0}", i);
                    if (i < reduction.Children.Count - 1)
                    {
                        arguments.Append(",");
                    }
                }

                StringBuilder initializeBlock = new StringBuilder();
                for (int i = 0; i < reduction.Children.Count; i++)
                {
                    var symbol = reduction.Children[i];
                    var generateInfo = symbol.GetGenerateInfo();
                    initializeBlock.AppendFormat("            this.{0} = new Token<{1}>() {{Column = token{2}.Position().Column,Line = token{2} .Position().Line,Symbol = ({1})token{2}.Data}};", generateInfo, symbol.GetGenerateClassName(),i);
                    initializeBlock.Append("\r\n");
                    initializeBlock.AppendFormat("            this.Children.Add(this.{0});", generateInfo);
                    if (i < reduction.Children.Count - 1)
                    {
                        initializeBlock.Append("\r\n");
                    }
                }

                StringBuilder reductionText = new StringBuilder();
                reductionText.Append(reduction.Name.Data);
                reductionText.Append(" ::= ");
                for (int i = 0; i < reduction.Children.Count; i++)
                {
                    reductionText.Append(reduction.Children[i].Data);
                    if (i < reduction.Children.Count - 1)
                    {
                        reductionText.Append(" ");
                    }
                }

                var classText = ReductionClass;
                classText = classText.Replace("{0}", reduction.GetGenerateClassName());
                classText = classText.Replace("{1}", reduction.Name.GetGenerateClassName());
                classText = classText.Replace("{2}", fields.ToString());
                classText = classText.Replace("{3}", arguments.ToString());
                classText = classText.Replace("{4}", initializeBlock.ToString());
                classText = classText.Replace("{5}", reductionText.ToString());
                File.WriteAllText(string.Format("{0}/{1}.cs", productionClassRootPath, reduction.GetGenerateClassName()), classText);
            }
        }

        private static void ModifyCreateObjectFunction(string[] texts, string syntaxFilePath)
        {
            var codeLines = texts.ToList();
            for (int i = 0; i < codeLines.Count; i++)
            {
                var line = codeLines[i];
                if (line.Contains("case ProductionIndex."))
                {
                    var reduction = Reduction.Parse(codeLines[i + 1]);
                    if (!BasisProduction.ContainsKey(reduction.Name.Data))
                    {
                        BasisProduction.Add(reduction.Name.Data, reduction.Name);
                    }
                    Reductions.Add(reduction);
                    codeLines.Insert(i + 2, reduction.GetCreateCode());
                    //                    codeLines[i + 1] = reduction.GetCreateCode() + codeLines[i + 1];
                }
            }
            File.WriteAllLines(syntaxFilePath, codeLines.ToArray());
        }

        //        private static void 

        private class Reduction
        {
            public static Reduction Parse(string text)
            {
                List<string> symbols = new List<string>();
                var regex = new Regex("(('([^'])*')|(<([^<>])*>)|([a-zA-Z]*))");
                var matchCollection = regex.Matches(text);
                foreach (var match in matchCollection)
                {
                    if (!string.IsNullOrEmpty(match.ToString()))
                    {
                        symbols.Add(match.ToString());
                    }
                }

                var reduction = new Reduction();
                reduction.Name = new Symbol(symbols[0]);
                for (int i = 1; i < symbols.Count; i++)
                {
                    reduction.Children.Add(new Symbol(symbols[i]));
                }
                return reduction;
            }

            public Symbol Name;

            public List<Symbol> Children = new List<Symbol>();

            public string GetCreateCode()
            {
                StringBuilder b = new StringBuilder();
                b.Append("                result = new ");
                b.Append(this.GetGenerateClassName());
                b.Append("(project,@class");
                if (this.Children.Count > 0)
                {
                    b.Append(",");
                }
                for (int i = 0; i < this.Children.Count; i++)
                {
                    b.AppendFormat("r[{0}]", i);
                    if (i < this.Children.Count - 1)
                    {
                        b.Append(",");
                    }
                }
                b.Append(");");
                return b.ToString();
            }

            public string GetGenerateClassName()
            {
                StringBuilder b = new StringBuilder();
                b.Append(this.Name.GetGenerateInfo());
                b.Append('_');
                for (int i = 0; i < this.Children.Count; i++)
                {
                    var symbol = this.Children[i];
                    b.Append(symbol.GetGenerateInfo());
                    if (i < this.Children.Count - 1)
                    {
                        b.Append("_");
                    }
                }
                return b.ToString();
            }
        }

        private class Symbol
        {
            public string Data;

            public Symbol(string data)
            {
                this.Data = data;
            }

            public bool IsProduction()
            {
                return this.Data.StartsWith("<");
            }

            public bool IsTerminal()
            {
                return !this.IsProduction();
            }

            public string GetGenerateInfo()
            {
                try
                {
                    return ProductionObjectGenerator.SymbolNames[this.Data];
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            public string GetGenerateClassName()
            {
                if (this.IsProduction())
                {
                    var productionName = this.Data.Substring(1, this.Data.Length - 2);
                    productionName = productionName.Substring(0, 1).ToUpper() + productionName.Substring(1, productionName.Length - 1);
                    productionName = productionName.Replace(" ", "_");
                    return string.Format("{0}_basisproduction", productionName);
                }
                return "string";
            }
        }
    }
}