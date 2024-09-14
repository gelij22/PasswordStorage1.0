using PasswordStorage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;

namespace PasswordStorage.data
{
     class DbEntryService : IEntryService
    {
        private List<Entry> entries;
        public DbEntryService()
        {
            entries = new List<Entry>();
        }
        public void Add(string name, string password)
        {
            string queryString = @"INSERT INTO Entries(Name, password) 
                                       VALUES (@name, @password);";
            using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
            using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
            {
                cmd.Parameters.Add("@name", System.Data.DbType.String).Value = name;
                cmd.Parameters.Add("@password", System.Data.DbType.String).Value = password;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные сохранены");
                conn.Close();
            }
        }
        public void Update(Entry entry)
        {
             
        }
        public void Delete(Entry entry)
        {
            string queryString = $@"DELETE FROM Entries WHERE id={entry.id};";
            using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
            using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
            {
                int rowsAffected =  cmd.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    MessageBox.Show("Запись удалена");
                }
                else
                {
                    MessageBox.Show($"Неверное значение: {rowsAffected}");
                }
               conn.Close();
            }
        }

        public void Modify(Entry entry)
        {
            string queryString = $@"UPDATE Entries SET  name='{entry.Name}', password='{entry.Password}' WHERE id ={entry.id};";
            using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
            using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    MessageBox.Show("Запись изменена");
                }
                else
                {
                    MessageBox.Show($"Неверное значение: {rowsAffected}");
                }
                conn.Close();
            }
        }

        public List<Entry> GetAll()
        {
            //Подключится к базе и взять все данные
            string queryString = @"select *  from Entries";
            using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
            using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
            using(SQLiteDataReader reader = cmd.ExecuteReader())
            {
                List<Entry> list = new List<Entry>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string password = reader.GetString(2);
                    Entry entry = new Entry(id, name, password);
                    list.Add(entry);    
                }
                return list;    
            }
        }
        public void Dispose()
        {

        }
    }
}
