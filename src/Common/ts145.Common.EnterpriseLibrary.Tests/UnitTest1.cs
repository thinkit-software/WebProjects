using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts145.Common.EnterpriseLibrary.Data;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace ts145.Common.EnterpriseLibrary.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CallProcedureTest_MSSQL()
        {
            IDbExecuteHelper helper = new EFDbExecuteHelper();
                        
            var result = helper.ExecuteDynamicReader("uspGetAddressesByCity",
                new[] {
                    new Tuple<string, object, DbType?, ParameterDirection>("city", "Dallas", DbType.String, ParameterDirection.Input)
                },
                null,
                null);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());            
        }
    }
}
