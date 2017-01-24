using System.Collections.Generic;
namespace Shyelk.Infrastructure.Core.CommunicationProtocols
{
    /// <summary>
    /// 通用查询返回
    /// </summary>
    public class QueryResult<TObject>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<TObject> Data { get; set; } = new List<TObject>();
        ///<summary>
        ///查询结果总数
        ///</summary>
        public int TotalCount { get; set; }
    }
}