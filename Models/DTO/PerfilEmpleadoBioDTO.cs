using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PerfilEmpleadoBioDTO {
        public int id_usuario { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string NSS { get; set; }
        public string infonavit { get; set; }
        public string salario { get; set; }
        public string estado_civil { get; set; }
        public int dias_descanso { get; set; }
        public string nomina { get; set; }
        public string fecha_ingreso { get; set; }
        public string puesto { get; set; }
        public string departamento { get; set; }
    }
}
