using System.Data;
using System.Data.SqlClient;
using static LaYumba.Functional.F;

namespace SampleWeb
{
  public static class ConnectionHelper
  {
    public static R Connect<R>(string connString, Func<IDbConnection, R> func)
      => Using(new SqlConnection(connString)
         , conn => { conn.Open(); return func(conn); });

    public static R Transact<R> (SqlConnection conn, Func<SqlTransaction, R> f)
    {
      using var tran = conn.BeginTransaction();

      R r = f(tran);
      tran.Commit();

      return r;
    }
  }
}
