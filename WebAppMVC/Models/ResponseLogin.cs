using Microsoft.CodeAnalysis.Operations;

namespace WebAppMVC.Models
{
    public class ResponseLogin
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ResponseLogin(string fullname, string email, string role)
        {
            this.FullName = fullname;
            this.Email = email;
            this.Role = role;
        }

        public ResponseLogin()
        {

        }
    }
}
