namespace prueba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Datos = new System.Windows.Forms.TabPage();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.numLector = new System.Windows.Forms.Label();
            this.Numregistro = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.fechaPicker = new System.Windows.Forms.DateTimePicker();
            this.conteoComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.sendData = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ubicacionComboBox = new System.Windows.Forms.ComboBox();
            this.btnCloseSession = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.almacenComboBox = new System.Windows.Forms.ComboBox();
            this.btnLoadDB = new System.Windows.Forms.Button();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.passwordTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.userTxtBox = new System.Windows.Forms.TextBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.Inventario = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.unidadComboBox = new System.Windows.Forms.ComboBox();
            this.nombre_producto = new System.Windows.Forms.Label();
            this.cantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.barcode = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainDBList = new System.Windows.Forms.ListBox();
            this.registros = new System.Windows.Forms.Label();
            this.updateMainTable = new System.Windows.Forms.Button();
            this.Datos.SuspendLayout();
            this.dataPanel.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.Inventario.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Datos.Controls.Add(this.dataPanel);
            this.Datos.Controls.Add(this.loginPanel);
            this.Datos.Location = new System.Drawing.Point(4, 26);
            this.Datos.Name = "Datos";
            this.Datos.Size = new System.Drawing.Size(229, 237);
            this.Datos.Text = "Login";
            // 
            // dataPanel
            // 
            this.dataPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataPanel.Controls.Add(this.button1);
            this.dataPanel.Controls.Add(this.numLector);
            this.dataPanel.Controls.Add(this.Numregistro);
            this.dataPanel.Controls.Add(this.label8);
            this.dataPanel.Controls.Add(this.fechaPicker);
            this.dataPanel.Controls.Add(this.conteoComboBox);
            this.dataPanel.Controls.Add(this.label7);
            this.dataPanel.Controls.Add(this.sendData);
            this.dataPanel.Controls.Add(this.label6);
            this.dataPanel.Controls.Add(this.ubicacionComboBox);
            this.dataPanel.Controls.Add(this.btnCloseSession);
            this.dataPanel.Controls.Add(this.label5);
            this.dataPanel.Controls.Add(this.almacenComboBox);
            this.dataPanel.Controls.Add(this.btnLoadDB);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.Location = new System.Drawing.Point(0, 0);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(229, 237);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(5, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 18;
            this.button1.Text = "Actualizar ";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numLector
            // 
            this.numLector.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.numLector.ForeColor = System.Drawing.Color.DarkRed;
            this.numLector.Location = new System.Drawing.Point(5, 180);
            this.numLector.Name = "numLector";
            this.numLector.Size = new System.Drawing.Size(115, 20);
            this.numLector.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Numregistro
            // 
            this.Numregistro.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Numregistro.ForeColor = System.Drawing.Color.DarkRed;
            this.Numregistro.Location = new System.Drawing.Point(5, 165);
            this.Numregistro.Name = "Numregistro";
            this.Numregistro.Size = new System.Drawing.Size(115, 20);
            this.Numregistro.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(1, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.Text = "Fecha:";
            // 
            // fechaPicker
            // 
            this.fechaPicker.CustomFormat = "";
            this.fechaPicker.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.fechaPicker.Location = new System.Drawing.Point(70, 94);
            this.fechaPicker.Name = "fechaPicker";
            this.fechaPicker.Size = new System.Drawing.Size(156, 23);
            this.fechaPicker.TabIndex = 14;
            this.fechaPicker.Value = new System.DateTime(2021, 5, 27, 0, 0, 0, 0);
            // 
            // conteoComboBox
            // 
            this.conteoComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.conteoComboBox.Location = new System.Drawing.Point(70, 65);
            this.conteoComboBox.Name = "conteoComboBox";
            this.conteoComboBox.Size = new System.Drawing.Size(156, 22);
            this.conteoComboBox.TabIndex = 13;
            this.conteoComboBox.SelectedIndexChanged += new System.EventHandler(this.conteoComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(-1, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.Text = "Conteo:";
            // 
            // sendData
            // 
            this.sendData.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.sendData.Location = new System.Drawing.Point(126, 179);
            this.sendData.Name = "sendData";
            this.sendData.Size = new System.Drawing.Size(100, 25);
            this.sendData.TabIndex = 9;
            this.sendData.Text = "Enviar Productos";
            this.sendData.Click += new System.EventHandler(this.sendData_Click_1);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(0, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.Text = "Ubicación:";
            // 
            // ubicacionComboBox
            // 
            this.ubicacionComboBox.Location = new System.Drawing.Point(70, 37);
            this.ubicacionComboBox.Name = "ubicacionComboBox";
            this.ubicacionComboBox.Size = new System.Drawing.Size(156, 23);
            this.ubicacionComboBox.TabIndex = 7;
            this.ubicacionComboBox.SelectedIndexChanged += new System.EventHandler(this.ubicacionComboBox_SelectedIndexChanged);
            // 
            // btnCloseSession
            // 
            this.btnCloseSession.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.btnCloseSession.Location = new System.Drawing.Point(126, 210);
            this.btnCloseSession.Name = "btnCloseSession";
            this.btnCloseSession.Size = new System.Drawing.Size(100, 25);
            this.btnCloseSession.TabIndex = 3;
            this.btnCloseSession.Text = "Cerrar Sesión";
            this.btnCloseSession.Click += new System.EventHandler(this.btnCloseSession_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(0, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.Text = "Almacén:";
            // 
            // almacenComboBox
            // 
            this.almacenComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.almacenComboBox.Location = new System.Drawing.Point(70, 8);
            this.almacenComboBox.Name = "almacenComboBox";
            this.almacenComboBox.Size = new System.Drawing.Size(156, 22);
            this.almacenComboBox.TabIndex = 1;
            this.almacenComboBox.SelectedIndexChanged += new System.EventHandler(this.almacenComboBox_SelectedIndexChanged);
            // 
            // btnLoadDB
            // 
            this.btnLoadDB.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.btnLoadDB.Location = new System.Drawing.Point(126, 148);
            this.btnLoadDB.Name = "btnLoadDB";
            this.btnLoadDB.Size = new System.Drawing.Size(100, 25);
            this.btnLoadDB.TabIndex = 5;
            this.btnLoadDB.Text = "Cargar Productos";
            this.btnLoadDB.Click += new System.EventHandler(this.btnLoadDB_Click_1);
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.loginPanel.Controls.Add(this.btnIngresar);
            this.loginPanel.Controls.Add(this.passwordTxtBox);
            this.loginPanel.Controls.Add(this.label4);
            this.loginPanel.Controls.Add(this.label2);
            this.loginPanel.Controls.Add(this.userTxtBox);
            this.loginPanel.Controls.Add(this.logo);
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(0, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(229, 237);
            this.loginPanel.GotFocus += new System.EventHandler(this.loginPanel_GotFocus);
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(69, 155);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(89, 30);
            this.btnIngresar.TabIndex = 9;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Location = new System.Drawing.Point(96, 113);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.PasswordChar = '*';
            this.passwordTxtBox.Size = new System.Drawing.Size(110, 23);
            this.passwordTxtBox.TabIndex = 8;
            this.passwordTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordTxtBox_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(28, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.Text = "Password:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.Text = "Usuario:";
            // 
            // userTxtBox
            // 
            this.userTxtBox.Location = new System.Drawing.Point(96, 84);
            this.userTxtBox.Name = "userTxtBox";
            this.userTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.userTxtBox.Size = new System.Drawing.Size(110, 23);
            this.userTxtBox.TabIndex = 2;
            this.userTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userTxtBox_KeyPress);
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(61, 14);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(118, 60);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // Inventario
            // 
            this.Inventario.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Inventario.Controls.Add(this.label3);
            this.Inventario.Controls.Add(this.unidadComboBox);
            this.Inventario.Controls.Add(this.nombre_producto);
            this.Inventario.Controls.Add(this.cantidad);
            this.Inventario.Controls.Add(this.label1);
            this.Inventario.Controls.Add(this.barcode);
            this.Inventario.Controls.Add(this.btnGuardar);
            this.Inventario.Controls.Add(this.button2);
            this.Inventario.Location = new System.Drawing.Point(4, 26);
            this.Inventario.Name = "Inventario";
            this.Inventario.Size = new System.Drawing.Size(229, 237);
            this.Inventario.Text = "Inventario";
            this.Inventario.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.Text = "Unidad:";
            // 
            // unidadComboBox
            // 
            this.unidadComboBox.Location = new System.Drawing.Point(86, 35);
            this.unidadComboBox.Name = "unidadComboBox";
            this.unidadComboBox.Size = new System.Drawing.Size(110, 23);
            this.unidadComboBox.TabIndex = 10;
            this.unidadComboBox.SelectedIndexChanged += new System.EventHandler(this.unidadComboBox_SelectedIndexChanged);
            this.unidadComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unidadComboBox_KeyPress);
            // 
            // nombre_producto
            // 
            this.nombre_producto.Location = new System.Drawing.Point(3, 61);
            this.nombre_producto.Name = "nombre_producto";
            this.nombre_producto.Size = new System.Drawing.Size(219, 53);
            // 
            // cantidad
            // 
            this.cantidad.Location = new System.Drawing.Point(187, 5);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(37, 23);
            this.cantidad.TabIndex = 8;
            this.cantidad.TextChanged += new System.EventHandler(this.cantidad_TextChanged);
            this.cantidad.GotFocus += new System.EventHandler(this.cantidad_GotFocus);
            this.cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantidad_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(152, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.Text = "Cant:";
            // 
            // barcode
            // 
            this.barcode.Location = new System.Drawing.Point(5, 6);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(141, 23);
            this.barcode.TabIndex = 4;
            this.barcode.TextChanged += new System.EventHandler(this.barcode_TextChanged);
            this.barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.barcode_KeyDown);
            this.barcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.barcode_KeyUp);
            this.barcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.barcode_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(37, 157);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(159, 37);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(86, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cerrar";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Datos);
            this.tabControl1.Controls.Add(this.Inventario);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(237, 267);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // mainDBList
            // 
            this.mainDBList.Location = new System.Drawing.Point(16, 36);
            this.mainDBList.Name = "mainDBList";
            this.mainDBList.Size = new System.Drawing.Size(197, 98);
            this.mainDBList.TabIndex = 0;
            // 
            // registros
            // 
            this.registros.Location = new System.Drawing.Point(28, 152);
            this.registros.Name = "registros";
            this.registros.Size = new System.Drawing.Size(100, 20);
            // 
            // updateMainTable
            // 
            this.updateMainTable.Location = new System.Drawing.Point(16, 175);
            this.updateMainTable.Name = "updateMainTable";
            this.updateMainTable.Size = new System.Drawing.Size(60, 21);
            this.updateMainTable.TabIndex = 2;
            this.updateMainTable.Text = "Update";
            this.updateMainTable.Click += new System.EventHandler(this.updateMainTable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(237, 267);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Inventario Físico";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Datos.ResumeLayout(false);
            this.dataPanel.ResumeLayout(false);
            this.loginPanel.ResumeLayout(false);
            this.Inventario.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Datos;
        private System.Windows.Forms.TabPage Inventario;
        private System.Windows.Forms.TextBox barcode;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox cantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.TextBox userTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox almacenComboBox;
        private System.Windows.Forms.Button btnCloseSession;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.Button btnLoadDB;
        private System.Windows.Forms.ListBox mainDBList;
        private System.Windows.Forms.Label registros;
        private System.Windows.Forms.Button updateMainTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ubicacionComboBox;
        private System.Windows.Forms.Label nombre_producto;
        private System.Windows.Forms.Button sendData;
        private System.Windows.Forms.Label Numregistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox unidadComboBox;
        private System.Windows.Forms.ComboBox conteoComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker fechaPicker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label numLector;
        private System.Windows.Forms.Button button1;

    }
}

