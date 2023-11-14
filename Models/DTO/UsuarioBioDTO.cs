using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class UsuarioBioDTO
    {
        public int id_usuario { get; set; }
        public string nombre_completo { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
    }
}
