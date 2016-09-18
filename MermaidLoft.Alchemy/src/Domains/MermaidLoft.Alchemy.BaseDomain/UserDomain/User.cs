using System;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public string PasswordContent { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShopUserNameId { get; set; }
        public string ShopTitle { get; set; }
        public EnumUserType UserType { get; set; }
        public EnumUserStatus Status { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int Version { get; set; }
    }

    public enum EnumUserType
    {
        Admin = 0,
        Normal=1,
    }

    public enum EnumUserStatus
    {
        Normal = 0,
        Stop = 1,
    }
}
