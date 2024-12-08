using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Test.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pId { get; set; }

        [Required]
        [MaxLength(50)]
        public string pName { get; set; }

        public int age { get; set; }

        [MaxLength(7)]
        public string gender {  get; set; }

        [InverseProperty("Patient")]
        [JsonIgnore]
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
