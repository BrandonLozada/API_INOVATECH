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
    public class BL_ESTADO_CIVIL
    {
        public static List<EstadoCivilDTO> ConsultaEstadosCiviles(string P_Cadena)
        {
            List<EstadoCivilDTO> lstEstadoCivil = new List<EstadoCivilDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {

            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spEstadosCiviles", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstEstadoCivil = (from item in Dt.AsEnumerable()
                                  select new EstadoCivilDTO
                                  {
                                      id_estado_civil = item.Field<int>("id_estado_civil"),
                                      nombre = item.Field<string>("nombre")
                                  }
                               ).ToList();
            }

            return lstEstadoCivil;
        }
    }
}
