using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace prueba
{
    class LocalDBConnection
    {
        private static string dataBaseFile = "/SQLiteDatalocal/localdb.db";
        private static string rootDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        private static SQLiteConnection connection = null;

        public static SQLiteConnection getInstance()
        {
            if (connection == null) {
                connection = new SQLiteConnection(getDataSource());
                //createTable_codigos();
            }
                
            return connection;
        }

        private static string getDataSource() {
            return "Data Source = ." + getDataBasePath();
        }

        private static string getDataBasePath()
        {
            string fullDBPath = (rootDirectory + Path.GetFullPath(dataBaseFile)).Replace("\\", "/");
                //throw new FileNotFoundException("NO se ha encontrado la base de datos "+fullDBPath);
            if (!Directory.Exists(rootDirectory + "\\SQLiteDatalocal"))
            {
                    System.IO.Directory.CreateDirectory(rootDirectory + "\\SQLiteDatalocal");
            }
            return fullDBPath;
        }

        private static void createTable_codigos()
        {
            string TABLE_CODIGOS = "CREATE table IF NOT EXISTS codigos(id integer primary key, codigo text, cantidad integer, fecha_cap text,id_producto integer, id_ubicacion integer, id_unidad integer,  subido integer);";
            string TABLE_PERSONAL = "CREATE table IF NOT EXISTS personal(id integer primary key,usuario text, almacen text, ubicacion text, conteo text, fecha text);";
            string TABLE_MAIN_CODIGOS = "CREATE table IF NOT EXISTS mainCodigos(id integer primary key, barcode text,codigo_producto text,nombre text, id_producto integer, id_ubicacion integer,id_unidad integer, unidad text);"; 
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(TABLE_PERSONAL+TABLE_CODIGOS+TABLE_MAIN_CODIGOS, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        
    }
}
