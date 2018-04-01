
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <class ctor> ::= <modifier list> Identifier <function body>
    public class Classctor_Modifierlist_Identifier_Functionbody : Class_ctor_basisproduction
    {
        public Token<Modifier_list_basisproduction> Modifierlist;
        public Token<string> Identifier;
        public Token<Function_body_basisproduction> Functionbody;


        public Classctor_Modifierlist_Identifier_Functionbody(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_list_basisproduction)token0.Data};
            this.Children.Add(this.Modifierlist);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
            this.Functionbody = new Token<Function_body_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Function_body_basisproduction)token2.Data};
            this.Children.Add(this.Functionbody);

            this.SyntaxVerify();
            this.BuildClass(project, @class);
        }

        public override bool SyntaxVerify()
        {
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            Modifier_list_basisproduction.ModifierVerify(symbolIndices, TypeLuaParser.SymbolIndex.Public, TypeLuaParser.SymbolIndex.Static);
            return base.SyntaxVerify();
        }

        public override bool IsStaticCtor()
        {
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            foreach (var symbolIndex in symbolIndices)
            {
                if (symbolIndex.Symbol == TypeLuaParser.SymbolIndex.Static)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool BuildClass(Project project, Class @class)
        {
            if (this.IsStaticCtor())
            {
                var parameters = this.Functionbody.Symbol.GetParameters(null);
                if (parameters != null && parameters.Length > 0)
                {
                    throw new SyntaxException("Static constructor must be Parameterless",this.Functionbody.Line,this.Functionbody.Column);
                }
                @class.StaticConstructor = new Function(this.Identifier.Symbol,AccessType.Public, @class, @class, null,null, this.Functionbody.Symbol);
            }
            else
            {
                @class.ClassConstructor = new Function(this.Identifier.Symbol, AccessType.Any, @class, @class, null, this.Functionbody.Symbol.GetParameters(null), this.Functionbody.Symbol);
            }
            return true;
        }
    }
}
        