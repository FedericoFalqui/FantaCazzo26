
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantaCazzo26.Models

{
    [Table("acquisitions")]
    public class Acquisition
    {
        [Key]
        public long Id { get; set; }


        public long SquadId { get; set; }
        public Squad Squad { get; set; } = null!;


        public long PlayerId { get; set; }
        public Player Player { get; set; } = null!;


        public int AcquisitionPrice { get; set; }
    }
}