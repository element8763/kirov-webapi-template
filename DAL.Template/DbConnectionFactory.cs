using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NAutowired.Core.Attributes;
using System.Data;

namespace DAL.Template
{
    [Component]
    public class DbConnectionFactory
    {

        [Autowired]
        private IOptions<Config> config { get; set; }

        private IDbConnection dbConnection;

        /// <summary>
        /// 
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = new MySqlConnection(config.Value.ConnectionString);
                }
                return dbConnection;
            }
        }
    }
}
