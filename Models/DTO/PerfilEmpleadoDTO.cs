using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PerfilEmpleadoDTO
    {
        public int id_usuario { get; set; }
        public string nomina { get; set; }
        public string fecha_ingreso { get; set; }
        public string fecha_egreso { get; set; }
        public int id_puesto { get; set; }
        public int id_departamento { get; set; }
    }

    public class PerfilEmpleadoRepDTO {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string primer_apellido { get; set; }
        public string? segundo_apellido { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; } //public string genero { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
    }
}
