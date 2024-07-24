using System.ComponentModel.DataAnnotations;

namespace DataMigration.Models
{
    public class MigrationHistory
    {

        [Key]
        public int MigrationHistoryId { get; set; }

        [Required]
        public required string Description { get; set; }


        [Required]
        public DateTime DateTime { get; set; }= DateTime.UtcNow;



    }
}
