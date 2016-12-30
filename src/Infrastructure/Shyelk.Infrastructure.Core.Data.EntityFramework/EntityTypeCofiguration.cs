using Microsoft.EntityFrameworkCore;

namespace Shyelk.Infrastructure.Core.Data.EntityFramework
{
    /// <summary>
    /// 实体映射配置
    /// </summary>
    public abstract class EntityTypeCofiguration
    {
        /// <summary>
        /// 实体配置
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"></see></param>
        public abstract void ModelConfigurate(ModelBuilder builder);
    }
}