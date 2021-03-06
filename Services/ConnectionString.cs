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
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = MaturitniCetba; Integrated Security = True; Connect Timeout = 30;   Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return connectionString;
        }
    }
}
