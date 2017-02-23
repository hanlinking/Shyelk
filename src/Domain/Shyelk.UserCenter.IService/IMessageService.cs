using System;
using System.Security.Claims;
using Shyelk.UserCenter.Models;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.IService
{
    public interface IMessageService:Shyelk.Infrastructure.Core.Service.IService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="address">邮箱地址</param>
        /// <param name="content">邮件内容</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="attach">附件(附件所在地址)</param>
        /// <returns>发送是否成功</returns>
        Task<bool> SendEmailAsync(string address,string content,string subject,params string[] attach);
    }
}