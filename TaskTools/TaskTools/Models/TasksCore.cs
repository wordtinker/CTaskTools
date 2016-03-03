using Prism.Mvvm;
using System.Collections.Generic;
using TaskTools.Data;

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
                }
                OnPropertyChanged(() => Pool);
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

        private TasksCore()
        {
            Pool = new List<TDTask>();
        }
    }
}
