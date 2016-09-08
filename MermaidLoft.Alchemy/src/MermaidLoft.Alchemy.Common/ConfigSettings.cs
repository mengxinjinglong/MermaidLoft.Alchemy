namespace MermaidLoft.Alchemy.Common
{
    public class ConfigSettings
    {
        public static string UserTable { get; set; }

        public static void Initialize()
        {
            UserTable = "User";
        }
    }
}
