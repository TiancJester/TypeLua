using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LanUnitTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;

    [TestClass]
    public class UnitTest : CommonTest
    {
        [TestMethod]
        public void TestSyntax()
        {
            var myParser = new TypeLuaParser();
            myParser.Setup();
            var filesDir = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles");
            var fields = Directory.GetFiles(filesDir);
            foreach (var field in fields)
            {
                var combine = Path.Combine(field);
                var readAllText = File.ReadAllText(combine);
                string state = "";
                try
                {
                    if (myParser.Parse(new StringReader(readAllText)))
                    {
                        state = "pass:" + Path.GetFileNameWithoutExtension(field);
                    }
                    else
                    {
                        state = "error:" + Path.GetFileNameWithoutExtension(field);
                    }
                }
                catch (Exception e)
                {
                    state = e.Message + ":" + Path.GetFileNameWithoutExtension(field);
                }

                Console.WriteLine(state);
            }
        }

        [TestMethod]
        public void TestProject()
        {
            this.TestFiles(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles/TestProject"), "ClassA.tl", "ClassB.tl", "com/ClassC.tl");
        }

        [TestMethod]
        public void TestFileX()
        {
            this.TestFiles(Path.Combine(Directory.GetCurrentDirectory(), "TestFiles/TestProject"), "com/ClassX.tl");
        }


        private void TestFiles(string root,params string[] classPaths)
        {
            var pathes = classPaths.ToList();
            for (int i = 0; i < classPaths.Length; i++)
            {
                var classPath = classPaths[i];
                pathes[i] = Path.Combine(root, classPath);
            }
            this.TestFiles(pathes);
        }
    }
}
