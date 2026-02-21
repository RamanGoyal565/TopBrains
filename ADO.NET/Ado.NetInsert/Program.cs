using System.Data.SqlClient;
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
}
class Program
{
    public static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Marks = 80 },
            new Student { Id = 2, Name = "Mary", Marks = 90 },
            new Student { Id = 3, Name = "David", Marks = 75 }
        };

        string connectionString =
                "Server=localhost\\SQLEXPRESS;" +
                "Database=TEST;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;";
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Student (Id, Name, Marks) VALUES (@Id, @Name, @Marks)";
                foreach (Student s in students)
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", s.Id);
                        cmd.Parameters.AddWithValue("@Name", s.Name);
                        cmd.Parameters.AddWithValue("@Marks", s.Marks);
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            Console.WriteLine("Inserted Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}