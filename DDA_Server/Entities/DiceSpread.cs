using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dice.Entities
{
   public class DiceSpread
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int D2 { get; set; }
        public int D4 { get; set; }
        public int D6 { get; set; }
        public int D8 { get; set; }
        public int D10_100 { get; set; }
        public int D12 { get; set; }
        public int D20 { get; set; }

        public string? Interpretation { get; set; }

        public DateTime Date { get; set; }
    }
}