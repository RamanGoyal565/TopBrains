using System.ComponentModel.DataAnnotations;

namespace ConcurencyCheck.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public double Salary { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}