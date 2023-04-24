using DAL;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BL_PERMISO
    {
        public static List<string> InsertarSolicitudPermiso(string P_Cadena, int P_id_usuario_solicitante, int P_id_permiso, string P_motivoE, string P_fecha_inicio, string P_fecha_fin, int P_id_usuario_autorizador)
        {
            List<string> lstMensaje = new List<string>();
            DateTime fechaini = Convert.ToDateTime(P_fecha_inicio);
            DateTime fechafin = Convert.ToDateTime(P_fecha_fin);

            var diffDias =  fechafin - fechaini;

            try
            {
                var dpParametros = new
                {
                    @P_IdUsuarioSolicitante = P_id_usuario_solicitante,
                    @P_IdPermiso = P_id_permiso,
                    @P_motivo = P_motivoE,
                    @P_dias = diffDias.Days + 1,
                    @P_Fechainicio = P_fecha_inicio,
                    @P_FechaFin = P_fecha_fin,
                    @P_idUsuarioAutorizador = P_id_usuario_autorizador
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spSolicitarPermiso", dpParametros);
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

        public static List<PermisoRepDTO> ListarSolicitudesPermisos(string P_Cadena)
        {
            List<PermisoRepDTO> lstPermisoRep = new List<PermisoRepDTO>();
            DataTable Dt = new DataTable();

            var dpParametros = new
            {
    
            };

            Dt = Contexto.Funcion_StoreDB(P_Cadena, "spListarPermisos", dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstPermisoRep = (from item in Dt.AsEnumerable()
                                 select new PermisoRepDTO
                                 {
                                     id_solicitud_permiso = item.Field<int>("id_solicitud_permiso"),
                                     id_permiso = item.Field<int>("id_permiso"),
                                     dias = item.Field<int>("dias"),
                                     fecha_inicio = item.Field<DateTime>("fecha_inicio").ToString("yyyy/MM/dd"),
                                     fecha_fin = item.Field<DateTime>("fecha_fin").ToString("yyyy/MM/dd"),
                                     fecha_solicitud = item.Field<DateTime>("fecha_solicitud").ToString("yyyy/MM/dd"),
                                     fecha_resolucion = item.Field<DateTime>("fecha_resolucion").ToString("yyyy/MM/dd"),
                                     estado = item.Field<string>("estado"),
                                     motivo = item.Field<string>("motivo"),
                                     observaciones = item.Field<string?>("observaciones"),
                                     nombre_usuario_solicitante = item.Field<string>("Usuario_solicitante"),
                                     nombre_usuario_autorizador = item.Field<string>("Usuario_autorizador")
                                 }
                               ).ToList();

            }
            return lstPermisoRep;
        }

        public static List<string> ActualizarSolicitudPermiso(string P_Cadena, int P_idSolicitud, int P_estado, string P_observaciones, int P_id_usuario_autorizador)
        {
            List<string> lstMensaje = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_idSolicitud_permiso = P_idSolicitud,
                    P_Estado = P_estado,
                    P_Observaciones = P_observaciones,
                    P_idUsuario_autorizador = P_id_usuario_autorizador
                };

                Contexto.Procedimiento_StoreDB(P_Cadena, "spAutorizarPermiso", dpParametros);
                lstMensaje.Add("00");
                lstMensaje.Add("Solicitud Actualizada");


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
