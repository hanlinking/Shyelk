using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Shyelk.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Name nn = new Name() { ss = "测试" };
            Expression<Func<Name, Name>> ex = u => new Name { date=DateTime.Now };
            MemberInitExpression ss = ex.Body as MemberInitExpression;
            if (ss != null)
            {
                foreach (MemberAssignment item in ss.Bindings)
                {
                   object value=Evaluate(item.Expression);
                   var gg = item.Member;
                    var name = gg.Name;
                }
            }
            Type tt = typeof(Name);
            var mm = tt.GetMembers();
            var dd = tt.GetProperties();
            foreach (var item in dd)
            {

            }
            var ff = tt.GetFields();
        }
        static string  create(){
            return "cehisjhi";
        }
        class Name
        {
            public string ss { get; set; }
            public DateTime? date{get;set;}
            public int ff { get; set; }
        }
        static object Evaluate(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Constant:
                    return ((ConstantExpression)expr).Value;
                case ExpressionType.MemberAccess:
                    var me = (MemberExpression)expr;
                    object target = null;
                    object[] argu=null;
                    if (me.Expression!=null)
                    {
                        target=Evaluate(me.Expression);
                    }else
                    {
                       target= Activator.CreateInstance(me.Type);
                    }
                    switch (me.Member.MemberType)
                    {
                        case System.Reflection.MemberTypes.Field:
                            return ((FieldInfo)me.Member).GetValue(target);
                        case System.Reflection.MemberTypes.Property:
                            var val= ((PropertyInfo)me.Member).GetValue(target, argu);
                            return val;
                        default:
                            throw new NotSupportedException(me.Member.MemberType.ToString());
                    }
                case ExpressionType.Convert:
                    var convertexp=(System.Linq.Expressions.UnaryExpression)expr;
                    var result=  Evaluate(convertexp.Operand);
                    return result;
                default:
                    throw new NotSupportedException(expr.NodeType.ToString());
            }
        }
        
    }
}
