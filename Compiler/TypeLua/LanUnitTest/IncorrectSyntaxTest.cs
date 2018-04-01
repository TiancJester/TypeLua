// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>13/03/2018</date>
// ----------------------------------------------------------------------------
namespace LanUnitTest
{
    using System;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TypeLua.Project;
    using TypeLua.Project.Exception;

    [TestClass]
    public class IncorrectSyntaxTest : CommonTest
    {
        [TestMethod]
        public void TestAll()
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), "IncorrectSyntax/");
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                this.TestIncorrectSyntax(file);
            }
        }

        private void TestIncorrectSyntax(string file)
        {
            try
            {
                this.TestFile(file);
            }
            catch (FileParseException e)
            {
                return;
            }
            throw new Exception(string.Format("No error found in {0}.", file));
        }
    }
}