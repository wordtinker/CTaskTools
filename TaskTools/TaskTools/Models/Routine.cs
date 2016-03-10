using System;
using System.Collections.Generic;

namespace TaskTools.Models
{
    public enum RepeatType
    {
        LastDay = -1,
        Sunday = DayOfWeek.Sunday, // 0
        Monday = DayOfWeek.Monday, // 1
        Tuesday = DayOfWeek.Tuesday, // 2
        Wednesday = DayOfWeek.Wednesday, // 3
        Thursday = DayOfWeek.Thursday, // 4
        Friday = DayOfWeek.Friday, //5
        Saturday = DayOfWeek.Saturday, // 6
        Day = 7,
        FirstDay = 101,
        _1 = 101,
        _2 = 102,
        _3 = 103,
        _4 = 104,
        _5 = 105,
        _6 = 106,
        _7 = 107,
        _8 = 108,
        _9 = 109,
        _10 = 110,
        // TODO
        _31 = 131
    }

    public class Routine
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public int Workload { get; set; }
        public Category Category { get; set; }
        public Stage Stage { get; set; }
        public RepeatType Repeated { get; set; }
        public int DueShift { get; set; }
        public int ValidShift { get; set; }
        public DateTime LastGenerated { get; set; }

        internal void Update()
        {
            TasksCore.Instance.UpdateRoutine(this);
        }

        public void Evaluate()
        {
            DateTime now = DateTime.Now;
            List<DateTime> plannedDates = new List<DateTime>();

            for (DateTime date = LastGenerated.AddDays(1); date <= now; date = date.AddDays(1))
            {
                if (Repeated >= RepeatType.Sunday && Repeated <= RepeatType.Saturday)
                {
                    if ((int)date.DayOfWeek == (int)Repeated)
                    {
                        plannedDates.Add(date);
                    }
                    // TODO test
                }
                else if (Repeated == RepeatType.Day)
                {
                    plannedDates.Add(date);
                    // TODO test
                }
                else if (Repeated == RepeatType.LastDay)
                {
                    // TODO
                }
                else // days of month
                {
                    // TODO
                }
            }

            plannedDates.ForEach((d) => GenerateTask(d));
        }

        private void GenerateTask(DateTime taskDate)
        {
            TDTask task = new TDTask
            {
                Routine = true,
                Category = this.Category,
                Stage = this.Stage,
                Text = this.Text,
                Workload = this.Workload,
                Incoming = DateTime.Now,
                Start = taskDate,
                Due = taskDate.AddDays(DueShift),
                ValidTill = taskDate.AddDays(ValidShift)
            };
            task.Update();
        }
    }
}
