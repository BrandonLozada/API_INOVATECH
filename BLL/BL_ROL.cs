using DAL;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BL_ROL
    {
        public static List<RolRepDTO> ConsultaRoles(string P_Cadena)
        {
            List<RolRepDTO> lstRolRep = new List<RolRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 1
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spRoles", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstRolRep = (from item in Dt.AsEnumerable()
                                      select new RolRepDTO
                                      {
                                          id_rol = item.Field<int>("id_rol"),
                                          nombre = item.Field<string>("nombre")
                                      }
                               ).ToList();
            }

            return lstRolRep;
        }
    }
}
