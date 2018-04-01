// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>13/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Exception
{
    public class UnknowTypeException : FileParseException
    {
        public UnknowTypeException(string message)
            : base(message)
        {
        }
    }
}