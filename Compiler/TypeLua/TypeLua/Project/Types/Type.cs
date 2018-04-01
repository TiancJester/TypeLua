// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>08/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    using System.Collections.Generic;

    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;

    public class Type
    {
        public static Type Nil = new Type("nil","", TypeCategory.Value);
        public static Type String = new Type("string","", TypeCategory.Value);
        public static Type Bool = new Type("bool","", TypeCategory.Value);
        public static Type Number = new Type("number","", TypeCategory.Value);
        public static Type Void = new Type("void","", TypeCategory.Value);
        public static Type Any = new Type("any","", TypeCategory.Value);
        public static Type Table = new Type("table","", TypeCategory.Value);
        public static Type Class = new Type("class","", TypeCategory.Value);
        public static Type This = new Type("this","", TypeCategory.Value);
        public static Type Super = new Type("super","", TypeCategory.Value);
        public static Type ListTable = new Type("ListTable", "", TypeCategory.Value);
        public static Type HashTable = new Type("HashTable", "", TypeCategory.Value);
        public static Type Function = new Type("function", "", TypeCategory.Function);

        private static Dictionary<string, Type> builtInTypes = new Dictionary<string, Type>();
        static Type()
        {
            builtInTypes.Add(Nil.Name,Nil);
            builtInTypes.Add(String.Name, String);
            builtInTypes.Add(Bool.Name, Bool);
            builtInTypes.Add(Number.Name, Number);
            builtInTypes.Add(Void.Name, Void);
            builtInTypes.Add(Any.Name, Any);
            builtInTypes.Add(Table.Name, Table);
            builtInTypes.Add(Class.Name, Class);
            builtInTypes.Add(This.Name, This);
            builtInTypes.Add(Super.Name, Super);
            builtInTypes.Add(ListTable.Name, ListTable);
            builtInTypes.Add(HashTable.Name, HashTable);
            builtInTypes.Add(Function.Name, Function);
        }

        public static Type GetBuiltInType(string typeName)
        {
            Type t;
            builtInTypes.TryGetValue(typeName, out t);
            return t;
        }

        /// <summary>
        /// 类型名字
        /// </summary>
        public string Name;

        /// <summary>
        /// 类型包名
        /// </summary>
        public string PackageName;

        /// <summary>
        /// 分类
        /// </summary>
        public TypeCategory Category { get; set; }

        private string fullName;

        public Type(string name,string package, TypeCategory category)
        {
            this.Name = name;
            this.PackageName = package;
            this.Category = category;
        }

        public virtual string FullName
        {
            get
            {
                if (this.fullName == null && this.IsDecidedType())
                {
                    if (this.PackageName == "")
                    {
                        this.fullName = this.Name;
                    }
                    else
                    {
                        this.fullName = this.PackageName + "." + this.Name;
                    }
                }
                return this.fullName;
            }
        }

        public virtual Type[] GetFirstGroupGenericTypeArguments()
        {
            return null;
        }

        public virtual Type[] GetSecondGroupGenericTypeArguments()
        {
            return null;
        }

        public virtual bool IsDecidedType()
        {
            return this.PackageName != null;
        }

        public virtual void ClarifyType(PackagesContext packagesContext)
        {
            if (!this.IsDecidedType())
            {
                var type = packagesContext.GetTLType(this.Name);
                if (type == null)
                {
                    throw new UnknowTypeException(string.Format("Cannot find type '{0}' in {1}", this.Name, packagesContext.Class.ClassFullName));
                }
                this.PackageName = type.PackageName;
                this.Name = type.Name;
            }
        }

        public virtual bool IsAssignableFrom(Type type)
        {
            if (type == Type.Nil || this == Type.Any)
            {
                return true;
            }
            if (this == type)
            {
                return true;
            }
            if (type == Table && (this == Table || (this.Name == ListTable.Name && this.PackageName == ListTable.PackageName) || (this.Name == HashTable.Name && this.PackageName == HashTable.PackageName)))
            {
                return true;
            }
            var thisClass = Project.Instance.GetClass(this.FullName);
            var targetClass = Project.Instance.GetClass(type.FullName);
            if (thisClass != null && targetClass != null && targetClass.IsDerivedOf(thisClass))
            {
                return true;
            }
            return false;
        }

        public Class GetClass()
        {
            if (this.Category == TypeCategory.Class)
            {
                return Project.Instance.GetClass(this.FullName);
            }
            return null;
        }

        public static bool operator ==(Type lhs, Type rhs)
        {
            var lo = lhs as object;
            var ro = rhs as object;
            if (lo == null && ro == null)
            {
                return true;
            }
            if (lo == null)
            {
                return false;
            }
            if (ro == null)
            {
                return false;
            }
            return lhs.FullName == rhs.FullName;
        }

        public static bool operator !=(Type lhs, Type rhs)
        {
            return !(lhs == rhs);
        }
    }
}