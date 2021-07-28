using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
namespace prueba
{
    class MainDB
    {
        private SqlDataReader reader;
        private SqlConnection connection;
        private SqlCommand cmd;

        private string user;
        private string database;
        private string server;
        private string password;

        private string connectionString;

        private void connectDB()
        {
            setSQLServerConnectionfromXML();
            connectionString = string.Format("Data Source={0};Initial Catalog={3};User ID={1};Password={2};Integrated Security=False; Connect Timeout=7", this.server, this.user, this.password, this.database);
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show("Error la conexión en el servidor "+server);
            }
        }

        private void setSQLServerConnectionfromXML()
        {
            try
            {
                
                XmlDocument doc = new XmlDocument();
                doc.Load("./Application/Inventario fisico/SQLiteDatalocal/App.config.xml");

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    String id = node.Attributes["id"].Value;
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name == "server")
                            {
                                this.server = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "database")
                            {
                                this.database = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "user")
                            {
                                this.user = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "password")
                            {
                                this.password = node.ChildNodes[i].InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Problemas con el archivo App.conf");
            }
            
        }

        public bool verifyUser_SQLServer(string usuario, string password) 
        {
            bool verificado = false;
            string commandString = string.Format("SELECT USUARIO, PASSWORD FROM USUARIOS WHERE USUARIO = '{0}'", usuario);
            try
            {
                connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    if (reader[1].ToString().Equals(password)) {
                        MessageBox.Show("Usuario verificado","Ingreso",MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
                        verificado = true;
                    }
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            return verificado;
        }

        public string userString 
        {
            get { return user; }
            set { user = value; }
        }

        public string serverString
        {
            get { return server; }
            set { server = value; }
        }

        public string passwordString
        {
            get { return password; }
            set { password = value; }
        }

        public string databaseString 
        {
            get { return database; }
            set { database = value;}
        }


        public void getAlmacenes(ComboBox almacenesBox)
        {
            string commandString = "SELECT NOMBRE FROM ALMACENES";
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    almacenesBox.Items.Add(reader[0].ToString());
                reader.Close();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            finally {
                this.connection.Close();
            }
        }

        public void _loadProductsToSQLite(string id_ubicacion, LiteDB liteDB, string fechaSeleccionada) 
        {
            string commandString = string.Format("SELECT DISTINCT P.ID,P.PRODUCTO,P.NOMBRE,PP.PRODUCTO,PP.UNIDAD_MEDIDA_EQUIVALENCIA,PP.CODIGO_BARRAS,PU.UBICACION,U.UNIDAD from PRODUCTOS as P "
                                                    +"INNER JOIN PRODUCTOS_PRECIOS as PP on P.ID = PP.PRODUCTO AND P.STATUS = 1 "
                                                    +"INNER JOIN PRODUCTOS_UBICACIONES as PU on PU.PRODUCTO = PP.PRODUCTO "
                                                    +"INNER JOIN UNIDADES as U on U.ID = PP.UNIDAD_MEDIDA_EQUIVALENCIA "
                                                    +"INNER JOIN CONTEOS_IF as CI on CI.PRODUCTO = P.ID and CI.FECHA = '{1}' "
                                                    +"AND PU.UBICACION = {0}", id_ubicacion,fechaSeleccionada);
            string id_producto      = string.Empty;
            string codigo_barras    = string.Empty;
            string nombre_producto  = string.Empty;
            string id_unidad        = string.Empty;
            string unidad           = string.Empty;
            string codigo_producto  = string.Empty;
            this.connection.Open();
            cmd = new SqlCommand(commandString, connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id_producto = reader[0].ToString();
                codigo_producto = reader[1].ToString();
                nombre_producto = reader[2].ToString();
                id_unidad = reader[4].ToString();
                codigo_barras = reader[5].ToString();
                unidad = reader[7].ToString();
                liteDB.downloadMainDB(codigo_barras, nombre_producto, id_producto, id_ubicacion, id_unidad, unidad, codigo_producto);
                
            }
            reader.Close();
            this.connection.Close();
        }
        

        public string getIdAlmacen(string nombre_almacen) 
        {
            string ID_almacen = "";
            string commandString = string.Format("SELECT Id FROM ALMACENES WHERE ALMACEN = '{0}'", nombre_almacen);
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    ID_almacen = reader[0].ToString();
                }
                reader.Close();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            finally
            {
                this.connection.Close();
            }

            return ID_almacen;
        }

        public string getIdUbicacion(string id_almacen, string clave_ubicacion)
        {
            string ID_ubicacion = "0";
            string commandString = string.Format("SELECT ID FROM UBICACIONES WHERE ALMACEN = {0} and CLAVE_UBICACION = '{1}'", id_almacen,clave_ubicacion);
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ID_ubicacion = reader[0].ToString();
                }
                reader.Close();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            finally
            {
                this.connection.Close();
            }

            return ID_ubicacion;
        }

