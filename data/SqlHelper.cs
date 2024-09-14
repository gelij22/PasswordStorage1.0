
using System.Data.SQLite;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordStorage.data
{
    internal static class SqlHelper
    {
        public static SQLiteConnection OpenDbConnection()
        {
            string connectionString = "Data Source=.\\..\\..\\Storage.db";
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
