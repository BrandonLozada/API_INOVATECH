using DAL;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
    public class BL_INFOBANCO {
        public static List<string> InsertarInfoBanco(string P_Cadena, int P_IdUsuarioE, string P_BancoE, string P_TitularE, string P_NumCuentaE, string P_ClabeE, string P_TarjetaE, int P_EsActiva)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdUsuario = P_IdUsuarioE,
                    P_Banco = P_BancoE,
                    P_Titular = P_TitularE,
                    P_Num_cuenta = P_NumCuentaE,
                    P_Clabe = P_ClabeE,
                    P_Tarjeta = P_TarjetaE,
                    P_EsActiva = P_EsActiva
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spCrearInfoBancaria", dpParametros);
                lstMensaje.Add("00");
                lstMensaje.Add("Información Guardada");
            }
            catch (SqlException e)
            {
                lstMensaje.Add("14");
                lstMensaje.Add(e.Message);
            }

            return lstMensaje;
        }

        public static List<InfoBancoRepDTO> ConsultaTodo(string P_Cadena)
        {
            List<InfoBancoRepDTO> lstInfoBancoRep = new List<InfoBancoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 1
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spListarInfoBancaria", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstInfoBancoRep = (from item in Dt.AsEnumerable()
                                 select new InfoBancoRepDTO
                                 {
                                     id_usuario = item.Field<int>("id_usuario"),
                                     nombre_completo = item.Field<string>("nombre_completo"),
                                     banco = item.Field<string>("banco"),
                                     titular = item.Field<string>("titular"),
                                     num_cuenta = item.Field<string>("num_cuenta"),
                                     clabe = item.Field<string>("clabe"),
                                     tarjeta = item.Field<string>("tarjeta"),
                                     es_activa = item.Field<string>("activo")
                                 }
                               ).ToList();
            }

            return lstInfoBancoRep;
        }
        public static List<InfoBancoRepDTO> ConsultaXNombre(string P_Cadena, string P_Letras)
        {
            List<InfoBancoRepDTO> lstInfoBancoRep = new List<InfoBancoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
                P_Accion = 2,
                P_NombreC = P_Letras
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spListarInfoBancaria", dpParametros);


            if (Dt.Rows.Count > 0)
            {
                lstInfoBancoRep = (from item in Dt.AsEnumerable()
                                   select new InfoBancoRepDTO
                                   {
                                       id_usuario = item.Field<int>("id_usuario"),
                                       nombre_completo = item.Field<string>("nombre_completo"),
                                       banco = item.Field<string>("banco"),
                                       titular = item.Field<string>("titular"),
                                       num_cuenta = item.Field<string>("num_cuenta"),
                                       clabe = item.Field<string>("clabe"),
                                       tarjeta = item.Field<string>("tarjeta"),
                                       es_activa = item.Field<string>("activo")
                                   }
                               ).ToList();
            }

            return lstInfoBancoRep;
        }
    }
}