        /*public static List<Object> getListIDUbicationFromServer(string id_almacen)
        {
            List<Object> listIDUbication = new List<Object>();
            string commandString = string.Format("SELECT ID FROM UBICACIONES WHERE ALMACEN = {0}", id_almacen);
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listIDUbication.Add(reader[0].ToString());
                }
                reader.Close();
            }
            catch (SqlException sql_ex)
            {
                MessageBox.Show(sql_ex.Message);
            }
            finally
            {
                this.connection.Close();
            }

            return listIDUbication;
        }*/

        public void getUbicaciones(ComboBox ubicacionesComboBox, string almacenNombre)
        {
            string commandString = string.Format("SELECT ub.CLAVE_UBICACION FROM UBICACIONES as ub INNER JOIN ALMACENES as alm on alm.ID = ub.ALMACEN and alm.NOMBRE = '{0}'", almacenNombre);
            try
            {
                connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    ubicacionesComboBox.Items.Add(reader[0].ToString());
                reader.Close();
                this.connection.Close();
            }
            catch (SqlException sql_ex)
            {
                throw sql_ex;
            }catch (InvalidOperationException inv){
                return;
            }
        }

        public bool getFechaConteo(String fechaSeleccionada){
            bool encontrado = false;
            fechaSeleccionada = fechaSeleccionada.Replace("/","-");
            string commandString = string.Format("SELECT DISTINCT FECHA FROM CONTEOS_IF");
            try
            {
                connection.Open();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    if (reader[0].ToString() == fechaSeleccionada) {
                        encontrado = true;
                    }
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SqlException sql_ex)
            {
                throw new Exception("SQLserver: Error al obtener datos de SQLServer, vuelva a intentarlo.");
            }
            return encontrado;
        }

        public void _insertValues_MainDB(string fecha_cap, string id_producto, string id_unidad, string cantidad, string usuario, string id_ubicacion, string conteo) 
        {
            string commandString = "";
            string conteoActual = "";               //Cantidad ConteoActual
            int conteoActualizado = 0;              //Total del Conteo
            fecha_cap = fecha_cap.Replace("/","-");
            try {
                if (conteo == "Conteo 1") 
                {
                    conteoActual = getConteoActual("CONTEO1",id_producto, id_unidad, fecha_cap, id_ubicacion);
                    conteoActualizado = int.Parse(conteoActual) + int.Parse(cantidad);

                    string diferencia = getDiferencia(conteoActualizado.ToString(), id_producto,id_unidad,fecha_cap,id_ubicacion);
                    commandString = string.Format("UPDATE CONTEOS_IF SET CONTEO1 = {0}, USUARIO1 = '{1}', DIFERENCIA = {3} WHERE PRODUCTO = {2} AND FECHA = '{4}' AND UNIDAD = {5} AND UBICACION = {6}", conteoActualizado.ToString(), usuario, id_producto, diferencia,fecha_cap,id_unidad, id_ubicacion);
                }
                else if (conteo == "Conteo 2")
                {
                    conteoActual = getConteoActual("CONTEO2", id_producto, id_unidad,fecha_cap, id_ubicacion);
                    conteoActualizado = int.Parse(conteoActual) + int.Parse(cantidad);

                    string diferencia = getDiferencia(conteoActualizado.ToString(), id_producto, id_unidad, fecha_cap, id_ubicacion);
                    commandString = string.Format("UPDATE CONTEOS_IF SET CONTEO2 = {0}, USUARIO2 = '{1}', DIFERENCIA = {3} WHERE PRODUCTO = {2} AND FECHA = '{4}' AND UNIDAD = {5} AND UBICACION = {6}", conteoActualizado.ToString(), usuario, id_producto, diferencia,fecha_cap, id_unidad, id_ubicacion);
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }

          
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                cmd.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        string getConteoActual(string CONTEO_N, string id_producto,string id_unidad, string fecha, string id_ubicacion) 
        {
            string cantidadActual = "0";
            string commandString = string.Format("SELECT {0} FROM CONTEOS_IF where PRODUCTO = {1} AND UNIDAD = {2} AND FECHA = '{3}' AND UBICACION = {4}",CONTEO_N, id_producto, id_unidad, fecha, id_ubicacion);
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidadActual = reader[0].ToString();
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el conteo");
            }
            return cantidadActual;
        }

        string getDiferencia(string cantidad, string id_producto, string id_unidad, string fecha, string id_ubicacion) {
            int diferencia = 0;
            int existencia = 0;
            string commandString = string.Format("SELECT EXISTENCIA FROM CONTEOS_IF where PRODUCTO = {0} AND UNIDAD = {1} AND FECHA = '{2}' AND UBICACION = {3}", id_producto, id_unidad, fecha, id_ubicacion);
            try
            {
                this.connectDB();
                cmd = new SqlCommand(commandString, connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existencia = int.Parse(reader[0].ToString());
                    diferencia = int.Parse(cantidad) - existencia;
                }
                reader.Close();
                this.connection.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch(InvalidCastException e_cast){
                throw e_cast;
            }
            return diferencia.ToString();
        }

    }
}
