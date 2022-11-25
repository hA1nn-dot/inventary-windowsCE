using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
namespace prueba
{
    class SQLiteFunction
    {
        private static SQLiteConnection connection;

        private static SQLiteConnection getInstance(){
            if (connection == null)
                new SQLiteConnection("Data Source = ./Application/Inventario fisico/SQLiteDatalocal/localdb.db");
            return connection;
        }
        private static void openConnection() {
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }

        private static void closeConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

        public static void changeStatusProduct(Product product) {
            string query = string.Format(
                "UPDATE codigos SET subido = 0 WHERE id_producto = {0} AND id_unidad = {1} AND subido = 1"
                ,product.productoID,product.unidadID
                );
            try
            {
                connection = getInstance();
                openConnection();
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                closeConnection();
            }
            catch(SQLiteException SQliteError){
                throw SQliteError;
            }
        }
        
    }
}
