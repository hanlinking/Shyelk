using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Shyelk.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Name nn=new Name(){ss="测试"};
            Expression<Func<Name,Name>> ex=u=>nn;
            MemberInitExpression ss= ex.Body as MemberInitExpression;
            if (ss!=null)
            {
            foreach (MemberAssignment item in ss.Bindings)
            {               
              ConstantExpression exp=item.Expression as ConstantExpression;
               string value= exp.Value as string;
               var gg= item.Member;
               var name= gg.Name;
            }            
            }
           Type tt= typeof(Name);
           var mm= tt.GetMembers();
           var dd=tt.GetProperties();
           foreach (var item in dd)
           {
                
           }
           var ff=tt.GetFields();
        }
        class Name
        {
            public string ss{get;set;}
            public int ff{get;set;}
        }
    }
}
