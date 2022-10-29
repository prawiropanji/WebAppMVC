using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.Models
{
    public class User
    {
        public User(int id, string password, int roleId, int employeId)
        {
            this.Id = id;
            this.Password = password;
            this.RoleId = roleId;
            this.EmployeeId = employeId;
        }

        public User()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Password { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId{ get; set; }
        public Employee Employee { get; set; }


    }
}
