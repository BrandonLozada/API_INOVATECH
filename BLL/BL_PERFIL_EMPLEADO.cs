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
    public class BL_PERFIL_EMPLEADO
    {
        public static List<string> InsertarPerfilEmpleado(string P_Cadena, int P_IdUsuarioE, string P_NominaE, string P_FechaIngresoE, string P_FechaEgresoE, int P_IdPuestoE, int P_IdDepartamentoE)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_IdUsuario = P_IdUsuarioE,
                    P_Nomina = P_NominaE,
                    P_FechaIngreso = P_FechaIngresoE,
                    P_FechaEgreso = P_FechaEgresoE,
                    P_IdPuesto = P_IdPuestoE,
                    P_IdDepartamento = P_IdDepartamentoE
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spCrearPerfilEmpleado", dpParametros);
                lstMensaje.Add("00"); // TODO: ¿Por qué es "00"?
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
