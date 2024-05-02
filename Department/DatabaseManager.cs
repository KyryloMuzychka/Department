using System;

namespace DepartmentServer
{
    /// <summary>
    /// Represents a manager for accessing the database.
    /// </summary>
    public class DatabaseManager
    {
        private readonly DataClasses1DataContext db;

        /// <summary>
        /// Constructs a new instance of the DatabaseManager class.
        /// </summary>
        public DatabaseManager()
        {
            db = new DataClasses1DataContext();
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <returns>The database context.</returns>
        public DataClasses1DataContext GetDatabase()
        {
            return db;
        }
    }
}
