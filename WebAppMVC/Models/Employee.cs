using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public Employee(int id, string fullName, string email, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public Employee()
        {

        }
    }
}
