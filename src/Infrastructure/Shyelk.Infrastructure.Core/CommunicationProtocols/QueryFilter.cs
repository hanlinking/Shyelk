using System;
using System.Collections.Generic;

namespace Shyelk.Infrastructure.Core.CommunicationProtocols
{
    /// <summary>
    /// 查询过滤器
    /// </summary>
    public class QueryFilter
    {
        ///<summary>
        ///过滤项目
        ///</summary>
        public virtual IDictionary<string, string> FilterItems { get; set; } = new Dictionary<string, string>();
        ///<summary>
        ///排序项目
        ///</summary>
        public virtual IDictionary<string, bool> OrderByItems { get; set; } = new Dictionary<string, bool>();
        ///<summary>
        ///页数(默认1)
        ///</summary>
        public virtual int Page { get; set; } = 1;
        /// <summary>
        /// 页面大小(默认10)
        /// </summary>
        public virtual int PageSize { get; set; } = 10;
    }
}