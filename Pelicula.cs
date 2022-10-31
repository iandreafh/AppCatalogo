using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogoBBDD
{
    public class Pelicula
    {

        public int? Id { get; set; }

        public string titulo { get; set; }

        public DateTime estreno { get; set; }

        public string? genero { get; set; }

        public int? valoracion { get; set; }
    }
}
