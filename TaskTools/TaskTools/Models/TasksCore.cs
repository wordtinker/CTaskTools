using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTools.Data;
using TaskTools.Models;

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
            if (storage != null)
            {
                if (task.Id == null)
                {
                    storage.SaveTask(task);
                    Pool.Add(task);
                }
                else
                {
                    storage.UpdateTask(task);
                }
                OnPropertyChanged(() => Pool);
            }
        }

        private TasksCore()
        {
            Pool = new List<TDTask>();
        }
    }
}
