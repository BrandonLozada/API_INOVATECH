using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace DAL
{
    public class Contexto
    {
        public static DataTable Funcion_StoreDB(String cadena, String P_Sentencia, object P_Parametro)
        {
            DataTable Dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var lst = conn.ExecuteReader(P_Sentencia, P_Parametro, commandType: CommandType.StoredProcedure);
                    Dt.Load(lst);

                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return Dt;

        }

        public static void Procedimiento_StoreDB(String cadena, String P_Sentencia, object P_Parametro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var lst = conn.ExecuteReader(P_Sentencia, P_Parametro, commandType: CommandType.StoredProcedure);
                }

            }
            catch (SqlException e)
            {
                throw e;
            }


        }
    }
}
