using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Test.Models
{
    [PrimaryKey(nameof(pId), nameof(cId), nameof(appointmentId))]
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appointmentId { get; set; }

        [Required]
        public DateTime appointmentDate { get; set; }

        [Required]
        public int slotNumber { get; set; }

        [ForeignKey("Patient")]
        public int pId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Clinic")]
        public int cId { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
