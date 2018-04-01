
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class field assignment> ::= 
    public class Classfieldassignment_ : Class_field_assignment_basisproduction
    {


        public Classfieldassignment_(Project project, Class @class)
        {

        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.AppendLine("nil");
        }
    }
}
        