
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class member list> ::= <class member list> <class member>
    public class Classmemberlist_Classmemberlist_Classmember : Class_member_list_basisproduction
    {
        public Token<Class_member_list_basisproduction> Classmemberlist;
        public Token<Class_member_basisproduction> Classmember;

        public Classmemberlist_Classmemberlist_Classmember(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Classmemberlist = new Token<Class_member_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_member_list_basisproduction)token0.Data};
            this.Children.Add(this.Classmemberlist);
            this.Classmember = new Token<Class_member_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Class_member_basisproduction)token1.Data};
            this.Children.Add(this.Classmember);
        }

        public override List<Token<Class_member_basisproduction>> GetClassMembers(List<Token<Class_member_basisproduction>> classMembers)
        {
            if (classMembers == null)
            {
                classMembers = new List<Token<Class_member_basisproduction>>();
            }
            this.Classmemberlist.Symbol.GetClassMembers(classMembers);
            classMembers.Add(this.Classmember);
            return classMembers;
        }
    }
}
        