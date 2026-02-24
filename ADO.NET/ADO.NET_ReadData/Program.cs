using Microsoft.Data.SqlClient;
class Program
{
    public static void Main()
    {
        string connectionString =
                "Server=localhost\\SQLEXPRESS;" +
                "Database=TopBrains;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;";
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from Product";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Price: {reader["Price"]}");
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}