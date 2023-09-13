namespace Backend.Application.Config
{
    public static class GlobalVariables
    {
        public static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStringLocal");
        public static string CacheConnectionString => "localhost";
    }
}
