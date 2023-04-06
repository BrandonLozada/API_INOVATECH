using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DomicilioDTO
    {
        public string id_usuario  { get; set; }
        public string calle { get; set; }
        public string? numero_interior { get; set; }
        public string? numero_exterior { get; set; }
        public string? entre_calles_1 { get; set; }
        public string? entre_calles_2 { get; set; }
        public string codigo_postal { get; set; }
        public string colonia { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
    }
}
