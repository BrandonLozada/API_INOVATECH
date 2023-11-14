using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO 
{
    public class InfoBancoRepDTO {
        public int id_usuario { get; set; }
        public string nombre_completo { get; set; }
        public string banco { get; set; }
        public string titular { get; set; }
        public string num_cuenta { get; set; }
        public string clabe { get; set; }
        public string tarjeta { get; set; }
        public string es_activa { get; set; }
    }
}
