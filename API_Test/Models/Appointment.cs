using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual Patient Patient { get; set; }

        [ForeignKey("Clinic")]
        public int cId { get; set; }
        [JsonIgnore]
        public virtual Clinic Clinic { get; set; }
    }
}
