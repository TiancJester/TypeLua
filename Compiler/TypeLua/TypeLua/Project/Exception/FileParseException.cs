// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>05/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Exception
{
    using System;

    public class FileParseException : Exception
    {
        public string FileName;

        public FileParseException(string message):base(message)
        {
            
        }



        public override string Message
        {
            get
            {
                if (FileName != null)
                {
                    return string.Format("[{0}]:{1}", this.FileName,base.Message);
                }
                return base.Message;
            }
        }

//        protected virtual string Info { get
//        {
//            return string.Empty;
//        } }
    }
}