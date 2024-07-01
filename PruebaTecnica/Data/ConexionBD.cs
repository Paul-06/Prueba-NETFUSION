namespace PruebaTecnica.Data
{
    public class ConexionBD
    {
        private string _dbConn = string.Empty;

        public ConexionBD()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _dbConn = builder.GetConnectionString("DefaultConnection")!;
        }

        public string GetDbConn()
        {
            return _dbConn;
        }
    }
}
