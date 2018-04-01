
namespace TypeLua.Production
{
    using System;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    using Type = TypeLua.Project.Types.Type;

    // <var exp> ::= <object exp> '[' <exp> ']'
    public class Varexp_Objectexp_Lbracket_Exp_Rbracket : Var_exp_basisproduction
    {
        public Token<Object_exp_basisproduction> Objectexp;

        public Token<string> Lbracket;

        public Token<Exp_basisproduction> Exp;

        public Token<string> Rbracket;

        public Varexp_Objectexp_Lbracket_Exp_Rbracket(
            Project project,
            Class @class,
            GOLD.Token token0,
            GOLD.Token token1,
            GOLD.Token token2,
            GOLD.Token token3)
        {
            this.Objectexp = new Token<Object_exp_basisproduction>()
                                 {
                                     Column = token0.Position().Column,
                                     Line = token0.Position().Line,
                                     Symbol = (Object_exp_basisproduction)token0.Data
                                 };
            this.Children.Add(this.Objectexp);
            this.Lbracket = new Token<string>()
                                {
                                    Column = token1.Position().Column,
                                    Line = token1.Position().Line,
                                    Symbol = (string)token1.Data
                                };
            this.Children.Add(this.Lbracket);
            this.Exp = new Token<Exp_basisproduction>()
                           {
                               Column = token2.Position().Column,
                               Line = token2.Position().Line,
                               Symbol = (Exp_basisproduction)token2.Data
                           };
            this.Children.Add(this.Exp);
            this.Rbracket = new Token<string>()
                                {
                                    Column = token3.Position().Column,
                                    Line = token3.Position().Line,
                                    Symbol = (string)token3.Data
                                };
            this.Children.Add(this.Rbracket);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var prefixList = this.Objectexp.Symbol.GetExpressions(packagesContext, expContext);
            if (prefixList.Length != 1)
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }
            var prefix = prefixList[0];
            if (prefix.Type == Type.Table || prefix.Type == Type.Any)
            {
                return new[] { new Expression() { Classify = ExpressionType.Value, Type = Type.Any } };
            }
            if (prefix.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }
            if (!(prefix.Type is GenericityType))
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }

            var prefixType = prefix.Type as GenericityType;
            var indexExpValue = this.Exp.Symbol.GetExpressions(packagesContext, expContext);
            if (indexExpValue.Length != 1)
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }

            if (prefixType.Name == Type.ListTable.Name && prefixType.PackageName == Type.ListTable.PackageName)
            {
                return this.GetExpTypeWithListTable(packagesContext, expContext, prefixType, indexExpValue[0]);
            }
            if (prefixType.Name == Type.HashTable.Name && prefixType.PackageName == Type.HashTable.PackageName)
            {
                return this.GetExpTypeWithHashTable(packagesContext, expContext, prefixType, indexExpValue[0]);
            }
            throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
        }

        public Expression[] GetExpTypeWithListTable(
            PackagesContext packagesContext,
            IContext expContext,
            GenericityType prefixType,
            Expression indexValue)
        {
            if (indexValue.Type != Type.Number || indexValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }
            return this.GetExpressionsWithValue(prefixType.FirstGroupGenericTypeArguments[0]);
        }

        public Expression[] GetExpTypeWithHashTable(
            PackagesContext packagesContext,
            IContext expContext,
            GenericityType prefixType,
            Expression indexValue)
        {
            if (indexValue.Type != prefixType.FirstGroupGenericTypeArguments[0] || indexValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Cannot resolve symbol here.", this.Lbracket.Line, this.Lbracket.Column);
            }
            return this.GetExpressionsWithValue(prefixType.FirstGroupGenericTypeArguments[1]);
        }

        public override Token GetPositionToken(object param = null)
        {
            return Lbracket;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Objectexp.Symbol.GenerateLua(c,root,builder,depth);
            builder.Append("[");
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append("]");
        }
    }
}