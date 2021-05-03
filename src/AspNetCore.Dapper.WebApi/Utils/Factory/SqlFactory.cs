using AspNetCore.Dapper.WebApi.Models.Enum;

namespace AspNetCore.Dapper.WebApi.Utils.Factory
{
    public class SqlFactory
    {
        public static string CreateQueryFolk(EnumDbType dbType)
        {
            string sql = string.Empty;
            switch (dbType)
            {
                case EnumDbType.SqlServer:
                    sql = @"
SELECT Id, Name, Birthday, Address, Phone, EmploymentOn FROM Folks
WHERE
EmploymentOn >= CONVERT(DATETIME, @StartDate, 102)
AND EmploymentOn <= CONVERT(DATETIME, @EndDate, 102)
";
                    break;
                case EnumDbType.Postgres:
                    sql = @"
SELECT ""Id"", ""Name"", ""Birthday"", ""Address"", ""Phone"", ""EmploymentOn"" FROM public.""Folks""
WHERE ""EmploymentOn"" >= to_date(@StartDate, 'YYYY-MM-DD')
AND ""EmploymentOn"" <= to_date(@EndDate, 'YYYY-MM-DD');
";
                    break;
            }

            return sql;
        }

        public static string CreateMergeFolk(EnumDbType dbType)
        {
            string sql = string.Empty;
            switch (dbType)
            {
                case EnumDbType.SqlServer:
                    sql = @"
MERGE INTO Folks a USING
(
    SELECT @Id            as Id,
           @Name          as Name,
           @Birthday      as Birthday,
           @Address       as Address,
           @Phone         as Phone,
           @EmploymentOn  as EmploymentOn
) b ON
(a.Id = b.Id)
WHEN MATCHED THEN UPDATE SET
a.Name = b.Name,
a.Birthday = b.Birthday,
a.Address = b.Address,
a.Phone = b.Phone,
a.EmploymentOn = b.EmploymentOn
WHEN NOT MATCHED THEN INSERT(Id, Name, Birthday, Address, Phone, EmploymentOn)
VALUES(@Id, @Name, @Birthday, @Address, @Phone, @EmploymentOn);
";
                    break;

                case EnumDbType.Postgres:
                    sql = @"
INSERT INTO ""Folks""(""Id"", ""Name"", ""Birthday"", ""Address"", ""Phone"", ""EmploymentOn"")
VALUES(@Id, @Name, @Birthday, @Address, @Phone, @EmploymentOn)
ON CONFLICT (""Id"")
DO UPDATE SET
""Name"" = @Name,
""Birthday"" = @Birthday,
""Address"" = @Address,
""Phone"" = @Phone,
""EmploymentOn"" = @EmploymentOn;
";
                    break;
            }

            return sql;
        }
    }
}
