namespace WebAppMVC.Models
{
    public class ResponseRegister
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }

        public ResponseRegister(string fullName, string email, DateTime birthDate, string password)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Password = password;
        }

        public ResponseRegister()
        {

        }
    }
}
