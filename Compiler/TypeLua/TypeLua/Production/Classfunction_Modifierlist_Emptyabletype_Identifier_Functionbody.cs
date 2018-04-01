
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <class function> ::= <modifier list> <emptyable type> Identifier <function body>
    public class Classfunction_Modifierlist_Emptyabletype_Identifier_Functionbody : Class_function_basisproduction
    {
        public Token<Modifier_list_basisproduction> Modifierlist;
        public Token<Emptyable_type_basisproduction> Emptyabletype;
        public Token<string> Identifier;
        public Token<Function_body_basisproduction> Functionbody;

        public Classfunction_Modifierlist_Emptyabletype_Identifier_Functionbody(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3)
        {
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_list_basisproduction)token0.Data};
            this.Children.Add(this.Modifierlist);
            this.Emptyabletype = new Token<Emptyable_type_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Emptyable_type_basisproduction)token1.Data};
            this.Children.Add(this.Emptyabletype);
            this.Identifier = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Identifier);
            this.Functionbody = new Token<Function_body_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Function_body_basisproduction)token3.Data};
            this.Children.Add(this.Functionbody);

            this.SyntaxVerify();
            this.BuildClass(project, @class);
        }

        public override bool SyntaxVerify()
        {
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            Modifier_list_basisproduction.ModifierVerify(symbolIndices, TypeLuaParser.SymbolIndex.Global, TypeLuaParser.SymbolIndex.Private, TypeLuaParser.SymbolIndex.Protected, TypeLuaParser.SymbolIndex.Public, TypeLuaParser.SymbolIndex.Static);
            return base.SyntaxVerify();
        }

        public override bool BuildClass(Project project, Class @class)
        {
            if (@class.Methods.ContainsElement(this.Identifier.Symbol))
            {
                throw new SyntaxException("Methods with the same name is already exist", this.Identifier.Line, this.Identifier.Column);
            }
            var parameters = this.Functionbody.Symbol.GetParameters(null);
            var returnValueTypes = this.Emptyabletype.Symbol.GetTLType(null);

            var function = new Function(this.Identifier.Symbol,
                this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>()).GetAccessType(),
                @class,
                @class,
                returnValueTypes,
                parameters,
                this.Functionbody.Symbol);
            @class.Methods.AddElement(function);

            return true;
        }
    }
}
        