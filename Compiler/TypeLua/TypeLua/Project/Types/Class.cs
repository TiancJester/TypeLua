// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Element;
    using TypeLua.Project.Package;

    public class Class : IContext
    {
        public string ClassName;

        public string PackageName;

        public string BaseClassName;

        public Production BaseClassProduction;
        
        public Class BaseClass;

        private Dictionary<Class,bool> prototypeChainMap;

        public Dictionary<Class, bool> PrototypeChainMap
        {
            get
            {
                if (this.prototypeChainMap == null)
                {
                    this.prototypeChainMap = new Dictionary<Class, bool>();
                    foreach (var c in this.PrototypeChain)
                    {
                        this.prototypeChainMap.Add(c, true);
                    }
                }
                return this.prototypeChainMap;
            }
        }
        private List<Class> prototypeChain;
        public List<Class> PrototypeChain
        {
            get
            {
                if (this.prototypeChain == null)
                {
                    this.prototypeChain = new List<Class>();
                    Class parent = this.BaseClass;
                    while (parent != null)
                    {
                        this.prototypeChain.Add(parent);
                        parent = parent.BaseClass;
                    }
                }
                return this.prototypeChain;
            }
        }

        private Types.Type type;

        public Project Project;

        public string ClassPath;

        public PackagesContext Packages;

        public Function StaticConstructor;

        public Function ClassConstructor;

        public Context Fields = new Context();

        public Context Methods = new Context();

        public Context Global = new Context();

        
        public Class(Project project,string path)
        {
            this.Project = project;
            this.ClassPath = path;
            this.Packages = new PackagesContext(this);
        }

        public string ClassFullName
        {
            get
            {
                return string.Format("{0}.{1}", this.PackageName, this.ClassName);
            }
        }

        public IContext ParentContext
        {
            get
            {
                return this.BaseClass;
            }
        }

        public Class ClassContext { get
        {
            return this;
        } }

        public ContextElement GetElement(string name, IContext current)
        {
            if (name == this.ClassName)
            {
                return this.ClassConstructor;
            }

            //member
            ContextElement element = null;
            AccessType accessType = AccessType.Any;
            if (this.Fields.ContainsElement(name))
            {
                var field = this.Fields.GetElement(name, current) as Field;
                element = field;
                accessType = field.Access;
            }
            else if (this.Methods.ContainsElement(name))
            {
                var function = this.Methods.GetElement(name, current) as Function;
                element = function;
                accessType = function.Access;
            }
            else if (this.Global.ContainsElement(name))
            {
                var member = this.Global.GetElement(name, current) as IClassMember;
                element = (ContextElement)member;
                accessType = member.Access;
            }
            if (current.ClassContext == this)
            {
                return element;
            }
            if (current.ClassContext.IsDerivedOf(this) && (accessType & (AccessType.Public | AccessType.Protected)) > 0)
            {
                return element;
            }
            if ((accessType & AccessType.Public) > 0)
            {
                return element;
            }
            return null;
        }

        public void AddElement(ContextElement element)
        {
            throw new Exception("Cannot add context element at class.");
        }

        public bool IsDerivedOf(Class c)
        {
            return this.PrototypeChainMap.ContainsKey(c);
        }

        public Types.Type GetTLType()
        {
            if (this.type == null)
            {
                this.type = new Types.Type(this.ClassName, this.PackageName, TypeCategory.Class);
            }
            return this.type;
        }
    }
}