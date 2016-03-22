using Prism.Mvvm;
using System.Collections.Generic;
using TaskTools.Data;
using System;
using System.Linq;
using System.Timers;

namespace TaskTools.Models
{
    public class TasksCore : BindableBase
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
        public List<Routine> Routines { get; private set; }
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
                FinishedPool.Clear();
                Routines.Clear();
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
                    foreach (Routine item in storage.GetRoutines())
                    {
                        Routines.Add(item);
                    }
                }
                OnPropertyChanged(() => Pool);
                OnPropertyChanged(() => FinishedPool);
                OnPropertyChanged(() => Routines);
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
            if (storage == null) return;

            if (storage.DeleteTask(task))
            {
                Pool.Remove(task);
                OnPropertyChanged(() => Pool);
            }
        }

        public void DeleteTasksUpTo(DateTime day)
        {
            if (storage == null) return;

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

        internal void UpdateRoutine(Routine routine)
        {
            if (storage == null) return;

            if (routine.Id == null)
            {
                if (storage.SaveRoutine(routine))
                {
                    Routines.Add(routine);
                    OnPropertyChanged(() => Routines);
                }
            }
            else
            {
                if (storage.UpdateRoutine(routine))
                {
                    OnPropertyChanged(() => Routines);
                }
            }
        }

        internal void DeleteRoutine(Routine routine)
        {
            if (storage == null) return;

            if (storage.DeleteRoutine(routine))
            {
                Routines.Remove(routine);
                OnPropertyChanged(() => Routines);
            }
        }

        private void GenerateTasksFromRoutines(object sender, ElapsedEventArgs e)
        {
            if (storage == null) return;

            foreach (Routine routine in Routines)
            {
                routine.Evaluate();
            }
        }

        private TasksCore()
        {
            Pool = new List<TDTask>();
            FinishedPool = new List<TDTask>();
            Routines = new List<Routine>();

            Timer routineTimer = new Timer(1000 * 60);
            routineTimer.Enabled = true;
            routineTimer.Elapsed += GenerateTasksFromRoutines;
            routineTimer.Start();
        }
    }
}
