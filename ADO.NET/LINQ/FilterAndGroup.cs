public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public double Salary { get; set; }
}
class FilterAndGroup
{
    public static void main()
    {
        List<Employee> list = new List<Employee>()
        {
            new Employee() { Id = 1, Name = "Alice", Department = "HR", Salary = 50000 },
            new Employee() { Id = 2, Name = "Bob", Department = "IT", Salary = 60000 },
            new Employee() { Id = 3, Name = "Charlie", Department = "HR", Salary = 55000 },
            new Employee() { Id = 4, Name = "David", Department = "IT", Salary = 65000 },
            new Employee() { Id = 5, Name = "Eve", Department = "Finance", Salary = 70000 }
        };
        var result = list.Where(emp => emp.Salary > 50000).GroupBy(emp => emp.Department);
        foreach(var group in result)
        {
            Console.WriteLine($"Department: {group.Key}");
            foreach (var emp in group)
            {
                Console.WriteLine($"  Name: {emp.Name}, Salary: {emp.Salary}");
            }
        }
    }
}