using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public static class CustomDbParameter
    {
        public static DbParameter BuildParameter(string sParameterName, SqlDbType enumDbType, ParameterDirection enumParameterDirection)
        {
            return CustomDbParameter.BuildInternalParameter(sParameterName, enumDbType, DBNull.Value, enumParameterDirection, 0, false);
        }
        public static DbParameter BuildParameter(string sParameterName, SqlDbType enumDbType, object objParamValue, ParameterDirection enumParameterDirection)
        {
            return CustomDbParameter.BuildInternalParameter(sParameterName, enumDbType, objParamValue, enumParameterDirection, 0, false);
        }
        public static DbParameter BuildParameter(string sParameterName, SqlDbType enumDbType, ParameterDirection enumParameterDirection, int nSize)
        {
            return CustomDbParameter.BuildInternalParameter(sParameterName, enumDbType, DBNull.Value, enumParameterDirection, nSize,false);
        }
        public static DbParameter BuildParameter(string sParameterName, SqlDbType enumDbType, ParameterDirection enumParameterDirection, int nSize,bool bIsNullable)
        {
            return CustomDbParameter.BuildInternalParameter(sParameterName, enumDbType, DBNull.Value, enumParameterDirection, nSize, bIsNullable);
        }
        private static DbParameter BuildInternalParameter(string sParameterName, SqlDbType enumDbType, object objParamValue, ParameterDirection enumParameterDirection, int nSize,bool IsNullable)
        {
            SqlParameter oParameter = new SqlParameter(sParameterName, enumDbType, nSize);
            oParameter.Value = objParamValue;
            oParameter.IsNullable = IsNullable;
            oParameter.Direction = ParameterDirection.Output;

            return oParameter;
        }

    }
}
