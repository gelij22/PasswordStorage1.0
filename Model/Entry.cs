using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordStorage.Model
{
   public class Entry
    {
        public int id { get; set; }
        private string name;
        private string password;
        
        public string Password
        {
            get { return password; }
            set {password = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Entry() { }
        public Entry(int id, string name, string pass)
        {
            this.id = id;
            this.password = pass;
            this.name = name;
        }
        public override string ToString()
        {
            return $"{id} ресурс: {name}  пароль: {password}";
        }
    }
}
