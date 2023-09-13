namespace Backend.Application.Config
{
    public static class GlobalVariables
    {
        public static string ConnectionString => "Data Source=localhost;Initial Catalog=Medalhas;Integrated Security=True;Connect Timeout=900;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string CacheConnectionString => "localhost";
    }
}
