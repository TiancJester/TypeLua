
namespace TypeLua.Production
{
    using System;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <class base opt> ::= extends Identifier
    public class Classbaseopt_Extends_Identifier : Class_base_opt_basisproduction
    {
        public Token<string> Extends;
        public Token<string> Identifier;

        public Classbaseopt_Extends_Identifier(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Extends = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Extends);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
            this.BuildClass(project, @class);
        }

        public override bool BuildClass(Project project, Class @class)
        {
            @class.BaseClassName = this.Identifier.Symbol;
            @class.BaseClassProduction = this;
            return base.BuildClass(project, @class);
        }

        public override void ContextVerify(IContext context)
        {
            var baseClass = context.ClassContext.Packages.GetClass(this.Identifier.Symbol);
            if (baseClass == null)
            {
                throw new SyntaxException("Base class not found.",this.Identifier.Line,this.Identifier.Column);
            }
            context.ClassContext.BaseClass = baseClass;
        }

        public override Token GetPositionToken(object param = null)
        {
            return Identifier;
        }
    }
}
        