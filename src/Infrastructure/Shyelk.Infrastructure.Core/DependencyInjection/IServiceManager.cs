using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shyelk.Infrastructure.Core.DependencyInjection
{
    ///<summary>
    /// 服务管理接口,用于服务注册
    ///</summary>   
    public interface IServiceManager
    {
        IServiceCollection RegisterService(IServiceCollection services);
    }
}
