using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PasswordStorage.ModelUser;
using PasswordStorage.Model;

namespace PasswordStorage.data
{
    internal class AppContext : DbContext
    {
        public DbSet<ModelUser.User> Users { get; set; }
        public AppContext() : base("DefaultConnection") { }
    }
}
