using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.QuickWeb.Core
{
    public class AuthorizeValidator: IAuthorizeValidator
    {

        public async Task<bool> ValidateSelfOperationAsync(ClaimsPrincipal claimsPrincipal, string userId)
        {
            if (claimsPrincipal.HasClaim(item => item.Type == ClaimTypes.PrimarySid && item.Value != userId)
                    && !claimsPrincipal.Claims.Any(item => item.Type == ClaimTypes.Role && item.Value == UserType.Admin))
            {
                throw new Exception("您不是管理员，无权操作非当前用户信息！");
            }
            return true;
        }

        public async Task<bool> ValidateSelfAsync(ClaimsPrincipal claimsPrincipal, string userId)
        {
            return claimsPrincipal.HasClaim(item => item.Type == ClaimTypes.PrimarySid && item.Value == userId);
        }

        public async Task<bool> TryValidateAdminAsync(ClaimsPrincipal claimsPrincipal, string userId)
        {
            if (claimsPrincipal.HasClaim(item => item.Type == ClaimTypes.PrimarySid && item.Value != userId)
                    && !claimsPrincipal.Claims.Any(item => item.Type == ClaimTypes.Role && item.Value == UserType.Admin))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> TryValidateAdminAsync(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any(item => item.Type == ClaimTypes.Role && item.Value == UserType.Admin))
            {
                return true;
            }
            return false;
        }

    }

    public interface IAuthorizeValidator
    {
        Task<bool> ValidateSelfOperationAsync(ClaimsPrincipal claimsPrincipal, string userId);
        Task<bool> ValidateSelfAsync(ClaimsPrincipal claimsPrincipal, string userId);
        Task<bool> TryValidateAdminAsync(ClaimsPrincipal claimsPrincipal, string userId);
        Task<bool> TryValidateAdminAsync(ClaimsPrincipal claimsPrincipal);
    }
}
