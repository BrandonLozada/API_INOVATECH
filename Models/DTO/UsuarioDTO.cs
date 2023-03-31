using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UsuarioDTO
    {
        public string correo { get; set; }
        public string contrasenia { get; set; } //public string celular { get; set; }
        public int es_activo { get; set; }
        public int id_rol { get; set; }
    }
}
