using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentServer
{
    public class DatabaseManager
    {
        private readonly DataClasses1DataContext db;

        public DatabaseManager()
        {
            db = new DataClasses1DataContext();
        }

        public DataClasses1DataContext GetDatabase()
        {
            return db;
        }
    }
}
