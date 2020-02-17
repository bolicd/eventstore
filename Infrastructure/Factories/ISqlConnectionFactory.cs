using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Factories
{
    public interface ISqlConnectionFactory
    {
        SqlConnection SqlConnection();
    }
}
