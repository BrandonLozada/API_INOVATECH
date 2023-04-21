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
    public class BL_PERMISO
    {
        public static List<PermisoRepDTO> ConsultaPermisos(string P_Cadena)
        {
            List<PermisoRepDTO> lstPermisoRep = new List<PermisoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spPermisos", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstPermisoRep = (from item in Dt.AsEnumerable()
                                      select new PermisoRepDTO
                                      {
                                          id_permiso = item.Field<int>("id_permiso"),
                                          nombre = item.Field<string>("nombre")
                                      }
                               ).ToList();
            }

            return lstPermisoRep;
        }
    }
}
