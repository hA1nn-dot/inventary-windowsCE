using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
namespace prueba
{
    class SQLiteFunction
    {
        private static SQLiteFunction function = null;
        private SQLiteFunction() {
            
        }
        public static SQLiteFunction getInstance(){
            if(function == null)
                function = new SQLiteFunction();
            return function;
        }
        public void updateDeletedProduct(Product product){
            string query = string.Format("UPDATE codigos set subido = 0 WHERE id_producto = {0} AND id_unidad = {1} AND id_ubicacion = {2}",
                product.getIDProduct(),product.getIDUnit(),product.getIDUbication());
            SQLiteConnection connection = LocalDBConnection.getInstance();
            openConnection(connection);
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            if (cmd.ExecuteNonQuery() == 0) {
                cmd.Dispose();
                connection.Close();
                throw new Exception("No se ha actualizado el producto");
            }
            cmd.Dispose();
            connection.Close();
        }

        public int getCountAvailableProductsInCodesTable() {
            string query = "SELECT COUNT(*) FROM codigos where subido = 1";
            SQLiteConnection connection = LocalDBConnection.getInstance();
            openConnection(connection);
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            int availableProducts = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            connection.Close();
            return availableProducts;
        }

        public Dictionary<string, string> searchAttributes(){
            Dictionary<string, string> userAttribute = new Dictionary<string, string>();
            string query = "SELECT usuario,conteo,fecha,ubicacion,almacen FROM personal";
            SQLiteConnection connection = LocalDBConnection.getInstance();
            openConnection(connection);
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                userAttribute.Add("userName", reader[0].ToString());
                userAttribute.Add("conteo", reader[1].ToString());
                userAttribute.Add("fecha", reader[2].ToString());
                userAttribute.Add("ubicacion", reader[3].ToString());
                userAttribute.Add("almacen", reader[4].ToString());
            }
            reader.Close();
            cmd.Dispose();
            connection.Close();
            return userAttribute;
        }

        public List<Product> getProductsList() {
            string query = "SELECT id_producto,id_unidad,id_ubicacion,cantidad,fecha_cap FROM codigos where subido = 1";
            List<Product> productList = new List<Product>();
            SQLiteConnection connection = LocalDBConnection.getInstance();
            openConnection(connection);
            
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                string id_producto = reader[0].ToString();
                string id_unidad = reader[1].ToString();
                string id_ubicacion = reader[2].ToString();
                string cantidad = reader[3].ToString();
                string fechaCaptura = reader[4].ToString();
                Product product = new Product(id_producto,id_unidad,id_ubicacion,cantidad,fechaCaptura);
                productList.Add(product);
            }
            
            reader.Close();
            cmd.Dispose();
            connection.Close();
            return productList;
        }



        private void openConnection(SQLiteConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }
    }
}
