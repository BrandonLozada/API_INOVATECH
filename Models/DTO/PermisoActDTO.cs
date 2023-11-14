using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PermisoActDTO {
        public int estado { get; set; } 
        public string? observaciones { get; set; }
        public int id_usuario_autorizador { get; set; }
    }
}
