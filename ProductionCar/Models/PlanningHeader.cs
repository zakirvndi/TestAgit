using System.ComponentModel.DataAnnotations;

namespace ProductionCar.Models
{
    public class PlanningHeader
    {
        [Key]
        public int PlanningID { get; set; }
        public DateTime InputDate { get; set; } = DateTime.Now;
        public int TotalActiveDays { get; set; }
        public int TotalProduction { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<PlanningDetail> Details { get; set; } = new List<PlanningDetail>();
    }
}