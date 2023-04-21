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
    public class BL_DOMICILIO
    {
        public static List<string> InsertarDomicilio(string P_Cadena, int P_IdUsuarioE, string P_CalleE, string P_NumeroIntE, string P_NumeroExtE, string P_EntreCalles1E, string P_EntreCalles2E, string P_CodigoPostalE, string P_ColoniaE, string P_CiudadE, string P_EstadoE, string P_PaisE)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdUsuario = P_IdUsuarioE,
                    P_Calle = P_CalleE,
                    P_NumeroInterior = P_NumeroIntE,
                    P_NumeroExterior = P_NumeroExtE,
                    P_EntreCalles1 = P_EntreCalles1E,
                    P_EntreCalles2 = P_EntreCalles2E,
                    P_CodigoPostal = P_CodigoPostalE,
                    P_Colonia = P_ColoniaE,
                    P_Ciudad = P_CiudadE,
                    P_Estado = P_EstadoE,
                    P_Pais = P_PaisE
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spLlenarDomicilio", dpParametros);
                lstMensaje.Add("00");
                lstMensaje.Add("Domicilio Guardado");
            }
            catch (SqlException e)
            {
                lstMensaje.Add("14");
                lstMensaje.Add(e.Message);
            }

            return lstMensaje;
        }

        public static List<DomicilioRepDTO> ConsultaDomicilio(string P_Cadena, int IdUsuario)
        {
            List<DomicilioRepDTO> lstDomicilioRep = new List<DomicilioRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_IdUsuario = IdUsuario
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spVistaDomicilio", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstDomicilioRep = (from item in Dt.AsEnumerable()
                                 select new DomicilioRepDTO
                                 {
                                     id_usuario = item.Field<int>("id_usuario"),
                                     calle = item.Field<string>("calle"),
                                     numero_interior = item.Field<string>("numero_interior"),
                                     numero_exterior = item.Field<string>("numero_exterior"),
                                     entre_calles_1 = item.Field<string>("entre_calles_1"),
                                     entre_calles_2 = item.Field<string>("entre_calles_2"),
                                     codigo_postal = item.Field<string>("codigo_postal"),
                                     colonia = item.Field<string>("colonia"),
                                     ciudad = item.Field<string>("ciudad"),
                                     estado = item.Field<string>("estado"),
                                     pais = item.Field<string>("pais")
                                 }
                               ).ToList();
            }

            return lstDomicilioRep;
        }
    }
}
