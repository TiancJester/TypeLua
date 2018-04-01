
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <var lvalue> ::= <var dec>
    public class Varlvalue_Vardec : Var_lvalue_basisproduction
    {
        public Token<Var_dec_basisproduction> Vardec;

        public Varlvalue_Vardec(Project project, Class @class, GOLD.Token token0)
        {
            this.Vardec = new Token<Var_dec_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_dec_basisproduction)token0.Data};
            this.Children.Add(this.Vardec);
        }

        public override void ContextVerify(IContext context)
        {
            this.Vardec.Symbol.ContextVerify(context);
        }

        public override Type GetLValueType(IContext context)
        {
            return this.Vardec.Symbol.GetVarType(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Vardec.Symbol.GenerateLua(c, root, builder, depth);
        }

        public override Token GetPositionToken(object param = null)
        {
            return Vardec.Symbol.GetPositionToken(param);
        }

        public override bool IsVariableDeclarer { get
        {
            return true;
        } }
    }
}
        