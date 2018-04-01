
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using GOLD;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    using Token = TypeLua.GOLDBuilder.Token;

    // <class field> ::= <modifier list> <emptyable type> Identifier <class field assignment> ';'
    public class Classfield_Modifierlist_Emptyabletype_Identifier_Classfieldassignment_Semi : Class_field_basisproduction
    {
        public Token<Modifier_list_basisproduction> Modifierlist;
        public Token<Emptyable_type_basisproduction> Emptyabletype;
        public Token<string> Identifier;
        public Token<Class_field_assignment_basisproduction> Classfieldassignment;
        public Token<string> Semi;

        public Classfield_Modifierlist_Emptyabletype_Identifier_Classfieldassignment_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4)
        {
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_list_basisproduction)token0.Data};
            this.Children.Add(this.Modifierlist);
            this.Emptyabletype = new Token<Emptyable_type_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Emptyable_type_basisproduction)token1.Data};
            this.Children.Add(this.Emptyabletype);
            this.Identifier = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Identifier);
            this.Classfieldassignment = new Token<Class_field_assignment_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Class_field_assignment_basisproduction)token3.Data};
            this.Children.Add(this.Classfieldassignment);
            this.Semi = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.Semi);

            this.SyntaxVerify();
            this.BuildClass(project, @class);
        }

        public override bool SyntaxVerify()
        {
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            Modifier_list_basisproduction.ModifierVerify(symbolIndices, TypeLuaParser.SymbolIndex.Global, TypeLuaParser.SymbolIndex.Private, TypeLuaParser.SymbolIndex.Protected, TypeLuaParser.SymbolIndex.Public, TypeLuaParser.SymbolIndex.Static, TypeLuaParser.SymbolIndex.Extern);

            if (this.Emptyabletype.Symbol.IsEmptyType())
            {
                throw new SyntaxException("field type cannot be 'void'", this.Emptyabletype.Line, this.Emptyabletype.Column);
            }

            return base.SyntaxVerify();
        }

        public override bool BuildClass(Project project, Class @class)
        {
            if (@class.Fields.ContainsElement(this.Identifier.Symbol))
            {
                throw new SyntaxException("Field with same name is already exist",this.Identifier.Line,this.Identifier.Column);
            }
            var tlTypes = this.Emptyabletype.Symbol.GetTLType(null);
            if (tlTypes.Length > 1)
            {
                throw new SyntaxException("More then one field type", this.Identifier.Line, this.Identifier.Column);
            }
            if (tlTypes.Length == 0)
            {
                throw new SyntaxException("No field type", this.Identifier.Line, this.Identifier.Column);
            }
            if (tlTypes[0].Name == "void")
            {
                throw new SyntaxException("Field type cannot be 'void'", this.Identifier.Line, this.Identifier.Column);
            }
            var variable = new Field(this.Identifier.Symbol, this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>()).GetAccessType(), this, @class);
            variable.Type = tlTypes[0];
            @class.Fields.AddElement(variable);
            return true;
        }

        public override void ContextVerify(IContext context)
        {
            this.Classfieldassignment.Symbol.ContextVerify(context);
            var expType = this.Classfieldassignment.Symbol.GetExpression(context);
            if (expType != null)
            {
                var tlType = this.Emptyabletype.Symbol.GetTLType(context.ClassContext.Packages)[0];
                if (!tlType.IsAssignableFrom(expType.Type))
                {
                    throw new SyntaxException(string.Format("Cannot convert from '{0}' to '{1}'.", expType.Type.FullName, tlType.FullName), this.Classfieldassignment);
                }
            }
        }

        public override Token GetPositionToken(object param)
        {
            return this.Identifier;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            var accessType = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>()).GetAccessType();

            if (depth == 0)
            {
                //class block
                if ((accessType & AccessType.Global) > 0)
                {
                    builder.Append(string.Format("{0} = nil", this.Identifier.Symbol));
                }
                else
                {
                    builder.Append(string.Format("{0}.{1} = nil", c.ClassName, this.Identifier.Symbol));
                }
            }
            else if (depth == 1)
            {
                //constructor block
                if ((accessType & AccessType.Global) > 0)
                {
                    builder.Append(string.Format("    {0} = ", this.Identifier.Symbol));
                }
                else if ((accessType & AccessType.Static) > 0)
                {
                    builder.Append(string.Format("    {0}.{1} = ", c.ClassName, this.Identifier.Symbol));
                }
                else
                {
                    builder.Append(string.Format("    self.{0} = ", this.Identifier.Symbol));
                }
                this.Classfieldassignment.Symbol.GenerateLua(c, root, builder, depth);
            } 
        }
    }
}
        