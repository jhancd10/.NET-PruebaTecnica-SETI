using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using SETI.Data.Common;
using SETI.Data.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Core.Helpers
{
    public  class ManualAccessDb : IManualAccessDb
    {
        private readonly string _connectionString;

        public ManualAccessDb(
            IOptions<AppSettings> appSettings)
        {
            _connectionString = appSettings.Value.AdoConnectionString;
        }

        public DataSet GetQueryResult(string consulta)
        {
            DataSet dataSetResponse = new();

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                cnx.Open();

                SqlCommand command = new(consulta, cnx)
                {
                    CommandTimeout = 0
                };

                SqlDataAdapter adapter = new(command);
                adapter.Fill(dataSetResponse);
                
                command.Dispose();
                adapter.Dispose();
                
                cnx.Close();
                cnx.Dispose();
            }
            return dataSetResponse;
        }
    }
}
