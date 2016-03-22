using System;
using System.Collections.Generic;
using Shared;

namespace TaskTools.Models
{
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
