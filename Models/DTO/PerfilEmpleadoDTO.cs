﻿using System;
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
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string NSS { get; set; }
        public string infonavit { get; set; }
        public decimal salario { get; set; }
        public int estado_civil { get; set; }
        public string fecha_ingreso { get; set; }
        public string fecha_egreso { get; set; }
        public int id_puesto { get; set; }
        public int id_departamento { get; set; }
    }
}
