using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;

namespace MermaidLoft.Alchemy.BaseDomain.BaseResourceDomain.Implementation
{
    public class BaseResourceQueryService : IBaseResourceQueryService
    {
        public async Task<IEnumerable<BaseResource>> FindForPageAsync(string name, string type, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                var conditionDate = new DynamicParameters();
                string conditionContent = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    conditionContent+=" and Name like @Name";
                    conditionDate.Add("Name","%"+name+ "%");
                }
                if (!string.IsNullOrEmpty(type))
                {
                    conditionContent += " and Type = @Type";
                    conditionDate.Add("Type", type);
                }
                var sql = string.Format("SELECT * FROM {0} where 1=1 {1} limit {2},{3}",
                    ConfigSettings.BaseResourceTable,
                    conditionContent,
                    (pageIndex - 1) * pageSize, pageSize);
                return await connection.QueryAsync<BaseResource>(sql,conditionDate);
            }
        }

        public async Task<BaseResource> FindSingleAsync(object condition)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return (await connection.QueryListAsync<BaseResource>(condition, ConfigSettings.BaseResourceTable)).SingleOrDefault();
            }
        }

        public async Task<int> GetCountAsync(string name, string type)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.GetCountAsync(
                    string.IsNullOrEmpty(name) ?
                        null : new { UserName = name },
                    ConfigSettings.BaseResourceTable);
            }
        }
    }
}
