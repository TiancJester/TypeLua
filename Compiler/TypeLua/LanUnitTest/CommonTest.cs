// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>14/03/2018</date>
// ----------------------------------------------------------------------------
namespace LanUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TypeLua.Project;
    using TypeLua.Project.Exception;

    public class CommonTest
    {
        protected static string ProjectRoot;

        protected static string[] CommonFiles;

        static CommonTest()
        {
            ProjectRoot = Directory.GetCurrentDirectory();
            var commonRoot = Path.Combine(ProjectRoot, "Common/");
            CommonFiles = Directory.GetFiles(commonRoot, "*.*", SearchOption.AllDirectories);
        }

        protected Project TestFile(string filePath)
        {
            List<string> files = new List<string>(CommonFiles.Length + 1);
            files.AddRange(CommonFiles);
            files.Add(filePath);
            return this.TestProject(files);
        }

        protected Project TestFiles(List<string> files)
        {
            files.AddRange(CommonFiles);
            return this.TestProject(files);
        }

        protected Project TestProject(string root)
        {
            List<string> files = new List<string>(CommonFiles.Length + 1);
            files.AddRange(CommonFiles);

            var testFiles = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            files.AddRange(testFiles);

            return this.TestProject(files);
        }

        private Project TestProject(List<string> files)
        {
            return Project.Build(ProjectRoot, files.ToArray());
        }
    }
}