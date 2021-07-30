using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace prueba
{
    class User
    {
        string userName;
        string conteo;
        string date;
        string ubicationName;
        string almacenName;
        public static User usuario = null;

        private User() {
            SQLiteFunction function = SQLiteFunction.getInstance();
            Dictionary<string,string> attributes = function.searchAttributes();
            foreach (var values in attributes) {
                if (values.Key == "userName") userName = values.Value;
                if (values.Key == "conteo") conteo = values.Value;
                if (values.Key == "fecha") date = values.Value;
                if (values.Key == "ubicacion") ubicationName = values.Value;
                if (values.Key == "almacen") almacenName = values.Value;
            }
        }
        public static User getInstance() {
            if (usuario == null)
                usuario = new User();
            return usuario;
        }

        public string getDate() {
            return date;
        }
        public string getAlmacenName() {
            return almacenName;
        }
        public string getUbicationName() {
            return ubicationName;
        }
        public string getConteo() {
            return conteo;
        }
        public string getUserName() {
            return userName;
        }

        
    }
}
