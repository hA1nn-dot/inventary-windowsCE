using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
namespace prueba
{
    class LiteDB
    {
        private SQLiteDataReader reader;
        private SQLiteConnection connection = new SQLiteConnection("Data Source = ./Application/Inventario fisico/SQLiteDatalocal/localdb.db");
        
        //  Dolphin 60s de Octavio\\\Program Files
        //"Data Source = ./Application/Inventario fisico/SQLiteDatalocal/localdb.db"
        // "Data Source = ./Program Files/Inventario fisico/SQLiteDatalocal/localdb.db"
        private SQLiteCommand cmd;

        private void connectLiteDB()
        {
            try
            {
                connection = new SQLiteConnection("Data Source = ./Application/Inventario fisico/SQLiteDatalocal/localdb.db");
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    this.connection.Open();
                }
            }
            catch (SQLiteException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            catch (InvalidOperationException invalid) {
                MessageBox.Show(invalid.Message);
            }
        }

        //Creación de la base de datos del lector
        public void createTable_codigos() 
        {
            string query = "CREATE table IF NOT EXISTS codigos(id integer primary key, codigo text, cantidad integer, fecha_cap text,id_producto integer, id_ubicacion integer, id_unidad integer,  subido integer)";
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (SQLiteException lite_ex)
            {
                MessageBox.Show(lite_ex.Message);
            }
        }
        public void createTable_personal()
        {
            string query = "CREATE table IF NOT EXISTS personal(id integer primary key,usuario text, almacen text, ubicacion text, conteo text, fecha text);";
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (SQLiteException lite_ex)
            {
                MessageBox.Show(lite_ex.Message);
            }

        }

        


        //Creación de la tabla para los codigos de barras y productos del SQL server
        public void createTable_MainCodigos() 
        {
            string query = "CREATE table IF NOT EXISTS mainCodigos(id integer primary key, barcode text,codigo_producto text,nombre text, id_producto integer, id_ubicacion integer,id_unidad integer, unidad text)";
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (SQLiteException lite_ex)
            {
                MessageBox.Show(lite_ex.Message);
            }
        }


