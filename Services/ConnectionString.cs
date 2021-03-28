using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class ConnectionString
    {

        public static string GetConnectionString()
        {
            string connectionString = @"Data Source=maturitnicetbadbserver.database.windows.net;Initial Catalog=MaturitniCetbaDatabase;User ID=serveradmin;Password=Dontresswow123;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return connectionString;
        }
    }
}
