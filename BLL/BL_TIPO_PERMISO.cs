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
    public class BL_TIPO_PERMISO
    {
        public static List<TipoPermisoDTO> ConsultaTiposPermisos(string P_Cadena)
        {
            List<TipoPermisoDTO> lstTipoPermiso = new List<TipoPermisoDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {

            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spTiposPermisos", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstTipoPermiso = (from item in Dt.AsEnumerable()
                                 select new TipoPermisoDTO
                                 {
                                     id_tipo_permiso = item.Field<int>("id_tipo_permiso"),
                                     nombre = item.Field<string>("nombre")
                                 }
                               ).ToList();
            }

            return lstTipoPermiso;
        }
    }
}
