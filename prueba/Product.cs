using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace prueba
{
    class Product
    {
        private string cantidad;
        private string idUbication;
        private string idProduct;
        private string idUnit;
        private string date;

        public Product(string id_producto, string id_unidad,string id_ubication,string cant,string fecha) {
            idProduct = id_producto;
            idUnit = id_unidad;
            idUbication = id_ubication;
            cantidad = cant;
            date = fecha;
        }

        public string getDate() {
            return date;
        }
        public string getCantidad() {
            return cantidad;
        }
        public string getIDUbication() {
            return idUbication;
        }
        public string getIDProduct() {
            return idProduct;
        }
        public string getIDUnit() {
            return idUnit;
        }
    }
}
