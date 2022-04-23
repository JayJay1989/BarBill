using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BarBill.Constants
{
    public static class DatabaseConstants
    {
        public const string DatabaseFilename = "BarBillSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string Path
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return System.IO.Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
