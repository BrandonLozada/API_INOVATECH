﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class UsuarioGenDTO
    {
        //Falta validar si sera usada del todo junto al EndPoint
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; } //public int id_genero { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
        public string es_activo { get; set; }
        public string nombre_rol { get; set; }
        public string fecha_registro { get; set; }
    }
    public class UsuarioRepDTO
    {
        public int id_usuario { get; set; }
        public string nombre_completo { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public string activo { get; set; }
        public string fecha_registro { get; set; }
    }

    public class UsuarioDTO 
    {
        public string nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; } //public int id_genero { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
        public int es_activo { get; set; }
        public int id_rol { get; set; }
    }
}
