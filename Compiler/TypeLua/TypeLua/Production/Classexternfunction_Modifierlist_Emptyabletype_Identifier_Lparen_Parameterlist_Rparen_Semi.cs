
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <class extern function> ::= <modifier list> <emptyable type> Identifier '(' <parameter list> ')' ';'
    public class Classexternfunction_Modifierlist_Emptyabletype_Identifier_Lparen_Parameterlist_Rparen_Semi : Class_extern_function_basisproduction
    {
        public Token<Modifier_list_basisproduction> Modifierlist;
        public Token<Emptyable_type_basisproduction> Emptyabletype;
        public Token<string> Identifier;
        public Token<string> Lparen;
        public Token<Parameter_list_basisproduction> Parameterlist;
        public Token<string> Rparen;
        public Token<string> Semi;

        public Classexternfunction_Modifierlist_Emptyabletype_Identifier_Lparen_Parameterlist_Rparen_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5,GOLD.Token token6)
        {
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_list_basisproduction)token0.Data};
            this.Children.Add(this.Modifierlist);
            this.Emptyabletype = new Token<Emptyable_type_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Emptyable_type_basisproduction)token1.Data};
            this.Children.Add(this.Emptyabletype);
            this.Identifier = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Identifier);
            this.Lparen = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Lparen);
            this.Parameterlist = new Token<Parameter_list_basisproduction>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Parameter_list_basisproduction)token4.Data};
            this.Children.Add(this.Parameterlist);
            this.Rparen = new Token<string>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (string)token5.Data};
            this.Children.Add(this.Rparen);
            this.Semi = new Token<string>() {Column = token6.Position().Column,Line = token6 .Position().Line,Symbol = (string)token6.Data};
            this.Children.Add(this.Semi);

            this.SyntaxVerify();
            this.BuildClass(project, @class);
        }

        public override bool SyntaxVerify()
        {
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            Modifier_list_basisproduction.ModifierVerify(symbolIndices, TypeLuaParser.SymbolIndex.Extern, TypeLuaParser.SymbolIndex.Global, TypeLuaParser.SymbolIndex.Private, TypeLuaParser.SymbolIndex.Protected, TypeLuaParser.SymbolIndex.Public, TypeLuaParser.SymbolIndex.Static);
            return base.SyntaxVerify();
        }

        public override bool BuildClass(Project project, Class @class)
        {
            if (@class.Methods.ContainsElement(this.Identifier.Symbol))
            {
                throw new SyntaxException("Methods with the same name is already exist", this.Identifier.Line, this.Identifier.Column);
            }
            var @params = this.Parameterlist.Symbol.GetParams(new List<Token<Parameter_basisproduction>>());
            Parameter[] parameters = new Parameter[@params.Count];
            for (int i = 0; i < @params.Count; i++)
            {
                var token = @params[i];
                parameters[i] = token.Symbol.GetParameter(null);
            }

            var returnValueTypes = this.Emptyabletype.Symbol.GetTLType(null);

            var function = new Function(this.Identifier.Symbol, 
                this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>()).GetAccessType(),
                @class,
                @class,
                returnValueTypes,
                parameters,
                null);
            @class.Methods.AddElement(function);

            return true;
        }
    }
}
        