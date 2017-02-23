namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    internal class SEDbContextConfig
    {
        public SEDbContextConfig(string connection,DatabaseType type, System.Reflection.Assembly[] entityMapper)
        {
            Connection=connection;
            Type=type;
            EntityMapper=entityMapper;
        }
        public string Connection{get;set;}
        public DatabaseType Type{get;set;}
        public System.Reflection.Assembly[] EntityMapper{get;set;}
    }
}