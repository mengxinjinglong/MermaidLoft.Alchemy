using Dapper;
using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ArticleDomain.Implementation
{
    public class ArticleQueryService : IArticleQueryService
    {
        public async Task<IEnumerable<Article>> FindForPageAsync(string title, string type, int pageIndex, int pageSize)
        {
            return await GetAsync<Article>(title, type,pageIndex,pageSize);

            //using (var connection = ConnectionConfig.Instance.GetConnection())
            //{
            //    var conditionDate = new DynamicParameters();
            //    string conditionContent = string.Empty;
            //    if (string.IsNullOrEmpty(title))
            //    {
            //        conditionContent += "and Title like @Title";
            //        conditionDate.Add("Name", "%" + title + "%");
            //    }
            //    if (string.IsNullOrEmpty(type))
            //    {
            //        conditionContent += "and Type = @Type";
            //        conditionDate.Add("Type", type);
            //    }
            //    var sql = string.Format("SELECT * FROM {0} where 1=1 {1} limit {2},{3}",
            //        ConfigSettings.ArticleTable,
            //        conditionContent,
            //        (pageIndex - 1) * pageSize, pageSize);
            //    return await connection.QueryAsync<Article>(sql, conditionDate);
            //}
        }

        public async Task<Article> FindSingleAsync(object condition)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return (await connection.QueryListAsync<Article>(condition, ConfigSettings.ArticleTable)).SingleOrDefault();
            }
        }

        public async Task<int> GetCountAsync(string title, string type)
        {
            return (await GetAsync<int>(title,type)).Single();
        }

        private async Task<IEnumerable<T>> GetAsync<T>(string title, string type, 
            int pageIndex=0, int pageSize=0) //where T : class
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                var conditionDate = new DynamicParameters();
                string conditionContent = string.Empty;
                if (!string.IsNullOrEmpty(title))
                {
                    conditionContent += " and Title like @Title";
                    conditionDate.Add("Title", "%" + title + "%");
                }
                if (!string.IsNullOrEmpty(type))
                {
                    conditionContent += " and Type = @Type";
                    conditionDate.Add("Type", type);
                }
                var sql = string.Empty;
                if (pageIndex == 0 && pageSize == 0)
                {
                    sql = string.Format("SELECT count(*) FROM {0} where 1=1 {1} ",
                    ConfigSettings.ArticleTable,
                    conditionContent);
                    return (await connection.QueryAsync<T>(sql, conditionDate));
                }
                else {
                    sql = string.Format("SELECT * FROM {0} where 1=1 {1}  order by AddTime desc limit {2},{3}",
                    ConfigSettings.ArticleTable,
                    conditionContent,
                    (pageIndex - 1) * pageSize, pageSize);
                    return await connection.QueryAsync<T>(sql, conditionDate);
                }
                return null;
            }
        }
    }
}
