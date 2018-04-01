// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>14/03/2018</date>
// ----------------------------------------------------------------------------
namespace LanUnitTest
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TypeLua.Project;
    using TypeLua.Project.Exception;

    [TestClass]
    public class GenerateLuaProjectTest : CommonTest
    {
        [TestMethod]
        public void TestAll()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "CorrectSyntax/");
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                this.TestSyntax(file, "TestGenerateLua" + i);
            }
        }

        [TestMethod]
        public void TestClassTest()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "CorrectSyntax/ClassTest.tl");
            this.TestSyntax(file, "TestGenerateLua");
        }

        [TestMethod]
        public void TestFunction()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "CorrectSyntax/ClassAccess.tl");
            this.TestSyntax(file, "TestGenerateLua");
        }

        private void TestSyntax(string file,string root)
        {
            var testFile = this.TestFile(file);
            testFile.GenerateLuaProject(Path.Combine(Directory.GetCurrentDirectory(), root));
        }
    }
}