        public void loadLocalData(List<string> lista_codigos){
            string query = "SELECT * FROM codigos";
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista_codigos.Add(reader[1].ToString());
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool validate_registers_inTable(string table) //true = hay usuario // false = 
        {
            string query = string.Format("SELECT * FROM {0}",table);
            bool encontrado = false;
            try
            {
                if (table != "")
                {
                    this.connectLiteDB();
                    cmd = new SQLiteCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        encontrado = true;        //Ya hay un usuario ingresado
                    }
                    reader.Close();
                    this.connection.Close();
                }
                else {
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error al validar registros en SQLite: "+ex.Message);
            }
            return encontrado;
        }

        public int countCodesTable() //true = hay usuario // false = 
        {
            int rows = 0;
            string query = string.Format("SELECT * FROM codigos");
            try
            {
                this.connectLiteDB();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rows++;
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            return rows;
        }


        public void downloadMainDB(string barcode, string nombre_producto, string id_producto ,string id_ubicacion, string id_unidad, string unidad, string codigo_producto) 
        {
            for (int i = 0; i < nombre_producto.Length; i++)
            {
                if (nombre_producto[i].ToString() == "'") {
                    nombre_producto = nombre_producto.Replace("'", " ");
                }
            }
            string query = string.Format("INSERT INTO mainCodigos VALUES (null,'{0}','{6}','{1}',{2},{3},{4},'{5}');", barcode, nombre_producto, id_producto, id_ubicacion, id_unidad, unidad, codigo_producto);
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (Exception ex)
            {

            }
        }

        


        public void disconnetLiteDB() 
        {
            connection.Close();
        }

        public void _addNewRegister(string codigo_barras, string cant, string unidad, string fecha)
        {
            DialogResult result;
            string id_producto = "0";
            string id_ubicacion = getID_ubicacion();
            string id_unidad = "0";
            string fecha_cap = fecha;
            string query;
            if (unidad != "")
            {
                id_producto = getID("id_producto", codigo_barras);
                id_unidad = getIDunidad("id_unidad", unidad);
                
            }
            
            query = string.Format("INSERT INTO codigos(codigo,cantidad,fecha_cap,id_producto,id_ubicacion,id_unidad,subido) VALUES ('{0}',{1},'{2}',{3},{4},{5},1)", codigo_barras, cant, fecha_cap, id_producto, id_ubicacion, id_unidad);
           
            
            try
            {
                this.connection.Dispose();
                this.connection.Open();
                cmd.Dispose();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
               
                result = MessageBox.Show("Producto Registrado", "Registrado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.connection.Close();
                cmd.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Problema al registrar el producto: "+ex.Message,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
            
        }

        string getID_ubicacion() {
            string id = "0";
            string commandString = string.Format("SELECT id_ubicacion FROM mainCodigos where id = 1");
            try
            {
                this.connection.Open();  //sospechoso
                
                cmd = new SQLiteCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    id = reader[0].ToString();
                }
                reader.Close();
                this.connection.Dispose();
                this.connection.Close();
            }
            catch (SQLiteException sql_ex)
            {
                MessageBox.Show("Problema al obtener el ID: " + sql_ex.Message);
            }
            return id;
        }

        string getID(string column,string codigo_barras) {
            string id = "";
            bool encontrado = false;
            string commandString = string.Format("SELECT {0} FROM mainCodigos WHERE barcode = '{1}'", column, codigo_barras);
            try
            {
                
                this.connection.Open();
                cmd = new SQLiteCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    encontrado = true;
                    id = reader[0].ToString();
                }
                reader.Close();
                this.connection.Dispose();
                this.connection.Close();
            }
            catch (SQLiteException sql_ex)
            {
                MessageBox.Show("Problema al obtener el ID: "+sql_ex.Message);
            }
            if (encontrado == false) 
            {

                commandString = string.Format("SELECT {0} FROM mainCodigos WHERE codigo_producto = '{1}'", column, codigo_barras);
                try
                {

                    this.connection.Open();
                    cmd = new SQLiteCommand(commandString, connection);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                    reader.Close();
                    this.connection.Dispose();
                    this.connection.Close();
                }
                catch (SQLiteException sql_ex)
                {
                    MessageBox.Show("Problema al obtener el ID: " + sql_ex.Message);
                }
            }
            return id;
        }

        string getIDunidad(string column, string unidad)
        {
            string id = "";
            string commandString = string.Format("SELECT {0} FROM mainCodigos WHERE unidad = '{1}'", column, unidad);
            try
            {

                this.connection.Open();
                cmd = new SQLiteCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id = reader[0].ToString();
                }
                reader.Close();
                this.connection.Dispose();
                this.connection.Close();
            }
            catch (SQLiteException sql_ex)
            {
                MessageBox.Show("Error al obtener id unidad SQLite: "+sql_ex.Message);
            }

            return id;
        }


        public string countMainRegistros() 
        {
            string query = string.Format("SELECT * FROM mainCodigos");
            int registros = 0;
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    registros++;
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return registros.ToString();
        }

        public int countRegistros(string table_name) 
        {
            string query = string.Format("SELECT * FROM {0}", table_name);
            int registros = 0;
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    registros++;
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return registros;
        }


        

        public string getCantidad(string codigo, string unidad) {

            string id_unidad = "";
            string id_producto = "";
            string query = "";
            string cantidad = "1";
            if (unidad == "")
            {
                query = string.Format("SELECT cantidad FROM codigos WHERE codigo = '{0}'", codigo);
            }
            else {
                id_unidad = getIDunidad("id_unidad", unidad);
                id_producto = getID("id_producto", codigo);
                query = string.Format("SELECT cantidad FROM codigos WHERE id_producto = {0} AND id_unidad = {1}", id_producto, id_unidad);            
            }
            
            try
            {
                this.connectLiteDB();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    cantidad = reader[0].ToString();
                    break;                                    //Solo lee el primero que encuentre
                }                                             //No esta validado por si hay codigos repetidos
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error al obtener la cantidad SQLite: "+ex.Message);
            }

            return cantidad;
        }

