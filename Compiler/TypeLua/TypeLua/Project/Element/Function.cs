// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Element
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Types;

    using Type = TypeLua.Project.Types.Type;

    public class Function : ContextElement, IContext, IClassMember
    {
        private Type[] returnValueTypes;
        public Type[] ReturnValueTypes
        {
            get
            {
                if (this.returnValueTypes == null && this.Type != null)
                {
                    var genericityType = this.Type as GenericityType;
                    if (genericityType.FirstGroupGenericTypeArguments != null)
                    {
                        this.returnValueTypes = genericityType.FirstGroupGenericTypeArguments;
                    }
                }
                return this.returnValueTypes;
            }
            set
            {
                this.returnValueTypes = value;
            }
        }

        public Parameter[] Parameters;

        public Class ClassContext { get; private set; }

        public AccessType Access { get; set; }

        public Production Body;

        public Context FunctionContext;

        public Function(string name, AccessType access, Class classContext, IContext parent, Type[] returnValueTypes, Parameter[] @params, Production body)
            : base(name)
        {
            this.Access = access;
            this.ClassContext = classContext;
            this.returnValueTypes = returnValueTypes;
            this.Parameters = @params;
            this.Body = body;

            this.FunctionContext = new Context();
            this.FunctionContext.ParentContext = parent;
            this.FunctionContext.ClassContext = classContext;

            if (this.Parameters != null && this.Parameters.Length > 0)
            {
                foreach (var parameter in this.Parameters)
                {
                    this.FunctionContext.AddElement(parameter);
                }
            }

            this.ResetType();
        }

        public void ResetType()
        {
            Type[] paramTypes = null;
            if (this.Parameters != null)
            {
                paramTypes = new Type[this.Parameters.Length];
                for (int i = 0; i < this.Parameters.Length; i++)
                {
                    var parameter = this.Parameters[i];
                    paramTypes[i] = parameter.Type;
                }
            }

            Type[] firstGroupGenericTypeArguments = null;
            Type[] secondGroupGenericTypeArguments = null;
            if (this.returnValueTypes == null)
            {
                firstGroupGenericTypeArguments = null;
            }
            else if (this.returnValueTypes.Length == 0)
            {
                firstGroupGenericTypeArguments = new[] { Type.Void };
            }
            else
            {
                firstGroupGenericTypeArguments = this.returnValueTypes;
            }

            if (paramTypes == null)
            {
                secondGroupGenericTypeArguments = null;
            }
            else if (paramTypes.Length == 0)
            {
                secondGroupGenericTypeArguments = new[] { Type.Void };
            }
            else
            {
                secondGroupGenericTypeArguments = paramTypes;
            }
            
            this.Type = new GenericityType(Type.Function.Name, "",firstGroupGenericTypeArguments,secondGroupGenericTypeArguments,TypeCategory.Function);
        }

        public IContext ParentContext
        {
            get
            {
                return this.ClassContext;
            }
        }

        public ContextElement GetElement(string name, IContext current)
        {
            return this.FunctionContext.GetElement(name, current);
        }

        public void AddElement(ContextElement element)
        {
            this.FunctionContext.AddElement(element);
        }

        public override ContextElementCategory ElementCategory { get { return ContextElementCategory.Function; } }


        public override Type Type { get; set; }
    }
}