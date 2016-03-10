using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    class RoutineViewModel
    {
        private Routine routine;
        private ICommand updateRoutine;

        public string Text { get; set; }
        public int Workload { get; set; }
        public Category Category { get; set; }
        public Stage Stage { get; set; }
        public RepeatType Repeated { get; set; }
        public int DueShift { get; set; }
        public int ValidShift { get; set; }

        public IEnumerable<Category> Categories
        {
            get
            {
                // TODO fix
                return Enum.GetValues(typeof(Category)).Cast<Category>();
            }
        }

        public IEnumerable<Stage> Stages
        {
            get
            {
                // TODO fix
                return Enum.GetValues(typeof(Stage)).Cast<Stage>();
            }
        }

        public IEnumerable<RepeatType> RepeatTypes
        {
            get
            {
                // TODO fix
                return Enum.GetValues(typeof(RepeatType)).Cast<RepeatType>();
            }
        }

        public ICommand UpdateRoutine
        {
            get
            {
                return updateRoutine ??
                (updateRoutine = new DelegateCommand(() =>
                {
                    routine.Text = Text;
                    routine.Workload = Workload;
                    routine.Category = Category;
                    routine.Stage = Stage;
                    routine.Repeated = Repeated;
                    routine.DueShift = DueShift;
                    routine.ValidShift = ValidShift;

                    routine.Update();
                }));
            }
        }

        public RoutineViewModel(): this(new Routine
        {
            Category = Category.Business,
            Stage = Stage.Today,
            Repeated = RepeatType.Day
        })
        { /* Empty */}

        public RoutineViewModel(Routine routine)
        {
            this.routine = routine;
            Text = routine.Text;
            Workload = routine.Workload;
            Category = routine.Category;
            Stage = routine.Stage;
            Repeated = routine.Repeated;
            DueShift = routine.DueShift;
            ValidShift = routine.ValidShift;
        }
    }
}
