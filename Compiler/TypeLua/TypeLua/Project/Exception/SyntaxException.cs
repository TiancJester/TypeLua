// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>05/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Exception
{
    using TypeLua.GOLDBuilder;

    public class SyntaxException : FileParseException
    {
        public int Line;

        public int Column;

        public SyntaxException(string message, int line, int column) : base(message)
        {
            this.Line = line;
            this.Column = column;
        }

        public SyntaxException(string message, Token positionToken) : base(message)
        {
            this.Line = positionToken.Line;
            this.Column = positionToken.Column;
        }

        public SyntaxException(string message, Production positionProduction) : base(message)
        {
            var positionToken = positionProduction.GetPositionToken(null);
            this.Line = positionToken.Line;
            this.Column = positionToken.Column;
        }

        public override string Message
        {
            get
            {
                return string.Format("{0} at line {1} column {2}", base.Message, this.Line + 1, this.Column + 1);
            }
        }
    }
}