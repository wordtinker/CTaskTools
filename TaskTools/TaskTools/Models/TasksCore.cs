using Prism.Mvvm;
using System.Collections.Generic;
using TaskTools.Data;
using System;
using System.Linq;

namespace TaskTools.Models
{
    class TasksCore : BindableBase
    {

        private static readonly TasksCore instance = new TasksCore();
        private TaskReader storage;

        public static TasksCore Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Pool of ToDo Items with valid date starting from Today.
        /// </summary>
        public List<TDTask> Pool { get; private set; }
        public List<TDTask> FinishedPool { get; private set; }
        public TaskReader Storage
        {
            get
            {
                return storage;
            }
            set
            {
                // Clear Pool
                Pool.Clear();
                // Set new storage and load tasks.
                storage = value;
                if (storage != null)
                {
                    foreach (TDTask task in storage.GetTasks())
                    {
                        Pool.Add(task);
                    }
                    foreach (TDTask task in storage.GetFinishedTasks())
                    {
                        FinishedPool.Add(task);
                    }
                }
                OnPropertyChanged(() => Pool);
                OnPropertyChanged(() => FinishedPool);
            }
        }

        internal void UpdateTask(TDTask task)
        {
            if (storage == null) return;

            if (task.Id == null)
            {
                if (storage.SaveTask(task))
                {
                    Pool.Add(task);
                    OnPropertyChanged(() => Pool);
                }
            }
            else
            {
                if (storage.UpdateTask(task))
                {
                    OnPropertyChanged(() => Pool);
                }
            }
        }

        internal void DeleteTask(TDTask task)
        {
            if (storage.DeleteTask(task))
            {
                Pool.Remove(task);
                OnPropertyChanged(() => Pool);
            }
        }

        internal void DeleteTasksUpTo(DateTime day)
        {
            var obsoleteTasks =
                (from t in FinishedPool
                where t.Finish <= day ||
                      t.ValidTill <= day
                 select t).ToList();
            obsoleteTasks.ForEach(task =>
            {
                if (storage.DeleteTask(task))
                {
                    FinishedPool.Remove(task);
                    OnPropertyChanged(() => FinishedPool);
                }
            });
        }

        private TasksCore()
        {
            Pool = new List<TDTask>();
            FinishedPool = new List<TDTask>();
        }
    }
}
