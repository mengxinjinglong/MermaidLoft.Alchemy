using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.BaseResourceDomain.Implementation
{
    public class BaseResourceService : IBaseResourceService
    {
        public async Task<bool> AddAsync(BaseResource resource)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.InsertAsync(resource, ConfigSettings.BaseResourceTable) > 0;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.DeleteAsync(new { Id = id }, ConfigSettings.BaseResourceTable) > 0;
            }
        }

        public async Task<bool> UpdateAsync(BaseResource resource)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.UpdateAsync(resource, new { Id = resource.Id }, ConfigSettings.BaseResourceTable) > 0;
            }
        }
    }
}
