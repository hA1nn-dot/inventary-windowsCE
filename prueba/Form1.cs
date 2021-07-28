using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
namespace prueba
{
    public partial class Form1 : Form
    {
        private Thread loadProductsThread;
        private List<string> codesList = new List<string>();
        private List<string> codesServerList = new List<string>();
        private List<string> almacenes = new List<string>();
        private List<string> PersonalData = new List<string>();


        private MainDB database = new MainDB();         //Clase de la base de datos SQL Server
        private LiteDB lector_database = new LiteDB();  //Clase de la base de datos SQLite

        private string rutaSQLite = @"./Application/Inventario fisico/SQLiteDatalocal/localdb.db";
        private string almacen;
        private string ubicacion;
        private string nombre_usuario;
        private string usuario_password;
        private string conteo;
        private string unidad;

        private bool sesion = false;
        private bool borrar = false;
        public DateTime fechaActual = DateTime.Today;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            fechaPicker.CustomFormat = "yyyy/MM/dd";
            fechaPicker.Format = DateTimePickerFormat.Custom;
            
            fechaPicker.Value = fechaActual;

            crear_DatabaseSQLite();
            sesion = lector_database.validate_registers_inTable("mainCodigos");

            if (lector_database.getFecha() != "")
                fechaPicker.Value = Convert.ToDateTime(lector_database.getFecha());
            

            if (sesion && lector_database.validate_registers_inTable("mainCodigos"))
            {
                almacenComboBox.Items.Insert(0, lector_database.getNameAlmacen());
                almacenComboBox.SelectedIndex = 0;
                ubicacionComboBox.Items.Insert(0, lector_database.getNameUbicacion());
                ubicacionComboBox.SelectedIndex = 0;
                conteoComboBox.Items.Insert(0, lector_database.getConteo());
                conteoComboBox.SelectedIndex = 0;

                update_InterfaceView(false);
                loadInterface();
            }
            else 
            {
                conteoComboBox.Items.Insert(0, "Conteo 1");
                conteoComboBox.Items.Insert(1, "Conteo 2");
                conteoComboBox.SelectedIndex = 0;
                loadLogin();
            }

            actualizarNum_productos();
            update_ProductosLector();

            CleanBoxes();
            barcode.Focus();
        }

        

        void actualizarNum_productos() {
            if (File.Exists(rutaSQLite))
                Numregistro.Text = "P. Cargados: " + lector_database.countRegistros("mainCodigos").ToString();
            else 
                Numregistro.Text = "P. Cargados: 0";
        }

        void update_ProductosLector() {
            if (File.Exists(rutaSQLite))
                numLector.Text = "P. Lector: " + lector_database.countRegistros("codigos").ToString();
            else 
                numLector.Text = "P. Lector: 0";
        }
        private void loadSQLiteData() 
        {
            codesList.Clear();
            lector_database.loadLocalData(codesList);
        }

        private void loadLogin() 
        {
            dataPanel.Visible = false;
            loginPanel.Visible = true;

            userTxtBox.Enabled = true;
            passwordTxtBox.Enabled = true;
            btnIngresar.Enabled = true;

            userTxtBox.Focus();
            passwordTxtBox.Text = "";
            fechaPicker.Value = fechaActual;
        }

        private void loadInterface()
        {
            dataPanel.Visible = true;
            loginPanel.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barcode_TextChanged(object sender, EventArgs e)
        {
            if (barcode.Text == "")
                CleanBoxes();
            else
                cantidad.Text = "1";
            
        }

        void getUnidades()
        {
            unidadComboBox.DataSource = null;
            unidadComboBox.Items.Clear();
            lector_database.getUnidades(unidadComboBox,barcode.Text);
        }


        private void barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string descripcion = "";
                string codigo = barcode.Text.Trim();

                if (!codigo.Equals(""))
                {
                    if (File.Exists(rutaSQLite))                                
                    {
                        if (lector_database.VerifyCodesLiteDB(codigo))              //verifica código en la tabla mainCodigos
                        {
                            descripcion = lector_database.getNombreProducto(codigo);
                            cargar_Datos_producto(codigo, descripcion);
                            focus_On_cantidad();
                        }
                        else if (lector_database.codeExist_tbl_codigos(codigo, "")) //Verifica código en la tabla codigos
                        {
                            cargar_Datos_producto(codigo, descripcion);
                            focus_On_cantidad();
                        }
                        else
                        {                                                        //No encontró el producto en el lector            
                            nombre_producto.Text = "<Producto no encontrado, ingresa otro código>";
                            borrar = true;
                        }                                                        
                    }
                    else {
                        MessageBox.Show("No ha iniciado sesión en este lector", "Inicia Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        CleanBoxes();
                    }
                }
                else 
                    CleanBoxes();
                
            }
            if ((int)e.KeyChar == (int)Keys.Back && borrar)
            {
                barcode.Text = "";
                borrar = false;
            }

        }

