
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <class block> ::= <modifier list> class Identifier <class base opt> <class member list>
    public class Classblock_Modifierlist_Class_Identifier_Classbaseopt_Classmemberlist : Class_block_basisproduction
    {
        public Token<Modifier_list_basisproduction> Modifierlist;
        public Token<string> Class;
        public Token<string> Identifier;
        public Token<Class_base_opt_basisproduction> Classbaseopt;
        public Token<Class_member_list_basisproduction> Classmemberlist;

        public Classblock_Modifierlist_Class_Identifier_Classbaseopt_Classmemberlist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4)
        {
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_list_basisproduction)token0.Data};
            this.Children.Add(this.Modifierlist);
            this.Class = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Class);
            this.Identifier = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Identifier);
            this.Classbaseopt = new Token<Class_base_opt_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Class_base_opt_basisproduction)token3.Data};
            this.Children.Add(this.Classbaseopt);
            this.Classmemberlist = new Token<Class_member_list_basisproduction>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Class_member_list_basisproduction)token4.Data};
            this.Children.Add(this.Classmemberlist);

            this.SyntaxVerify();
            this.BuildClass(project, @class);
        }

        public override bool SyntaxVerify()
        {
            //the class modifier
            var symbolIndices = this.Modifierlist.Symbol.GetModifiers(new List<Token<TypeLuaParser.SymbolIndex>>());
            Modifier_list_basisproduction.ModifierVerify(symbolIndices, TypeLuaParser.SymbolIndex.Public);

            //members
            var classMembers = this.Classmemberlist.Symbol.GetClassMembers(new List<Token<Class_member_basisproduction>>());
            int staticCtorCount = 0;
            var type = typeof(Classmember_Classctor);
            foreach (var classMember in classMembers)
            {
                if (type.IsAssignableFrom(classMember.Symbol.GetType()) && (((Classmember_Classctor)classMember.Symbol).IsStaticCtor()))
                {
                    if (++staticCtorCount > 1)
                    {
                        throw new SyntaxException("static constructor is redundant", classMember.Line, classMember.Column);
                    }
                }
            }

            return base.SyntaxVerify();
        }



        public override bool BuildClass(Project project, Class @class)
        {
            @class.ClassName = this.Identifier.Symbol;
//            @class.BaseClassName = this.Classbaseopt.Symbol.GetClassName();
            try
            {
                project.AddClass(@class);
            }
            catch (Exception e)
            {
                throw new SyntaxException(e.Message, this.Identifier.Line, this.Identifier.Column);
            }
            
            return base.BuildClass(project, @class);
        }
    }
}
        