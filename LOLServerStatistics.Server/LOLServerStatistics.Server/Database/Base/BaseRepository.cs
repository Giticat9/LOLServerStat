using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace LOLServerStatistics.Server.Database.Base
{
    public class BaseRepository
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection UsingConnection(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            var cs = _configuration.GetConnectionString(connectionStringName) ?? throw new InvalidOperationException();

            return new SqlConnection(cs);
        }
    }
}
