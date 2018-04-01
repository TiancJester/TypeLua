
namespace TypeLua.Production
{
    using System;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    using Type = TypeLua.Project.Types.Type;

    // <var exp> ::= <object exp> Member
    public class Varexp_Objectexp_Member : Var_exp_basisproduction
    {
        public Token<Object_exp_basisproduction> Objectexp;
        public Token<string> Member;

        private string member;

        private string MemberValue
        {
            get
            {
                if (member == null)
                {
                    var symbol = this.Member.Symbol;
                    member = symbol.Substring(1, symbol.Length - 1);
                }
                return member;
            }
        }

        private string accessType = ".";

        public Varexp_Objectexp_Member(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Objectexp = new Token<Object_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Object_exp_basisproduction)token0.Data};
            this.Children.Add(this.Objectexp);
            this.Member = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Member);
        }


        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var prefixList = this.Objectexp.Symbol.GetExpressions(packagesContext, expContext);
            if (prefixList.Length != 1)
            {
                throw new SyntaxException(string.Format("Cannot resolve symbol {0}",this.MemberValue),this.Member.Line,this.Member.Column);
            }
            var prefix = prefixList[0];

            switch (prefix.Classify)
            {
                case ExpressionType.Value:
                    if (prefix.Type.Name == Type.Function.Name && prefix.Type.PackageName == Type.Function.PackageName)
                    {
                        throw new SyntaxException(string.Format("Cannot resolve global symbol {0}", this.MemberValue), this.Member.Line, this.Member.Column);
                    }
                    if (prefix.Type == Type.Table || prefix.Type == Type.Any)
                    {
                        return new[] { new Expression() { Classify = ExpressionType.Value, Type = Type.Any } };
                    }
                    var c = packagesContext.GetClass(prefix.Type.Name);
                    if (c == null)
                    {
                        throw new SyntaxException(string.Format("Cannot resolve symbol {0}", this.MemberValue), this.Member.Line, this.Member.Column);
                    }
                    return this.GetExpTypeAsVariable(packagesContext, expContext, c );
                case ExpressionType.Class:
                    if (prefix.Type == Type.Any)
                    {
                        this.accessType = "";
                        return this.GetExpressionsWithValue(Type.Any);
                    }
                    if (prefix.Type == Type.Table)
                    {
                        return this.GetExpressionsWithValue(Type.Any);
                    }
                    c = packagesContext.GetClass(prefix.Type.Name);
                    if (c == null)
                    {
                        throw new SyntaxException(string.Format("Cannot resolve symbol {0}", this.MemberValue), this.Member.Line, this.Member.Column);
                    }
                    return this.GetExpTypeAsClass(packagesContext, expContext, c);
                case ExpressionType.This:
                    return this.GetExpTypeAsVariable(packagesContext, expContext, expContext.ClassContext);
                case ExpressionType.Super:
                    if (expContext.ParentContext != null && expContext.ParentContext is Class)
                    {
                        return this.GetExpTypeAsVariable(packagesContext, expContext, expContext.ParentContext);
                    }
                    break;
            }
            throw new SyntaxException(string.Format("Cannot resolve symbol {0}", this.MemberValue), this.Member.Line, this.Member.Column);
        }



        private Expression[] GetExpTypeAsClass(
            PackagesContext packagesContext,
            IContext expContext,
            IContext targetContext)
        {
            var elementInParent = targetContext.GetElementInParent(this.MemberValue, expContext);
            if (elementInParent.ElementCategory == ContextElementCategory.Field)
            {
                var field = elementInParent as Field;
                if ((int)(field.Access & AccessType.Global) != 0)
                {
                    throw new SyntaxException(string.Format("Cannot access global symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
                }
                if ((int)(field.Access & AccessType.Static) == 0)
                {
                    throw new SyntaxException(string.Format("Cannot access non-static symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
                }
                return this.GetExpressionsWithValue((elementInParent as Variable).Type);
            }
            throw new SyntaxException(string.Format("Cannot resolve symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
        }

        private Expression[] GetExpTypeAsVariable(PackagesContext packagesContext, IContext expContext, IContext targetContext)
        {
            var elementInParent = targetContext.GetElementInParent(this.MemberValue, expContext);
            if (elementInParent == null)
            {
                throw new SyntaxException(string.Format("Cannot access symbol '{0}' here.", this.MemberValue), this.Member.Line, this.Member.Column);
            }
            if (elementInParent.ElementCategory == ContextElementCategory.Variable || elementInParent.ElementCategory == ContextElementCategory.Parameter)
            {
                return this.GetExpressionsWithValue(elementInParent.Type);
            }
            if (elementInParent.ElementCategory == ContextElementCategory.Function)
            {
                var function = elementInParent as Function;
                if (function.ParentContext is Class)
                {
                    if ((int)(function.Access & AccessType.Global) != 0)
                    {
                        throw new SyntaxException(string.Format("Cannot access global symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
                    }
                }
                this.accessType = ":";
                this.accessType = ".";
                return this.GetExpressionsWithValue(elementInParent.Type);
            }
            if (elementInParent.ElementCategory == ContextElementCategory.Field)
            {
                var field = elementInParent as Field;
                if ((int)(field.Access & AccessType.Global) != 0)
                {
                    throw new SyntaxException(string.Format("Cannot access global symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
                }
                return this.GetExpressionsWithValue(elementInParent.Type);
            }
            throw new SyntaxException(string.Format("Cannot resolve symbol '{0}'", this.MemberValue), this.Member.Line, this.Member.Column);
        }

        public override Token GetPositionToken(object param = null)
        {
            return Member;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Objectexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(string.Format("{0}{1}",this.accessType, this.MemberValue));
        }
    }
}
