// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>12/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Package
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    public class PackagesContext
    {
        public Production PackageList;

        public Class Class { get; }

        public Project Project { get; }

        private Dictionary<string, Class> classMap = new Dictionary<string, Class>();

        private Dictionary<string, Luafile> luafileMap = new Dictionary<string, Luafile>();

        private Dictionary<Class, bool> importedClass = new Dictionary<Class, bool>(); 

        public PackagesContext(Class tlClass)
        {
            this.Class = tlClass;
            this.Project = tlClass.Project;
        }

        public void Import(string context, string alias = null)
        {
            //包？
            var package = this.Project.GetPackage(context);
            if (package != null)
            {
                if (alias != null)
                {
                    throw new FileParseException("Cannot name a package.");
                }
                foreach (var c in package.Values)
                {
                    this.Import(c.ClassName, c);
                }
                return;
            }

            //类？
            var tlClass = this.Project.GetClass(context);
            if (tlClass != null)
            {
                this.Import(alias ?? tlClass.ClassName, tlClass);
                return;
            }

            //lua？
            var luafile = this.Project.GetLuaFile(context);
            if (luafile != null)
            {
                this.Import(alias ?? luafile.Name, luafile);
                return;
            }

            throw new FileParseException(string.Format("Cannot find package or class named '{0}'.", context));
        }

        private void Import(string className, Class tlClass)
        {
            if (this.luafileMap.ContainsKey(className))
            {
                throw new FileParseException("Import failed, lua files with same name is already exist.");
            }
            if (this.classMap.ContainsKey(className))
            {
                throw new FileParseException("Import failed, class with same name is already exist.");
            }
            if (this.importedClass.ContainsKey(tlClass))
            {
                throw new FileParseException("Import failed, class already imported.");
            }
            this.classMap.Add(className, tlClass);
            this.importedClass.Add(tlClass,true);
        }

        private void Import(string className, Luafile luafile)
        {
            if (this.luafileMap.ContainsKey(className))
            {
                throw new FileParseException("Import failed, lua files with same name is already exist.");
            }
            if (this.classMap.ContainsKey(className))
            {
                throw new FileParseException("Import failed, class with same name is already exist.");
            }
            this.luafileMap.Add(className, luafile);
        }

        public Class GetClass(string className)
        {
            Class c;
            this.classMap.TryGetValue(className, out c);
            return c;
        }

        public Luafile GetLuaFile(string name)
        {
            Luafile f;
            this.luafileMap.TryGetValue(name, out f);
            return f;
        }

        public Type GetTLType(string name)
        {
            var c = this.GetClass(name);
            if (c != null)
            {
                return c.GetTLType();
            }
            var f = this.GetLuaFile(name);
            if (f != null)
            {
                return Type.Table;
            }
            return Type.GetBuiltInType(name);
        }

        public IEnumerable<Class> GetClasses()
        {
            return this.classMap.Values;
        }

        public Dictionary<string, Class> GetClassMap()
        {
            return this.classMap;
        }

        public Dictionary<string, Luafile> GetLuafileMap()
        {
            return this.luafileMap;
        }
    }
}