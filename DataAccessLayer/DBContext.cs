using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DbContext
    {
        private readonly string _connectionString;
        protected SqlConnection Connection { get; private set; }

        public DbContext()
        {
            // Store the connection string here
            _connectionString = "Data Source=PHUCNVH;Database=FUMiniHotelManagement;" +
                "User Id=sa;Password=1234567;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false;";
            Connection = new SqlConnection(_connectionString);
        }

        protected void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        protected void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
