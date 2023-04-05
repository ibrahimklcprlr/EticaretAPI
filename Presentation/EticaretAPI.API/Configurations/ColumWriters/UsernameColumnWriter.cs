﻿using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace EticaretAPI.API.Configurations.ColumWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
        {

        }
        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
          var(username,value)= logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            return value?.ToString()??null;
        }
    }
}
