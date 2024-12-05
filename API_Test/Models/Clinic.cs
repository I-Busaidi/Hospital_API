using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Test.Models
{
    public class Clinic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cId { get; set; }

        [Required]
        [MaxLength(30)]
        public string cSpec { get; set; }

        [Required]
        public int numberOfSlots { get; set; } = 20;

        [InverseProperty("Clinic")]
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
