﻿using System;
using Shared;

namespace TaskTools.Models
{
    public class TDTask
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public DateTime Incoming { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Due { get; set; }
        public DateTime? ValidTill { get; set; }
        public DateTime? Finish { get; set; }
        public Category Category { get; set; }
        public bool Completed { get; set; }
        public Stage Stage { get; set; }
        public bool Routine { get; set; }
        public int Workload { get; set; }

        public void Update()
        {
            TasksCore.Instance.UpdateTask(this);
        }

        public void Delete()
        {
            TasksCore.Instance.DeleteTask(this);
        }
    }
}
