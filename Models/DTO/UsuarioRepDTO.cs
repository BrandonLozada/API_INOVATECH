using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class UsuarioRepDTO
    {
        public int id_usuario { get; set; }
        public string nombre_completo { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public string activo { get; set; }
        public string fecha_registro { get; set; }
    }
}
