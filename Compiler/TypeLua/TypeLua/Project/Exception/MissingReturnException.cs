// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>23/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Exception
{
    public class MissingReturnException : FileParseException
    {
        public MissingReturnException(string message)
            : base(message)
        {
        }
    }
}