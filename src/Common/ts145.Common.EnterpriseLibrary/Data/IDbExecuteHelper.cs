using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts145.Common.EnterpriseLibrary.Data
{
    public interface IDbExecuteHelper
    {
        void AddParameter(DbCommand command, string parameterName, object value, DbType? dbType, ParameterDirection direction);

        object GetParameterValue(DbCommand command, string parameterName);

        IEnumerable<dynamic> ExecuteDynamicReader(string procedureName, IEnumerable<Tuple<string, object, DbType?, ParameterDirection>> parameters, List<dynamic> outputs, List<dynamic> returnValues);
    }
}
