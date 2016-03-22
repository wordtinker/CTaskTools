using System.Collections.Generic;
using TaskTools.Models;

namespace TaskTools.Data
{
    public abstract class TaskReader
    {
        public abstract string Extension { get; }

        public abstract bool InitializeFile(string fileName);
        public abstract bool LoadFile(string fileName);
        public abstract List<TDTask> GetTasks();
        public abstract List<TDTask> GetFinishedTasks();
        public abstract bool SaveTask(TDTask newTask);
        public abstract bool UpdateTask(TDTask task);
        public abstract bool DeleteTask(TDTask task);
        public abstract List<Routine> GetRoutines();
        public abstract bool SaveRoutine(Routine newRoutine);
        public abstract bool UpdateRoutine(Routine routine);
        public abstract bool DeleteRoutine(Routine routine);
    }
}
