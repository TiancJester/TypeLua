// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Types;

    public class Project
    {
        public static Project Build(string projectRoot, params string[] classPaths)
        {
            var myParser = new TypeLuaParser();
            myParser.Setup();
            
            var project = new Project(projectRoot);

            projectRoot = projectRoot + "\\";

            Types.Class tlClass = null;
            List<Types.Class> classes = new List<Types.Class>();

            try
            {
                foreach (var classPath in classPaths)
                {
                    var path = classPath.Replace(projectRoot, "");
                    path = path.Replace("\\", "/");
                    var extension = Path.GetExtension(path);
                    if (extension == ".tl")
                    {
                        tlClass = new Types.Class(project, path.Substring(0, path.Length - 3));
                        var readAllText = File.ReadAllText(classPath);

                        myParser.Parse(new StringReader(readAllText), project, tlClass);
                        classes.Add(tlClass);
                    }
                    else if (extension == ".lua")
                    {
                        var luafile = new Luafile(project, path.Substring(0, path.Length - 4));
                        project.luafiles[luafile.FullName] = luafile;
                    }
                }

                var classParser = new ClassParser();

                for (int i = 0; i < classes.Count; i++)
                {
                    tlClass = classes[i];
                    tlClass.Packages.PackageList.BuildPackageContext(tlClass.Packages);
                }

                for (int i = 0; i < classes.Count; i++)
                {
                    tlClass = classes[i];
                    classParser.BuildGlobalContext(tlClass);
                }

                for (int i = 0; i < classes.Count; i++)
                {
                    tlClass = classes[i];
                    classParser.ClarifyType(tlClass);
                }

                for (int i = 0; i < classes.Count; i++)
                {
                    tlClass = classes[i];
                    classParser.TypeVerify(tlClass);
                }
            }
            catch (FileParseException e)
            {
                e.FileName = tlClass.ClassPath;
                Console.WriteLine(e.StackTrace);
                throw e;
            }
            
            return project;
        }

        /// <summary>
        /// lua full name => luafile
        /// </summary>
        private Dictionary<string, Luafile> luafiles = new Dictionary<string, Luafile>();

        /// <summary>
        /// class full name => class
        /// </summary>
        private Dictionary<string, Types.Class> classes = new Dictionary<string, Types.Class>();

        /// <summary>
        /// package name => (class name => class)
        /// </summary>
        private Dictionary<string, Dictionary<string, Types.Class>> packageClasses = new Dictionary<string, Dictionary<string, Types.Class>>();

        public string RootPath;

        public static Project Instance { get; private set; }

        public Project(string path)
        {
            Instance = this;
            this.RootPath = path;
        }

        public Types.Class GetClass(string fullName)
        {
            Types.Class c;
            this.classes.TryGetValue(fullName, out c);
            return c;
        }

        public Dictionary<string, Types.Class> GetPackage(string packageName)
        {
            Dictionary<string, Types.Class> c;
            this.packageClasses.TryGetValue(packageName, out c);
            return c;
        }

        public Luafile GetLuaFile(string fullName)
        {
            Luafile c;
            this.luafiles.TryGetValue(fullName, out c);
            return c;
        }

        /// <summary>
        /// add class to project context.
        /// </summary>
        /// <param name="tlClass">The tl class.</param>
        /// <exception cref="FileParseException">
        /// </exception>
        public void AddClass(Types.Class tlClass)
        {
            if (this.classes.ContainsKey(tlClass.ClassFullName))
            {
                throw new FileParseException(string.Format("class '{0}' already exist.", tlClass.ClassFullName));
            }
            if (this.packageClasses.ContainsKey(tlClass.ClassFullName))
            {
                throw new FileParseException(string.Format("Threr is already a package '{0}' in the project.", tlClass.ClassFullName));
            }

            Dictionary<string, Types.Class> packageClass;
            this.packageClasses.TryGetValue(tlClass.PackageName, out packageClass);
            if (packageClass == null)
            {
                packageClass = new Dictionary<string, Types.Class>();
                this.packageClasses.Add(tlClass.PackageName,packageClass);
            }
            packageClass.Add(tlClass.ClassName, tlClass);

            this.classes.Add(tlClass.ClassFullName, tlClass);
        }

        /// <summary>
        /// Generates the lua project.
        /// </summary>
        /// <param name="luaProjectRootPath">The lua project root path.</param>
        public void GenerateLuaProject(string luaProjectRootPath)
        {
            //复制.lua文件
            foreach (var luafile in this.luafiles)
            {
                var replace = luafile.Key.Replace(".","\\");
                var from = Path.Combine(this.RootPath, replace) + ".lua";
                var to = Path.Combine(luaProjectRootPath, replace) + ".lua";

                var directoryName = Path.GetDirectoryName(to);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.Copy(from, to, true);
            }


            var rootDic = luaProjectRootPath.Split('\\');
            var root = rootDic[rootDic.Length - 1];

            var stringBuilder = new StringBuilder();
            //翻译.tl文件为.lua
            foreach (var c in this.classes.Values)
            {
                var to = Path.Combine(luaProjectRootPath, c.ClassPath) + ".lua";
                stringBuilder.Clear();
                c.GenerateLua(root,stringBuilder);
                Console.WriteLine(stringBuilder.ToString());

                var directoryName = Path.GetDirectoryName(to);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.WriteAllText(to, stringBuilder.ToString());
            }
        }
    }
}