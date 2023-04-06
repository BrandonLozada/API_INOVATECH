using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO //namespace Models.DTO
{
    public class UsuarioDTO
    {
        public string correo { get; set; }
        public string contrasenia { get; set; } //public string celular { get; set; }
        public int es_activo { get; set; }
        public int id_rol { get; set; }
    }

    //public class UsuarioDTO2
    //{
    //    public string nombre { get; set; }
    //    public string primer_apellido { get; set; }
    //    public string? segundo_apellido { get; set; }
    //    public string fecha_nacimiento { get; set; }
    //    public string sexo { get; set; } //public int id_genero { get; set; }
    //    public string celular { get; set; }
    //    public string correo { get; set; }
    //    public string contrasenia { get; set; }
    //    public int es_activo { get; set; }
    //    public int id_rol { get; set; }
    //}
}
