using System.Collections.Generic;
using TaskTools.Models;

namespace TaskTools.Data
{
    abstract class TaskReader
    {
        public abstract string Extension { get; }

        public abstract bool InitializeFile(string fileName);
        public abstract bool LoadFile(string fileName);
        public abstract List<TDTask> GetTasks();
        public abstract List<TDTask> GetFinishedTasks();
        public abstract bool SaveTask(TDTask newTask);
        public abstract bool UpdateTask(TDTask task);
        public abstract void DeleteTask(TDTask task); 
    }
}
