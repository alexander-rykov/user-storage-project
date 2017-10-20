using UserStorageServices;

namespace UserStorageApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new Client(new UserStorageService());

            client.Run();
        }
    }
}
