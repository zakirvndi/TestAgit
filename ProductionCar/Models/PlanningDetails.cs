using System.ComponentModel.DataAnnotations;

namespace ProductionCar.Models
{
    public class PlanningDetail
    {
        [Key]
        public int DetailID { get; set; }
        public int PlanningID { get; set; }
        public string DayName { get; set; } = "";
        public int DayOrder { get; set; }
        public int OriginalPlanning { get; set; }
        public int DistributedPlanning { get; set; }
        public bool IsHoliday { get; set; }
        public PlanningHeader Header { get; set; } = null!;
    }
}