using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
    public class BL_INFOBANCO {
        public static List<string> InsertarInfoBanco(string P_Cadena, int P_IdUsuarioE, string P_BancoE, string P_TitularE, string P_NumCuentaE, string P_ClabeE, string P_TarjetaE)
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
                    P_Tarjeta = P_TarjetaE
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
    }
}
