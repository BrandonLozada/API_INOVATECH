using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PermisoDTO {
        public int id_usuario_solicitante { get; set; }
        public int id_permiso { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
    }
}
