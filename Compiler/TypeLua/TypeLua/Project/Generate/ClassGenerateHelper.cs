// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>19/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Generate
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.Project.Element;
    using TypeLua.Project.Types;

    public static class ClassGenerateHelper
    {
        //        private static string classdefine = @"--== start class define ==--
        //local Apple = tlclass(\"generate.Common.Fruits.Apple\",\"generate.Common.Fruits.Fruit\")
        //";

        private static Dictionary<int, string> indentations = new Dictionary<int, string>();

        public static string GetIndentation(this int depth)
        {
            string indentation;
            if (!indentations.TryGetValue(depth, out indentation))
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < depth; i++)
                {
                    builder.Append("    ");
                }
                indentation = builder.ToString();
                indentations.Add(depth, indentation);
            }
            return indentation;
        }

        public static void GenerateLua(this Class c,string root, StringBuilder builder)
        {
            //
            List<Field> globalFields, staticFields, classFields;
            List<Function> globalFunctions, staticFunctions, classFunctions;
            c.GetMembers(out globalFields, out staticFields, out classFields, out globalFunctions, out staticFunctions, out classFunctions);

            //
            builder.AppendLine("--== start class define ==--");
            if (c.BaseClass == null)
            {
                builder.AppendLine(string.Format("local {0} = tlclass(\"{1}\")", c.ClassName, c.GetLuaFullName(root)));
            }
            else
            {
                builder.AppendLine(string.Format("local {0} = tlclass(\"{1}\",\"{2}\")", c.ClassName, c.GetLuaFullName(root), c.BaseClass.GetLuaFullName(root)));
            }
            if (classFunctions.Count > 0)
            {
                builder.Append(string.Format("tlmethod({0}",c.ClassName));
                for (int i = 0; i < classFunctions.Count; i++)
                {
                    builder.Append(",");
                    var classFunction = classFunctions[i];
                    builder.Append(string.Format("\"{0}\"", classFunction.Name));
                }
                builder.AppendLine(")");
            }
            
            builder.AppendLine();

            //
            builder.AppendLine("--== require modules ==--");
            var classMap = c.Packages.GetClassMap();
            foreach (var importClass in classMap)
            {
                if (importClass.Value != c)
                {
                    builder.AppendLine(string.Format("local {0}", importClass.Key));
                }
            }
            var luafileMap = c.Packages.GetLuafileMap();
            foreach (var luafile in luafileMap)
            {
                builder.AppendLine(string.Format("local {0}", luafile.Key));
            }
            builder.Append("\r\n");
            builder.AppendLine(string.Format("function {0}._loadreference()", c.ClassName));
            foreach (var importClass in classMap)
            {
                if (importClass.Value != c)
                {
                    builder.AppendLine(string.Format("    {0} = tlload(\"{1}\")", importClass.Key, importClass.Value.GetLuaFullName(root)));
                }
            }
            foreach (var luafile in luafileMap)
            {
                builder.AppendLine(string.Format("    {0} = require(\"{1}\")", luafile.Key, luafile.Value.GetLuaFullName(root)));
            }
            builder.AppendLine("end");

            //
            StringBuilder internalBlock = new StringBuilder();
            if (c.StaticConstructor != null)
            {
                builder.AppendLine("--== static constructor ==--");
                builder.AppendFormat("function {0}._staticctor", c.ClassName);
                var functionBody = c.StaticConstructor.Body as IFunctionBody;

                internalBlock.Clear();
                GenerateFields(globalFields, "", 1, c, root, internalBlock);
                GenerateFields(staticFields, "", 1, c, root, internalBlock);
                functionBody.AddFunctionCode(internalBlock.ToString());

                c.StaticConstructor.Body.GenerateLua(c, root, builder, 0);
                builder.AppendLine();
            }
            else if(globalFields.Count > 0 || staticFields.Count > 0)
            {
                builder.AppendLine("--== static constructor ==--");
                builder.AppendLine(string.Format("function {0}._staticctor()", c.ClassName));
                internalBlock.Clear();
                GenerateFields(globalFields, "", 1, c, root, internalBlock);
                GenerateFields(staticFields, "", 1, c, root, internalBlock);
                builder.Append(internalBlock.ToString());
                builder.AppendLine("end");
            }

            //
            //            GenerateFields(globalFields, "--== global fileds ==--", 0, c, root, builder);
            //
            GenerateFunctions(globalFunctions, "--== global functions ==--", c, root, builder);
            //
//            GenerateFields(staticFields, "--== static fileds ==--", 0, c, root, builder);
            //
            GenerateFunctions(staticFunctions, "--== static functions ==--", c, root, builder);
            //
            GenerateFields(classFields, "--== class fileds ==--", 0, c, root, builder);
            //
            if (c.ClassConstructor != null)
            {
                builder.AppendLine("--== constructor ==--");
                builder.AppendFormat("function {0}:_ctor", c.ClassName);

                var functionBody = c.ClassConstructor.Body as IFunctionBody;
                internalBlock.Clear();
                GenerateFields(classFields, "", 1, c, root, internalBlock);
                functionBody.AddFunctionCode(internalBlock.ToString());

                c.ClassConstructor.Body.GenerateLua(c, root, builder, 0);
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine("--== constructor ==--");
                builder.AppendLine(string.Format("function {0}:_ctor()", c.ClassName));
                internalBlock.Clear();
                GenerateFields(classFields, "", 1, c, root, internalBlock);
                builder.Append(internalBlock.ToString());
                builder.AppendLine("end");
            }
            //
            GenerateFunctions(classFunctions, "--== class functions ==--", c, root, builder);

            //
            builder.AppendLine("--== end class define ==--");
            builder.AppendLine(string.Format("return {0}", c.ClassName));
        }

        private static string GetLuaFullName(this Class c, string root)
        {
            return root + "." + c.ClassPath.Replace("/", ".");
        }

        private static string GetLuaFullName(this Luafile c, string root)
        {
            return root + "." + c.FullName;
        }

        private static void GetMembers(this Class c,
            out List<Field> globalFields,
            out List<Field> staticFields,
            out List<Field> classFields,
            out List<Function> globalFunctions,
            out List<Function> staticFunctions,
            out List<Function> classFunctions)
        {
            globalFields = new List<Field>();
            staticFields = new List<Field>();
            classFields = new List<Field>();
            globalFunctions = new List<Function>();
            staticFunctions = new List<Function>();
            classFunctions = new List<Function>();

//            foreach (var contextElement in c.Global.GetAllElements().Values)
//            {
//                if (contextElement is Field)
//                {
//                    var field = contextElement as Field;
//                    if ((int)(field.Access & AccessType.Global) > 0)
//                    {
//                        globalFields.Add(field);
//                    }
//                    else if ((int)(field.Access & AccessType.Static) > 0)
//                    {
//                        staticFields.Add(field);
//                    }
//                    else
//                    {
//                        classFields.Add(field);
//                    }
//                }
//                if (contextElement is Function)
//                {
//                    var function = contextElement as Function;
//                    if ((int)(function.Access & AccessType.Global) > 0)
//                    {
//                        globalFunctions.Add(function);
//                    }
//                    else if ((int)(function.Access & AccessType.Static) > 0)
//                    {
//                        staticFunctions.Add(function);
//                    }
//                    else
//                    {
//                        classFunctions.Add(function);
//                    }
//                }
//            }
            foreach (var contextElement in c.Fields.GetAllElements().Values)
            {
                var field = contextElement as Field;
                if ((field.Access & AccessType.Global) > 0)
                {
                    globalFields.Add(field);
                }
                else if ((field.Access & AccessType.Static) > 0)
                {
                    staticFields.Add(field);
                }
                else
                {
                    classFields.Add(field);
                }
            }
            foreach (var contextElement in c.Methods.GetAllElements().Values)
            {
                var function = contextElement as Function;
                if ((int)(function.Access & AccessType.Global) > 0)
                {
                    globalFunctions.Add(function);
                }
                else if ((int)(function.Access & AccessType.Static) > 0)
                {
                    staticFunctions.Add(function);
                }
                else
                {
                    classFunctions.Add(function);
                }
            }
        }

        private static void GenerateFields(List<Field> fields, string title,int depth, Class c, string root, StringBuilder builder)
        {
            if (fields.Count > 0)
            {
                if (title != null)
                {
                    builder.AppendLine(title);
                }
                foreach (var field in fields)
                {
                    field.GenerateField(depth, c, root, builder);
                }
            }
        }
        private static void GenerateFunctions(List<Function> functions, string title, Class c, string root, StringBuilder builder)
        {
            if (functions.Count > 0)
            {
                if (title != null)
                {
                    builder.AppendLine(title);
                }
                foreach (var function in functions)
                {
                    function.GenerateFunction(c, root, builder);
                    builder.AppendLine();
                }
            }
        }

        private static void GenerateField(this Field field,int depth, Class c, string root, StringBuilder builder)
        {
            field.DefineProduction.GenerateLua(c, root, builder, depth);
            builder.AppendLine();
        }

        private static void GenerateFunction(this Function function, Class c, string root, StringBuilder builder)
        {
            if (function.Body != null)
            {
                string access = null;
                string module = null;
                if ((function.Access & AccessType.Global) > 0)
                {
                    access = "";
                    module = "";
                }
                else if ((function.Access & AccessType.Static) > 0)
                {
                    access = ".";
                    module = c.ClassName;
                }
                else
                {
                    access = ":";
                    module = c.ClassName;
                }
                builder.Append(string.Format("function {0}{1}{2}", module, access, function.Name));
                function.Body.GenerateLua(c, root, builder, 0);

                builder.AppendLine();
            }
        }
    }
}