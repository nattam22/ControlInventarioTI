using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlInventarioTI
{
    internal class Equipo
    {
        public int Id { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string tipoEquipo { get; set; }
        public string fechaCompra { get; set; }
        public double Precio { get; set; }
    }
}
