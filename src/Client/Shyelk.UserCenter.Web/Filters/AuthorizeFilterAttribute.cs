using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Shyelk.UserCenter.Web.Filters
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class AuthorizeFilterAttribute : System.Attribute,IResourceFilter
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string positionalString;
        
        // This is a positional argument
        public AuthorizeFilterAttribute (string positionalString)
        {
            this.positionalString = positionalString;
            
            // TODO: Implement code here
            throw new System.NotImplementedException();
        }
        
        public string PositionalString
        {
            get { return positionalString; }
        }
        
        // This is a named argument
        public int NamedInt { get; set; }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            StringValues token=string.Empty;
            if (context.HttpContext.Request.Headers.TryGetValue("token",out token))
            {
                
            } 
        }
    }
}