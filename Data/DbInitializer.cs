using Dapper;

namespace UnitessTestApp.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(DapperContext context)
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory()) + "\\Data\\DBInit.sql";

            if (File.Exists(directory))
            {
                FileInfo file = new(directory);

                string script = file.OpenText().ReadToEnd();

                using var connection = context.CreateConnection();
                connection.Execute(script);

                file.OpenText().Close();
            }
        }
    }
}
