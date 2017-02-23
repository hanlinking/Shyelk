namespace Shyelk.Infrastructure.Core.Data.EntityFramework.Exceptions
{
    public class SEDbOperationException:System.Exception
    {
        public SEDbOperationException() { }
        public SEDbOperationException( string message ) : base( message ) { }
        public SEDbOperationException( string message, System.Exception inner ) : base( message, inner ) { }
    }
}