        public string getNombreProducto(string codigo_barras)
        {
            string nombre_producto = "";
            bool encontrado = false;
            string commandString = string.Format("SELECT nombre FROM mainCodigos WHERE codigo_producto = '{0}'", codigo_barras);
            try
            {
                this.connectLiteDB();       //no modificar
                cmd = new SQLiteCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    encontrado = true;
                    nombre_producto = reader[0].ToString();
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException sql_ex)
            {
                MessageBox.Show("Error al obtener el nombre del producto: "+sql_ex.Message);
            }

            if (encontrado == false)
            {
                commandString = string.Format("SELECT nombre FROM mainCodigos WHERE barcode = '{0}'", codigo_barras);
                try
                {
                    this.connectLiteDB();       //no modificar
                    cmd = new SQLiteCommand(commandString, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        nombre_producto = reader[0].ToString();
                    }
                    reader.Close();
                    this.connection.Close();
                }
                catch (SQLiteException sql_ex)
                {
                    MessageBox.Show("Error al obtener el nombre del producto: " + sql_ex.Message);
                }
            }
            return nombre_producto;
        }

        public void actualizarListadeCodigos(string codigo,string cantidad_modificada,string unidad) 
        {
            string id_unidad = "";
            string query = "";
            string id_producto = "";
            if (unidad == "")
            {
                query = string.Format("UPDATE codigos SET cantidad = {0} WHERE codigo = '{1}'", cantidad_modificada, codigo);
            }
            else {
                id_unidad = getIDunidad("id_unidad", unidad);
                id_producto = getID("id_producto",codigo);
                query = string.Format("UPDATE codigos SET cantidad = {0} WHERE id_producto = {1} AND id_unidad = {2}", cantidad_modificada, id_producto, id_unidad);
            }
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Datos Actualizados","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
                this.connection.Close();
                cmd.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Problema al actualizar datos: "+ex.Message);
            }
            
        }

        public void _loadUsuario(string usuario, string almacen, string ubicacion, string conteo, string fecha)
        {
            try
            {
                string query = string.Format("INSERT INTO personal VALUES (null,'{0}','{1}','{2}','{3}','{4}')", usuario, almacen, ubicacion,conteo,fecha);
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ////Verify if code exist in codigos table
        public bool codeExist_tbl_codigos(string codigo_evalua, string unidad)
        {
            bool encontrado = false;
            string id_unidad = "";
            string query = "";
            string id_producto = "";
            if (unidad == "")
            {
                query = string.Format("SELECT * FROM codigos WHERE codigo = '{0}'", codigo_evalua);
            }
            else {
                id_unidad = getIDunidad("id_unidad", unidad);
                id_producto = getID("id_producto",codigo_evalua);
                query = string.Format("SELECT * FROM codigos WHERE id_producto = {0} AND id_unidad = {1};", id_producto, id_unidad);
            }
            
            
            try
            {
                connectLiteDB();    //No cambiar    
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    encontrado = true;
                }
                reader.Close();
                this.connection.Close();
                cmd.Dispose();
            }
            catch (SQLiteException ex)
            {
                encontrado = false;
            }
            
            return encontrado;

            
        }

        //Verify if code exist in mainCodigos table
        public bool VerifyCodesLiteDB(string codigo_evalua) {
            bool encontrado = false;
            string query = string.Format("SELECT * FROM mainCodigos WHERE barcode = '{0}'", codigo_evalua);
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) {
                    this.connection.Open();
                }
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    encontrado = true;
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (encontrado)
            {
                return encontrado;
            }
            else 
            {
                query = string.Format("SELECT * FROM mainCodigos WHERE codigo_producto = '{0}'", codigo_evalua);
                try
                {
                    this.connection.Open();
                    cmd = new SQLiteCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        encontrado = true;
                    }
                    reader.Close();
                    this.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            }
            return encontrado;
            
        }

        public bool leer_MainCodigos(string codigo_evalua, string unidad) 
        {
            
            string id_unidad = getIDunidad("id_unidad", unidad);
            bool encontrado = false;
            string query = string.Format("SELECT * FROM mainCodigos WHERE barcode = '{0}' AND id_unidad = {1}", codigo_evalua, id_unidad);
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    this.connection.Open();
                }
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    encontrado = true;
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (encontrado)
            {
                return encontrado;
            }
            else
            {
                query = string.Format("SELECT * FROM mainCodigos WHERE codigo_producto = '{0}' AND id_unidad = {1}", codigo_evalua,id_unidad);
                try
                {
                    this.connection.Open();
                    cmd = new SQLiteCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        encontrado = true;
                    }
                    reader.Close();
                    this.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return encontrado;
        }

