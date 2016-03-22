using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using TaskTools.Models;
using Shared;

namespace TaskTools.ViewModels
{
    abstract class TaskPanel: BindableBase
    {
        protected TasksCore core;

        public abstract string TabTitle { get; }
        public IEnumerable<TDTaskViewModel> Business
        {
            get
            {
                return SelectForCategory(Category.Business);
            }
        }
        public IEnumerable<TDTaskViewModel> Environment
        {
            get
            {
                return SelectForCategory(Category.Environment);
            }
        }
        public IEnumerable<TDTaskViewModel> FnF
        {
            get
            {
                return SelectForCategory(Category.FriendsFamily);
            }
        }
        public IEnumerable<TDTaskViewModel> Fun
        {
            get
            {
                return SelectForCategory(Category.Fun);
            }
        }
        public IEnumerable<TDTaskViewModel> Health
        {
            get
            {
                return SelectForCategory(Category.Health);
            }
        }
        public IEnumerable<TDTaskViewModel> Money
        {
            get
            {
                return SelectForCategory(Category.Money);
            }
        }
        public IEnumerable<TDTaskViewModel> SelfDevelopment
        {
            get
            {
                return SelectForCategory(Category.SelfDevelopment);
            }
        }

        public IEnumerable<TDTaskViewModel> Incoming
        {
            get
            {
                IEnumerable<TDTaskViewModel> tasks =
                    from t in core.Pool
                    where t.Stage == Stage.Incoming
                    select new TDTaskViewModel(t);
                return tasks;
            }
        }

        public TaskPanel()
        {
            this.core = TasksCore.Instance;
            this.core.PropertyChanged += (sender, e) =>
            {
                // Raise every property
                OnPropertyChanged(string.Empty);
            };
        }

        protected abstract IEnumerable<TDTaskViewModel> SelectForCategory(Category cat);
    }

    internal class TodayTasks : TaskPanel
    {
        public TodayTasks() : base() {/* empty */ }

        public override string TabTitle
        {
            get { return "Today"; }
        }

        protected override IEnumerable<TDTaskViewModel> SelectForCategory(Category cat)
        {
            IEnumerable<TDTaskViewModel> tasks =
                    from t in core.Pool
                    where t.Category == cat &&
                          (t.Stage == Stage.Today ||
                          t.Completed == true ||
                          t.Due <= DateTime.Today ||
                          t.Start <= DateTime.Today)
                    select new TDTaskViewModel(t);
            return tasks;
        }
    }

    internal class Backlog : TaskPanel
    {
        public Backlog() : base() {/* Empty */}

        public override string TabTitle
        {
            get { return "Backlog"; }
        }

        protected override IEnumerable<TDTaskViewModel> SelectForCategory(Category cat)
        {
            IEnumerable<TDTaskViewModel> tasks =
                    from t in core.Pool
                    where t.Category == cat && 
                          t.Stage == Stage.Backlog
                    select new TDTaskViewModel(t);
            return tasks;
        }
    }

    internal class Someday : TaskPanel
    {
        public Someday() : base() {/* Empty */}

        public override string TabTitle
        {
            get { return "Someday"; }
        }

        protected override IEnumerable<TDTaskViewModel> SelectForCategory(Category cat)
        {
            IEnumerable<TDTaskViewModel> tasks =
                    from t in core.Pool
                    where t.Category == cat && 
                          t.Stage == Stage.Someday
                    select new TDTaskViewModel(t);
            return tasks;
        }
    }

    internal class Waiting : TaskPanel
    {
        public Waiting() : base() {/* Empty */}

        public override string TabTitle
        {
            get { return "Waiting"; }
        }

        protected override IEnumerable<TDTaskViewModel> SelectForCategory(Category cat)
        {
            IEnumerable<TDTaskViewModel> tasks =
                    from t in core.Pool
                    where t.Category == cat && 
                          t.Stage == Stage.Waiting
                    select new TDTaskViewModel(t);
            return tasks;
        }
    }
}
