using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.Models
{
    public class Departement
    {
        public Departement(int id, string name, int divisionId)
        {
            Id = id;
            Name = name;
            DivisionId = divisionId;
        }

        public Departement()
        {

        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("DivisionId")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }  // reference property                                                       nanti bisa akses Division.Id Divison.Name 
    }
}
