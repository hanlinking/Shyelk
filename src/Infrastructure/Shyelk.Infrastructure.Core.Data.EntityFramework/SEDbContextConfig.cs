namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    internal class SEDbContextConfig
    {
        public SEDbContextConfig(string connection,DatabaseType type,string entityMapper)
        {
            Connection=connection;
            Type=type;
            EntityMapper=entityMapper;
        }
        public string Connection{get;set;}
        public DatabaseType Type{get;set;}
        public string EntityMapper{get;set;}
    }
}