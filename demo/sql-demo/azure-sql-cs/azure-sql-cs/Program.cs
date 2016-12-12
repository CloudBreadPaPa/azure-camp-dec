using System;
using C = System.Data.SqlClient;

namespace azure_sql_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new C.SqlConnection(
                "Server=tcp:dw-sqlsvr-camp-test.database.windows.net,1433;Initial Catalog=dwazcamptest001;Persist Security Info=False;User ID=konan94;Password=P@ssw0rdP@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))  // 연결 문자열을 복사하고 Azure SQL Database 생성시 지정한 user id와 pwd로 변경. 서버명과 DB명은 자동으로 연결 문자열에 지정
            {
                connection.Open();
                Console.WriteLine("Connected successfully.");

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey(true);
            }
        }
    }
}
