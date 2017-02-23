using System;
using System.Threading.Tasks;
using Shyelk.UserCenter.IService;
namespace Shyelk.UserCenter.Service
{
    public class MessageService : IMessageService
    {
        public Task<bool> SendEmailAsync(string address, string content, string subject, params string[] attach)
        {
            throw new NotImplementedException();
        }
    }
}