using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    public class TDTaskViewModel
    {
        private TDTask task;
        private ICommand updateTask;
        private ICommand deleteTask;

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

        public IEnumerable<Category> Categories
        {
            get
            {
                return Enum.GetValues(typeof(Category)).Cast<Category>();
            }
        }

        public IEnumerable<Stage> Stages
        {
            get
            {
                return Enum.GetValues(typeof(Stage)).Cast<Stage>();
            }
        }

        public ICommand UpdateTask
        {
            get
            {
                return updateTask ??
                (updateTask = new DelegateCommand(() =>
                {
                    task.Text = Text;
                    task.Incoming = Incoming;
                    task.Start = Start;
                    task.Due = Due;
                    task.ValidTill = ValidTill;
                    task.Finish = Finish;
                    task.Category = Category;
                    task.Completed = Completed;
                    task.Stage = Stage;
                    task.Routine = Routine;
                    task.Workload = Workload;

                    task.Update();
                }, () =>
                {
                    // TODO
                    return true;
                }
                ));
            }
        }

        public ICommand DeleteTask
        {
            get
            {
                return deleteTask ??
                (deleteTask = new DelegateCommand(() =>
                {
                    task.Delete();
                }));
            }
        }

        public TDTaskViewModel() : this(new TDTask
        {
            Category = Category.Business,
            Stage = Stage.Incoming,
            Incoming = DateTime.Now
        })
        {/* Empty */}

        public TDTaskViewModel(TDTask task)
        {
            this.task = task;
            Text = task.Text;
            Incoming = task.Incoming;
            Start = task.Start;
            Due = task.Due;
            ValidTill = task.ValidTill;
            Finish = task.Finish;
            Category = task.Category;
            Completed = task.Completed;
            Stage = task.Stage;
            Routine = task.Routine;
            Workload = task.Workload;
        }
    }
}