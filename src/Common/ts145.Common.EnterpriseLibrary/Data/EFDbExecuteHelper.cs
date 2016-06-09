using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Collections.Concurrent;

namespace ts145.Common.EnterpriseLibrary.Data
{
    public class EFDbExecuteHelper : IDbExecuteHelper
    {        
        private Database _db;
        
        private static readonly DatabaseProviderFactory _providerFactory = new DatabaseProviderFactory();

        public EFDbExecuteHelper(string connectionString = "")
        {
            DatabaseFactory.ClearDatabaseProviderFactory();
            DatabaseFactory.SetDatabaseProviderFactory(_providerFactory);            
            _db = string.IsNullOrWhiteSpace(connectionString) ? _providerFactory.CreateDefault() : _providerFactory.Create(connectionString);            
        }

        public void AddParameter(DbCommand command, string parameterName, object value, DbType? dbType, ParameterDirection direction)
        {            
            _db.AddParameter(command, parameterName, dbType.HasValue ? dbType.Value : default(DbType), direction, parameterName, DataRowVersion.Current, value);
        }

        public object GetParameterValue(DbCommand command, string parameterName)
        {
            var value = _db.GetParameterValue(command, parameterName);
            return value;
        }

        /// <summary>
        /// Execute Procedure And Return ExpandoObject
        /// </summary>
        /// <param name="procedureName">Procedure Name</param>
        /// <param name="parameters"> Tuple List of Parameter (ParameterName, value, DbType, ParameterDirection)</param>
        /// <param name="outputs">Query Select Result List</param>
        /// <param name="returnValues">Return Value List</param>
        /// <returns></returns>
        public IEnumerable<dynamic> ExecuteDynamicReader(string procedureName, IEnumerable<Tuple<string, object, DbType?, ParameterDirection>> parameters, List<dynamic> outputs, List<dynamic> returnValues)
        {
            using (var command = _db.GetStoredProcCommand(procedureName))
            {
                foreach (var parameter in parameters)
                {
                    AddParameter(command, parameter.Item1, parameter.Item2, parameter.Item3, parameter.Item4);
                }
                
                var dataset = _db.ExecuteDataSet(command);

                using (var dataTableReader = dataset.CreateDataReader())
                {
                    outputs = outputs ?? new List<dynamic>();
                    returnValues = returnValues ?? new List<dynamic>();

                    foreach (var parameter in parameters.Where(p => p.Item4 != ParameterDirection.Input))
                    {
                        dynamic dto = new System.Dynamic.ExpandoObject();
                        ((IDictionary<String, Object>)dto).Add(parameter.Item1, GetParameterValue(command, parameter.Item1));

                        if (parameter.Item4 == System.Data.ParameterDirection.ReturnValue)
                            returnValues.Add(dto);
                        else
                            outputs.Add(dto);
                    }

                    while (dataTableReader.Read())
                    {
                        dynamic dto = new System.Dynamic.ExpandoObject();
                        for (int i = 0; i < dataTableReader.FieldCount; i++)
                        {
                            ((IDictionary<String, Object>)dto).Add(dataTableReader.GetName(i), dataTableReader[i]);
                        }
                        yield return dto;
                    }
                }
            }
        }
    }
}
