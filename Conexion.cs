using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogoBBDD
{
    public class Conexion
    {

        public SqlConnection cnx;

        public Conexion()
        {
            try
            {
                cnx = new SqlConnection("Data Source=WINAPKUXNGWBYQM\\SQLEXPRESS;Initial Catalog=Catalogo;Integrated Security=True");
                Console.WriteLine("Éxito al conectar.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al conectar. " + ex.Message);
            }
        }
    }
}
