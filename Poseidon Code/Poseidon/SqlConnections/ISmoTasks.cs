using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Poseidon.SqlConnections
{
    public interface ISmoTasks
    {
        IEnumerable<string> SqlServers { get; }
        List<string> GetDatabases(SqlConnectionString connectionString);
        List<DatabaseTable> GetTables(SqlConnectionString connectionString);
    }
}
