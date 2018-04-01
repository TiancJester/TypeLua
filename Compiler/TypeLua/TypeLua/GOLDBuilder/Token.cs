// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>03/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.GOLDBuilder
{
    public class Token<T> : Token
    {
        public T Symbol;
        
        public Token()
        {
            
        }

        public Token(T symbol,int line, int column)
        {
            this.Symbol = symbol;
            this.Line = line;
            this.Column = column;
        }

        public override object GetData()
        {
            return this.Symbol;
        }
    }

    public abstract class Token
    {
        public int Line;

        public int Column;

        public abstract object GetData();
    }
}