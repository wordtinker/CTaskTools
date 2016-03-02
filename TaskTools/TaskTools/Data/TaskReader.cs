using System.Collections.Generic;
using TaskTools.Models;

namespace TaskTools.Data
{
    abstract class TaskReader
    {
        public abstract string Extension { get; }

        public abstract void InitializeFile(string fileName);
        public abstract void LoadFile(string fileName);
        public abstract List<TDTask> GetTasks();
        public abstract List<TDTask> GetFinishedTasks();
        public abstract void SaveTask(TDTask newTask);
        public abstract void UpdateTask(TDTask task);
        public abstract void DeleteTask(TDTask task); 
    }
}
