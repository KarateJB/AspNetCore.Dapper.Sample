using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Dapper.WebApi.Models.Config
{
    public class AppSettings
    {
        public ConnectionStringOptions ConnectionStrings { get; set; }
        public SyncDateOptions SyncDates { get; set; }
    }

    public class ConnectionStringOptions
    {
        public string Demo_PG { get; set; }
        public string Demo_MSSQL { get; set; }
    }

    public class SyncDateOptions
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
