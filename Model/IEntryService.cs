using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordStorage.Model
{
    internal interface IEntryService : IDisposable
    {
        void Add(string name, string pass);

        void Update(Entry entry);

        void Delete(Entry entry);

        void Modify(Entry entry);

        List<Entry> GetAll();
    }
}
