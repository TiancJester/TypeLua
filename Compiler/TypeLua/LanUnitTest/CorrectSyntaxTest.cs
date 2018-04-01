// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>14/03/2018</date>
// ----------------------------------------------------------------------------
namespace LanUnitTest
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TypeLua.Project.Exception;

    [TestClass]
    public class CorrectSyntaxTest : CommonTest
    {
        [TestMethod]
        public void TestAll()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "CorrectSyntax/");
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                this.TestSyntax(file);
            }
        }

        [TestMethod]
        public void TestOne()
        {
            string fileName = "AssignmentExp.tl";
            var root = Path.Combine(Directory.GetCurrentDirectory(), "CorrectSyntax/");
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == fileName)
                {
                    this.TestSyntax(file);
                }
                
            }
        }

        private void TestSyntax(string file)
        {
            Console.WriteLine(file);
            this.TestFile(file);
        }
    }
}