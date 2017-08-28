using System;
using Infrastructure.Security;

using StackExchange.Redis;

namespace MermaidLoft.Alchemy.QuickStartConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //MD5Util.QucikStart();
                //Console.WriteLine("******************");
                //Base64Util.QucikStart();

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
                IDatabase db = redis.GetDatabase();
                string value = "abcdefg";
                db.StringSet("mykey", value);
                string getValue = db.StringGet("mykey");
                Console.WriteLine("get value :{0}", getValue);
                for (int i = 0; i < 10; i++)
                {
                    db.ListRightPush("money", "money"+i);
                }
                var list  = db.ListRange("money");
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadLine();
        }
    }
}