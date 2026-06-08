using ProductionCar.Models;

namespace ProductionCar.Services
{
    public class PlanningService
    {
        private readonly string[] _dayNames =
        {
            "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday", "Sunday"
        };

        public List<PlanningDetail> Distribute(List<int> dailyInput)
        {
            var result = new List<PlanningDetail>();

            // mengecualikan holiday (0) lalu hitung total produksi dan hari asep kerja
            var activeDays = dailyInput
                .Select((value, index) => new { value, index })
                .Where(x => x.value > 0)
                .ToList();

            if (activeDays.Count == 0)
                return BuildEmptyDays(dailyInput);

            int total = activeDays.Sum(x => x.value);
            int totalActiveDays = activeDays.Count;
            int basePerDay = total / totalActiveDays;
            int remainder = total % totalActiveDays;

            // sort untuk menentukan prioritas hari untuk membagi sisa (remainder)
            var priorityIndexes = activeDays
                .OrderByDescending(x => x.value)
                .Take(remainder)
                .Select(x => x.index)
                .ToHashSet();

            // bagi sama rata
            for (int i = 0; i < dailyInput.Count; i++)
            {
                bool isHoliday = dailyInput[i] == 0;
                int distributedValue = 0;

                if (!isHoliday)
                    distributedValue = priorityIndexes.Contains(i)
                        ? basePerDay + 1
                        : basePerDay;

                result.Add(new PlanningDetail
                {
                    DayName = _dayNames[i],
                    DayOrder = i + 1,
                    OriginalPlanning = dailyInput[i],
                    DistributedPlanning = distributedValue,
                    IsHoliday = isHoliday
                });
            }

            return result;
        }

        public PlanningHeader BuildHeader(List<PlanningDetail> details)
        {
            return new PlanningHeader
            {
                InputDate = DateTime.Now,
                TotalActiveDays = details.Count(x => !x.IsHoliday),
                TotalProduction = details.Sum(x => x.OriginalPlanning),
                Note = "Auto distributed",
                CreatedAt = DateTime.Now
            };
        }

        private List<PlanningDetail> BuildEmptyDays(List<int> dailyInput)
        {
            return dailyInput.Select((value, index) => new PlanningDetail
            {
                DayName = _dayNames[index],
                DayOrder = index + 1,
                OriginalPlanning = value,
                DistributedPlanning = 0,
                IsHoliday = true
            }).ToList();
        }
    }
}