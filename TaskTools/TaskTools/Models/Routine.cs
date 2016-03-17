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
        _11 = 111,
        _12 = 112,
        _13 = 113,
        _14 = 114,
        _15 = 115,
        _16 = 116,
        _17 = 117,
        _18 = 118,
        _19 = 119,
        _20 = 120,
        _21 = 121,
        _22 = 122,
        _23 = 123,
        _24 = 124,
        _25 = 125,
        _26 = 126,
        _27 = 127,
        _28 = 128,
        _29 = 129,
        _30 = 130, 
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
        public DateTime? LastGenerated { get; set; }

        internal void Update()
        {
            TasksCore.Instance.UpdateRoutine(this);
        }

        internal void Delete()
        {
            TasksCore.Instance.DeleteRoutine(this);
        }

        public void Evaluate()
        {
            DateTime now = DateTime.Now;
            List<DateTime> plannedDates = new List<DateTime>();
            DateTime lastGen = LastGenerated ?? now.AddDays(-1);

            for (DateTime date = lastGen.AddDays(1).Date; date <= now.Date; date = date.AddDays(1).Date)
            {
                if (Repeated >= RepeatType.Sunday && Repeated <= RepeatType.Saturday)
                {
                    if ((int)date.DayOfWeek == (int)Repeated)
                    {
                        plannedDates.Add(date);
                    }
                }
                else if (Repeated == RepeatType.Day)
                {
                    plannedDates.Add(date);
                }
                else if (Repeated == RepeatType.LastDay)
                {
                    if (date.Day == DateTime.DaysInMonth(date.Year, date.Month))
                    {
                        plannedDates.Add(date);
                    }
                }
                else // days of month
                {
                    if (date.Day == (int)Repeated - 100)
                    {
                        plannedDates.Add(date);
                    }
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

            LastGenerated = taskDate;
            Update();
        }
    }
}
