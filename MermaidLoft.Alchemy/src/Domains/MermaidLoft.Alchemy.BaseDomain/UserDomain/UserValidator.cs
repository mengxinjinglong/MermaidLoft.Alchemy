using System;
using Infrastructure.Utilities;
using System.Threading.Tasks;
using System.Linq;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserValidator
    {
        public async Task<bool> ValidatorAsync(UserQueryService querySerice,User user)
        {
            Ensure.NotNull(querySerice, "UserValidator.UserQueryService");
            Ensure.NotNull(user, "UserValidator.User");
            var list = await querySerice.FindUsersAsync(user.Account);
            if (list.Any())
            {
                throw new Exception("用户账号已存在，请重新输入！");
            }
            return true;
        }
    }
}
