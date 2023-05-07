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
        public string motivo { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public int id_usuario_autorizador { get; set; }
    }

    public class PermisoRepDTO {
        public int id_solicitud_permiso { get; set; }
        public string tipo_permiso { get; set; }
        public int dias { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public string fecha_solicitud { get; set; }
        public string fecha_resolucion { get; set; }
        public string estado { get; set; }
        public string motivo { get; set; }
        public string? observaciones { get; set; }
        public string nombre_usuario_solicitante { get; set; }
        public string nombre_usuario_autorizador { get; set; }
    }

    public class PermisoActDTO {
        public int estado { get; set; } 
        public string? observaciones { get; set; }
        public int id_usuario_autorizador { get; set; }
    }
}
