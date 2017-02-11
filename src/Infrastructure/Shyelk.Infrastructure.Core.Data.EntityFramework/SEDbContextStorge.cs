namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    internal class SEDbContextStorge
    {
        public SEDbContextStorge(string name,SEDbContext context)
        {
            Name=name;
            Context=context;
        }
        public string Name{get;set;}
        public SEDbContext Context{get;set;}
    }
}