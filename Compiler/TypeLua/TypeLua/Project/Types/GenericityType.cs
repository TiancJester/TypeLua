// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>08/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    using System.Text;

    using TypeLua.Project.Package;

    public class GenericityType : Type
    {
        public static new GenericityType Any = new GenericityType("any", "", Type.Any, Type.Any, TypeCategory.Function);

        private Type[] firstGroupGenericTypeArguments;
        public Type[] FirstGroupGenericTypeArguments
        {
            get
            {
                return this.firstGroupGenericTypeArguments;
            }
            set
            {
                this.firstGroupGenericTypeArguments = value;
                this.genericityTypeName = null;
            }
        }

        private Type[] secondGroupGenericTypeArguments;

        public Type[] SecondGroupGenericTypeArguments
        {
            get
            {
                return this.secondGroupGenericTypeArguments;
            }
            set
            {
                this.secondGroupGenericTypeArguments = value;
                this.genericityTypeName = null;
            }
        }

        private string genericityTypeName;


        public GenericityType(string name, string package, Type[] firstGroupGenericTypeArguments, Type[] secondGroupGenericTypeArguments, TypeCategory category)
            : base(name, package, category)
        {
            this.FirstGroupGenericTypeArguments = firstGroupGenericTypeArguments;
            this.SecondGroupGenericTypeArguments = secondGroupGenericTypeArguments;
        }

        public GenericityType(string name, string package, Type firstGroupGenericTypeArguments, Type secondGroupGenericTypeArguments, TypeCategory category)
            : base(name, package, category)
        {
            this.FirstGroupGenericTypeArguments = new []{ firstGroupGenericTypeArguments };
            if (secondGroupGenericTypeArguments != null)
            {
                this.SecondGroupGenericTypeArguments = new[] { secondGroupGenericTypeArguments };
            }
        }

        public override Type[] GetFirstGroupGenericTypeArguments()
        {
            return this.FirstGroupGenericTypeArguments;
        }

        public override Type[] GetSecondGroupGenericTypeArguments()
        {
            return this.SecondGroupGenericTypeArguments;
        }

        public override string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.genericityTypeName))
                {
                    var baseName = base.FullName;
                    StringBuilder builder = new StringBuilder();
                    builder.Append(baseName);
                    builder.Append("<");
                    if (this.FirstGroupGenericTypeArguments != null && this.FirstGroupGenericTypeArguments.Length > 0)
                    {
                        foreach (var genericTypeArgument in this.FirstGroupGenericTypeArguments)
                        {
                            builder.Append(genericTypeArgument.FullName);
                        }
                    }
                    if (this.SecondGroupGenericTypeArguments != null && this.SecondGroupGenericTypeArguments.Length > 0)
                    {
                        builder.Append(":");
                        foreach (var genericTypeArgument in this.SecondGroupGenericTypeArguments)
                        {
                            builder.Append(genericTypeArgument.FullName);
                        }
                    }
                    builder.Append(">");
                    this.genericityTypeName = builder.ToString();
                }
                
                return this.genericityTypeName;
            }
        }

        public override void ClarifyType(PackagesContext packagesContext)
        {
            if (!this.IsDecidedType())
            {
                if (this.FirstGroupGenericTypeArguments != null)
                {
                    foreach (var genericTypeArgument in this.FirstGroupGenericTypeArguments)
                    {
                        genericTypeArgument.ClarifyType(packagesContext);
                    }
                }
                if (this.SecondGroupGenericTypeArguments != null)
                {
                    foreach (var genericTypeArgument in this.SecondGroupGenericTypeArguments)
                    {
                        genericTypeArgument.ClarifyType(packagesContext);
                    }
                }
            }
            base.ClarifyType(packagesContext);
        }

        public override bool IsDecidedType()
        {
            if (this.FirstGroupGenericTypeArguments != null)
            {
                foreach (var genericTypeArgument in this.FirstGroupGenericTypeArguments)
                {
                    if (!genericTypeArgument.IsDecidedType())
                    {
                        return false;
                    }
                }
            }
            if (this.SecondGroupGenericTypeArguments != null)
            {
                foreach (var genericTypeArgument in this.SecondGroupGenericTypeArguments)
                {
                    if (!genericTypeArgument.IsDecidedType())
                    {
                        return false;
                    }
                }
            }
            return base.IsDecidedType();
        }
    }
}