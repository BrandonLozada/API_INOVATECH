using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Models.DTO;

namespace BLL
{
    public class BL_PUESTO
    {
        public static List<PuestoRepDTO> ConsultaPuestos(string P_Cadena)
        {
            List<PuestoRepDTO> lstPuestoRep = new List<PuestoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spPuestos", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstPuestoRep = (from item in Dt.AsEnumerable()
                                 select new PuestoRepDTO
                                 {
                                     id_puesto = item.Field<int>("id_puesto"),
                                     nombre = item.Field<string>("nombre")
                                 }
                               ).ToList();
            }

            return lstPuestoRep;
        }
    }
}
