
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <parameter list> ::= <parameter list> ',' <parameter>
    public class Parameterlist_Parameterlist_Comma_Parameter : Parameter_list_basisproduction
    {
        public Token<Parameter_list_basisproduction> Parameterlist;
        public Token<string> Comma;
        public Token<Parameter_basisproduction> Parameter;

        public Parameterlist_Parameterlist_Comma_Parameter(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Parameterlist = new Token<Parameter_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Parameter_list_basisproduction)token0.Data};
            this.Children.Add(this.Parameterlist);
            this.Comma = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Comma);
            this.Parameter = new Token<Parameter_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Parameter_basisproduction)token2.Data};
            this.Children.Add(this.Parameter);
        }

        public override List<Token<Parameter_basisproduction>> GetParams(List<Token<Parameter_basisproduction>> @params)
        {
            if (@params == null)
            {
                @params = new List<Token<Parameter_basisproduction>>();
            }
            this.Parameterlist.Symbol.GetParams(@params);
            @params.Add(this.Parameter);
            return @params;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Parameterlist.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(", ");
            Parameter.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        