        public void deleteCode(string codigo_eliminar, string unidad) {

            string id_unidad = "";
            string query = "";
            string id_producto = "";
            if (unidad == "")
            {
                id_unidad = getIDunidad("id_unidad", unidad);
                query = string.Format("DELETE FROM codigos WHERE codigo = '{0}'", codigo_eliminar);
            }
            else {
                id_unidad = getIDunidad("id_unidad", unidad);
                id_producto = getID("id_producto",codigo_eliminar);
                query = string.Format("DELETE FROM codigos WHERE id_producto = {0} AND id_unidad = {1}", id_producto, id_unidad);
            }

            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.connectLiteDB();
                    cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                this.connection.Close();
                cmd.Dispose();
                MessageBox.Show("Codigo borrado","Codigo Eliminado", MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        
        }

        public void download_ValuesLocalDB(MainDB SQLServer, string conteo)
        {
            string query = "SELECT * FROM codigos";

            string fecha = string.Empty;
            string id_producto = string.Empty;
            string id_unidad = string.Empty;
            string cantidad = string.Empty;
            string usuario = getUsuario();
            string id_ubicacion = string.Empty;
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, this.connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cantidad = reader[2].ToString();
                    fecha = reader[3].ToString();
                    id_producto = reader[4].ToString();
                    id_ubicacion = reader[5].ToString();
                    id_unidad = reader[6].ToString();

                    SQLServer._insertValues_MainDB(fecha, id_producto, id_unidad, cantidad, usuario, id_ubicacion, conteo);
                }
                MessageBox.Show("DATOS ENVIADOS");
                reader.Close();
                cmd.Dispose();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            catch (InvalidOperationException imp) 
            {
                throw imp;
            }
            catch (Exception pas)
            {
                throw pas;
            }
        }

        public void deleteCodigosTable()
        {
            string query = string.Format("DELETE FROM codigos");
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, this.connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string getUsuario() {
            string query = string.Format("SELECT usuario FROM personal");
            try
            {
                this.connectLiteDB();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.disconnetLiteDB();
            }
            return "";
        }

        public string getNameAlmacen() {
            string nameAlmacen = "";
            string query = string.Format("SELECT almacen FROM personal");
            try
            {
                this.connectLiteDB();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nameAlmacen = reader[0].ToString();
                }
                reader.Close();
                this.disconnetLiteDB();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nameAlmacen;
        }

        public string getNameUbicacion()
        {
            string nameUbicacion = "";
            string query = string.Format("SELECT ubicacion FROM personal");
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nameUbicacion = reader[0].ToString();
                }
                reader.Close();
                this.disconnetLiteDB();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nameUbicacion;
        }

        public string getConteo()
        {
            string conteo = "";
            string query = string.Format("SELECT conteo FROM personal");
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conteo = reader[0].ToString();
                }
                reader.Close();
                this.disconnetLiteDB();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return conteo;
        }

        public string getFecha() 
        {
            string fecha = "";
            string query = string.Format("SELECT fecha FROM personal");
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fecha = reader[0].ToString();
                }
                reader.Close();
                this.disconnetLiteDB();
                cmd.Dispose();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fecha;
        }

        public void getPersonalData(List<string> personalData) 
        {
            string query = string.Format("SELECT almacen,ubicacion,conteo FROM personal");
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    personalData.Add(reader[0].ToString());
                    personalData.Add(reader[1].ToString());
                    personalData.Add(reader[2].ToString());
                }
                reader.Close();
                this.disconnetLiteDB();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getUnidades(ComboBox unidadesComboBox, string barcode)
        {
            string query = string.Format("SELECT unidad FROM mainCodigos where codigo_producto = '{0}'", barcode);
            string id_producto = "";
            int i = 0;
            bool encontrado = false;
            try
            {
                this.connection.Open();
                cmd = new SQLiteCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    encontrado = true;
                    unidadesComboBox.Items.Insert(i, reader[0].ToString());
                    unidadesComboBox.SelectedIndex = 0;
                    i++;
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Si el codigo que leyó un codigo de barras, lee el codigo del producto
            
            if (encontrado == false) {
                try
                {
                    query = string.Format("SELECT unidad FROM mainCodigos where barcode = '{0}'", barcode);
                    i = 0;
                    this.connection.Open();
                    cmd = new SQLiteCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        encontrado = true;
                        unidadesComboBox.Items.Insert(i, reader[0].ToString());
                        unidadesComboBox.SelectedIndex = 0;
                        i++;
                    }
                    reader.Close();
                    this.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }

    
}
