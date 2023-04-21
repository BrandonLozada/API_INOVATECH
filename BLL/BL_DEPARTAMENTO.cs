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
    public class BL_DEPARTAMENTO
    {
        public static List<DepartamentoRepDTO> ConsultaDepartamentos(string P_Cadena)
        {
            List<DepartamentoRepDTO> lstDepartamentoRep = new List<DepartamentoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
               
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spDepartamentos", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstDepartamentoRep = (from item in Dt.AsEnumerable()
                                select new DepartamentoRepDTO
                                {
                                    id_departamento = item.Field<int>("id_departamento"),
                                    nombre = item.Field<string>("nombre")
                                }
                               ).ToList();
            }

            return lstDepartamentoRep;
        }
    }
}
