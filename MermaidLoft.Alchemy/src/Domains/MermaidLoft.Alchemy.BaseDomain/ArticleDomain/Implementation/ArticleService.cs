using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ArticleDomain.Implementation
{
    public class ArticleService : IArticleService
    {
        public async Task<bool> AddAsync(Article article)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.InsertAsync(article, ConfigSettings.ArticleTable) > 0;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.DeleteAsync(new { Id = id }, ConfigSettings.ArticleTable) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Article article)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.UpdateAsync(article, new { Id = article.Id }, ConfigSettings.ArticleTable) > 0;
            }
        }
    }
}
