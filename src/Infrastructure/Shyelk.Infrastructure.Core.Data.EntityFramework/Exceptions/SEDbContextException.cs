namespace Shyelk.Infrastructure.Core.Data.EntityFramework.Exceptions
{
    public class SEDbContextException : System.Exception
    {
        public SEDbContextException() { }
        public SEDbContextException( string message ) : base( message ) { }
        public SEDbContextException( string message, System.Exception inner ) : base( message, inner ) { }
    }
}