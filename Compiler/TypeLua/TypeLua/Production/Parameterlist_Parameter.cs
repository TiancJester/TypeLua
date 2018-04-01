
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <parameter list> ::= <parameter>
    public class Parameterlist_Parameter : Parameter_list_basisproduction
    {
        public Token<Parameter_basisproduction> Parameter;

        public Parameterlist_Parameter(Project project, Class @class, GOLD.Token token0)
        {
            this.Parameter = new Token<Parameter_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Parameter_basisproduction)token0.Data};
            this.Children.Add(this.Parameter);
        }

        public override List<Token<Parameter_basisproduction>> GetParams(List<Token<Parameter_basisproduction>> @params)
        {
            @params.Add(this.Parameter);
            return @params;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Parameter.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        