        //Carga las unidades, descripción y la cantidad del producto
        void cargar_Datos_producto(string codigo, string descripcion) 
        {
            nombre_producto.Text = "Producto: " + descripcion;
            getUnidades();

            if (unidadComboBox.Items.Count <= 0)
                this.unidad = "";
            else 
                this.unidad = unidadComboBox.SelectedItem.ToString();
            
            cantidad.Text = lector_database.getCantidad(codigo, unidad);
            focus_On_cantidad();
        }

        void focus_On_cantidad() 
        {
            cantidad.GotFocus += cantidad_GotFocus;
            cantidad.Focus();
            cantidad.Select(0, cantidad.TextLength);
        }

        private void cantidad_GotFocus(object sender, EventArgs e)
        {
            cantidad.Select(0, cantidad.TextLength);
        }

        private void cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string codigo = barcode.Text.Trim();
                if (File.Exists(rutaSQLite))
                {
                    if (lector_database.VerifyCodesLiteDB(codigo))  //Verifica si existe el código en la tabla mainCodigos.
                        guardarDatos(false);
                    else
                        guardarDatos(true);
                }
                else {
                    MessageBox.Show("No ha iniciado sesión en este lector", "Inicia Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    CleanBoxes();
                }
            }

        }

        //Evento al tocar el panel de la pestaña Inventario
        private void tabPage1_Click(object sender, EventArgs e)
        {
            barcode.Focus();
        }

        //Botón de inicio de sesión en la pestaña login
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            iniciarSesion();
            crear_DatabaseSQLite();
        }

        void iniciarSesion() 
        {
            userTxtBox.Enabled = false;
            passwordTxtBox.Enabled = false;
            btnIngresar.Enabled = false;
            this.nombre_usuario = userTxtBox.Text.Trim();
            this.usuario_password = passwordTxtBox.Text.Trim();
            if (nombre_usuario == "" || usuario_password == "")
            {
                MessageBox.Show("Favor de ingresar los datos correctamente.", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                loadLogin();
            }
            else {
                try
                {
                    if (database.verifyUser_SQLServer(nombre_usuario, usuario_password))
                    {
                        loadInterface();
                        getAlmacen();
                        actualizarNum_productos();
                        update_ProductosLector();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña o el usuario son incorrectos, favor de verificar.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        loadLogin();
                        passwordTxtBox.Text = "";
                        passwordTxtBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    loadLogin();
                    passwordTxtBox.Text = "";
                    passwordTxtBox.Focus();
                }
            }
            
        }

        //Llenado de ComboBox almacenes
        private void getAlmacen() 
        {
            almacenComboBox.DataSource = null;          //limpiando Comboboxes
            almacenComboBox.Items.Clear();
            ubicacionComboBox.DataSource = null;
            ubicacionComboBox.Items.Clear();

            database.getAlmacenes(almacenComboBox);     //llenar el combo de almacenes
            almacenComboBox.SelectedIndex = 0;
        }

        //Cerrando sesión
        private void btnCloseSession_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string message = "¿Seguro que quieres cerrar sesión? Se eliminarán todos los registros del lector.";
            string caption = "Cerrar sesión";
            
            result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try {
                    actualizarNum_productos();
                    update_ProductosLector();
                    File.Delete(rutaSQLite);

                    if (!File.Exists(rutaSQLite))
                    {
                        habilitarBtnInterface();
                        loadLogin();
                        sesion = false;
                        conteoComboBox.Items.Clear();
                        conteoComboBox.Items.Insert(0, "Conteo 1");
                        conteoComboBox.Items.Insert(1, "Conteo 2");
                        conteoComboBox.SelectedIndex = 0;
                        nombre_usuario = "";
                    }
                    else {
                        MessageBox.Show("No hay registros cargados", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    
                }catch(Exception ex){
                    MessageBox.Show("Error al borrar los datos");
                }
            }
            
        }

        void habilitarBtnInterface() 
        {
            almacenComboBox.Enabled = true;
            ubicacionComboBox.Enabled = true;
            conteoComboBox.Enabled = true;
            fechaPicker.Enabled = true;
            btnCloseSession.Enabled = true;
            btnLoadDB.Enabled = true;
        }

        //Función para crear la base de datos SQLite
        void crear_DatabaseSQLite() 
        {
            lector_database.createTable_codigos();
            lector_database.createTable_MainCodigos();
            lector_database.createTable_personal();
        }

        //Guardar el codigo leido por el lector
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string codigo = barcode.Text.Trim();
            if (File.Exists(rutaSQLite))
            {
                if (lector_database.VerifyCodesLiteDB(codigo))
                    guardarDatos(false);
                else
                    guardarDatos(true);
            }
            else {
                MessageBox.Show("No ha iniciado sesión en este lector", "Inicia Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                CleanBoxes();
            }
        }

        private void unidadComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
                focus_On_cantidad();
            
        }

        void guardarDatos(bool allowedCode) 
        {
            string codigo = barcode.Text.Trim();
            string cant = cantidad.Text.Trim();
            string unidad = "";
            
            try
            {
                if (codigo != "" && cant != "" && int.Parse(cant) >= 0)
                {
                    if (File.Exists(rutaSQLite))
                    {
                        if (unidadComboBox.Items.Count <= 0)
                        {

                        }
                        else
                            unidad = unidadComboBox.SelectedItem.ToString();
                        
                        if (lector_database.codeExist_tbl_codigos(codigo, unidad))      //Si existe el producto en el lector
                        {
                            if (lector_database.getCantidad(codigo, unidad) != cant)
                                existentRegister(codigo, cant, unidad);                 //Actualiza cantidad del producto
                        }
                        else
                            newCodeRegister(codigo, cant, unidad, allowedCode);         //Añade nuevo producto encontrado
                    }
                    else 
                        MessageBox.Show("No ha ininciado sesión en el lector","Error de sesión",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                    
                }
                else {
                    MessageBox.Show("Favor de rellenar los campos correctamente","Error de valor",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                }
                
            }
            catch (Exception ex)
            {

            }
            finally {
                CleanBoxes();
                barcode.Focus();
            }
        }

        //Función para actualizar la cantidad o borrar un producto
        void existentRegister(string codeReader, string cant, string unidad) 
        {
            DialogResult resultado;
            if (cant.Trim() == "0"){
                resultado = MessageBox.Show("¿Seguro que quieres borrar éste registro?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                if (resultado == System.Windows.Forms.DialogResult.Yes) {
                    lector_database.deleteCode(codeReader, unidad);
                    update_ProductosLector();
                }
                    
            }else
                lector_database.actualizarListadeCodigos(codeReader, cant,unidad);
        }

        //Función para insertar un nuevo registro a SQLite
        void newCodeRegister(string codeReader, string cant, string unidad, bool allowedRegister) 
        {
            DialogResult resultado;
            try {
                if (!codeReader.Equals("") && !cant.Equals("") && int.Parse(cant) > 0)
                {
                    if (allowedRegister)
                    {
                        //resultado = MessageBox.Show("Este código no existe en su catálogo.", "Codigo inexistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        /*if (resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            lector_database._addNewRegister(codeReader, cant, unidad, fechaPicker.Text);
                        }*/
                    }
                    else if (Int32.Parse(lector_database.countMainRegistros()) == 0)
                    {
                        MessageBox.Show("No hay codigos registrados.","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                    }
                    else if (lector_database.leer_MainCodigos(codeReader,unidad))
                    {
                        lector_database._addNewRegister(codeReader,cant,unidad,fechaPicker.Text);
                        nombre_producto.Text = lector_database.getNombreProducto(codeReader);
                        update_ProductosLector();
                    }
                }
            }catch(Exception ex){}
            
        }

        private void CleanBoxes(){
            barcode.Text = "";
            cantidad.Text = "";
            unidadComboBox.DataSource = null;
            unidadComboBox.Items.Clear();
            nombre_producto.Text = "<Escriba código o clave y presione Enter para verificar>";
        }

        //Carga Datos SQL Server al SQLite  
        private void btnLoadDB_Click_1(object sender, EventArgs e)
        {
            loadData_toSQLite();
        }

        //Función para cargar los productos al lector SQLServer -> SQLite
        void loadData_toSQLite() {
            string almacenSeleccionado = "";
            string ubicacionSeleccionado = "";
            string conteoSeleccionado = "";
            string fechaSeleccionada = "";
            crear_DatabaseSQLite();
            try
            {
                almacenSeleccionado = almacenComboBox.SelectedItem.ToString();
                ubicacionSeleccionado = ubicacionComboBox.SelectedItem.ToString();
                conteoSeleccionado = conteoComboBox.SelectedItem.ToString();
                fechaSeleccionada = fechaPicker.Text;
                try {
                    if (database.getFechaConteo(fechaSeleccionada))
                    {
                        SQLServer_to_SQLite(almacenSeleccionado, ubicacionSeleccionado, conteoSeleccionado, fechaSeleccionada);
                    }
                    else
                    {
                        MessageBox.Show("No hay conteo en la fecha: " + fechaSeleccionada, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }catch(Exception sql_ex){
                    MessageBox.Show(sql_ex.Message,"Error SQLServer",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error al ingresar los datos, favor de verificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                update_InterfaceView(true);
            }
        }

        //Función para habilitar/dehabilitar los botones de login
        void update_InterfaceView(bool ableButton)
        {
            almacenComboBox.Enabled = ableButton;
            ubicacionComboBox.Enabled = ableButton;
            conteoComboBox.Enabled = ableButton;
            fechaPicker.Enabled = ableButton;
            btnLoadDB.Enabled = ableButton;
        }
        
       
        void clean_ComboBoxesInterface() 
        {
            ubicacionComboBox.DataSource = null;
            ubicacionComboBox.Items.Clear();
            almacenComboBox.DataSource = null;
            almacenComboBox.Items.Clear();
        }

        void SQLServer_to_SQLite(string almacenSeleccionado, string ubicacionSeleccionado, string conteoSeleccionado, string fechaSeleccionada) 
        {
            List<int> listIDUbication = new List<int>();
            try
            {

                update_InterfaceView(false);
                this.almacen = database.getIdAlmacen(almacenSeleccionado);
                /*if (ubicacionSeleccionado == "Todas las áreas") //Haim
                    listIDUbication = MainDB.getListIDUbicationFromServer(almacenSeleccionado);
                else*/
                    this.ubicacion = database.getIdUbicacion(this.almacen, ubicacionSeleccionado);

                loadProductsThread = new Thread(() =>
                {
                    try
                    {
                        MessageBox.Show("Cargando productos, por favor espere...");
                        database._loadProductsToSQLite(ubicacion, lector_database, fechaSeleccionada);
                        MessageBox.Show("Productos cargados existosamente...");
                        sesion = true;
                        if (lector_database.countRegistros("personal") == 0)
                            lector_database._loadUsuario(nombre_usuario, almacenSeleccionado, ubicacionSeleccionado, conteoSeleccionado, fechaSeleccionada);
                    }
                    catch (SqlException sqlError)
                    {
                        throw sqlError;
                    }
                    catch (Exception exc) {
                        throw exc;
                    }
                    
                });
                loadProductsThread.Start();
                btnCloseSession.Enabled = true;
                sendData.Enabled = true;
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Error de red: " + exc.Message.ToString());
                _borrarSQLite();
                clean_ComboBoxesInterface();
                update_InterfaceView(true);
            }
            catch (Exception exc2)
            {
                MessageBox.Show("Error general" + exc2.Message.ToString());
                _borrarSQLite();
                clean_ComboBoxesInterface();
                update_InterfaceView(true);
            }
            

            /*if (database._loadProductsToSQLite(ubicacion, lector_database,fechaSeleccionada))
            {
                actualizarNum_productos();
                sesion = true;
                if (lector_database.countRegistros("personal") == 0) {
                    lector_database._loadUsuario(nombre_usuario, almacenSeleccionado, ubicacionSeleccionado, conteoSeleccionado, fechaSeleccionada);
                }
            }
            else {
                _borrarSQLite();
                clean_ComboBoxesInterface();
                update_InterfaceView(true);
                getAlmacen();
                return;
            }
            btnCloseSession.Enabled = true;
            sendData.Enabled = true;*/
        }

        private void updateMainTable_Click(object sender, EventArgs e)
        {
            actualizarNum_productos();
        }

        //Carga las ubicaciones de SQL Server
        private void almacenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ubicacionComboBox.DataSource = null;
            ubicacionComboBox.Items.Clear();
            try
            {
                if (almacenComboBox.SelectedItem.ToString() != "")
                {
                    if (ubicacionComboBox.Enabled && sesion == false) {
                        if (almacenComboBox.SelectedItem.ToString() == "B4 RIO VERDE") {
                            ubicacionComboBox.Items.Add("Todas las áreas");
                        }else
                            database.getUbicaciones(ubicacionComboBox, almacenComboBox.SelectedItem.ToString());
                        ubicacionComboBox.SelectedIndex = 0;
                    }
                        
                }
            }
            catch (SqlException sqlException)
            {
                MessageBox.Show("SQLserver: Error al cargar ubicaciones", "Error ubicaciones", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (NullReferenceException null_exc) {
                MessageBox.Show("Seleccione un almacen", "Error almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }catch(Exception erroeComboBoxes){
                MessageBox.Show("Error de carga: No se han cargado los datos correspondientes, vuelva a cargar","Error de carga");
                loadLogin();
            }
            
               
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                barcode.Focus();
        }
        
        private void sendData_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(rutaSQLite))
            {
                DialogResult resultado;
                resultado = MessageBox.Show("¿Seguro que quiere enviar los registros?", "Envio de registros", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                    _envioRegistros();
            }
            else
                MessageBox.Show("No se han cargado los productos al lector.", "Error cargar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        //Función para descargar los registros del lector SQLite -> SQLserver
        private void _envioRegistros() {
            
                try
                {
                    string conteo = conteoComboBox.SelectedItem.ToString();
                     
                    lector_database.download_ValuesLocalDB(database, conteo);
                    sesion = false;
                    _borrarSQLite();
                        
                    clean_ComboBoxesInterface();
                    actualizarNum_productos();
                    update_ProductosLector();
                    getAlmacen();
                    update_InterfaceView(true);
                }catch (InvalidOperationException ex2) {
                    MessageBox.Show("Ha ocurrido un error inesperado, vuelva a enviar de nuevo. 122x "+ex2.Message.ToString(), "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }catch(SQLiteException exq){
                    MessageBox.Show("Ha ocurrido un error inesperado, vuelva a enviar de nuevo. 124x " + exq.Message.ToString(), "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                catch (SqlException sql)
                {
                    MessageBox.Show("Ha ocurrido un error inesperado, vuelva a enviar de nuevo. 1288x " + sql.Message.ToString(), "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error inesperado, vuelva a enviar de nuevo." + ex.Message.ToString(), "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
        }

        private void unidadComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(unidadComboBox.Items.Count <= 0))
            {
                try {
                    this.unidad = unidadComboBox.SelectedItem.ToString();
                    cantidad.Text = lector_database.getCantidad(barcode.Text, this.unidad);
                    focus_On_cantidad();
                }catch(Exception ex){
                    return;
                }
            }
        }

        //Función para borrar el archivo SQLite
        void _borrarSQLite() {
            try
            {
                if (File.Exists(rutaSQLite))
                    File.Delete(rutaSQLite);
                else
                    MessageBox.Show("No hay registros cargados", "Error registros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar los datos","Error al borrar",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
        }


        private void userTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                passwordTxtBox.Focus();
            }
        }

        private void passwordTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                iniciarSesion();
            }
        }

        private void conteoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ubicacionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDeleteLiteTable_Click(object sender, EventArgs e)
        {

        }

        private void loginPanel_GotFocus(object sender, EventArgs e)
        {

        }

        private void barcode_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void barcode_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarNum_productos();
            update_ProductosLector();
        }
    }
}