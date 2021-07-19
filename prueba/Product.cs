using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace prueba
{
    class Product
    {
        //private int productoID;
        //private int ubicacionID = null;
        //private int unidadID = null;

        //private int cantidad = null;
        //private string fecha = null;
        //private string usuario = null;

        public int productoID{
            get { return productoID; }
            set { productoID = value; }
        }
        public int ubicacionID {
            get { return ubicacionID; }
            set { ubicacionID = value; }
        }
        public int unidadID {
            get { return unidadID; }
            set { unidadID = value; }
        }
        public string fecha {
            get { return fecha; }
            set { fecha = value; }
        }
        public string usuario {
            get { return usuario; }
            set { usuario = value; }
        }
        public int cantidad {
            get { return cantidad;}
            set { cantidad = value; }
        }
    }
}
