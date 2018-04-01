
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <class member list>
    public class Class_member_list_basisproduction : Production
    {
        public virtual List<Token<Class_member_basisproduction>> GetClassMembers(List<Token<Class_member_basisproduction>> classMembers)
        {
            return classMembers;
        }
    }
}
        