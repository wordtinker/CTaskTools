using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskTools.Models;
using TaskTools.Shared;

namespace TaskTools.ViewModels
{
    class RoutineViewModel
    {
        private Routine routine;
        private ICommand updateRoutine;
        private ICommand deleteRoutine;

        public string Text { get; set; }
        public int Workload { get; set; }
        public Category Category { get; set; }
        public Stage Stage { get; set; }
        public RepeatType Repeated { get; set; }
        public int DueShift { get; set; }
        public int ValidShift { get; set; }

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

        public ICommand DeleteRoutine
        {
            get
            {
                return deleteRoutine ??
                (deleteRoutine = new DelegateCommand(() =>
                {
                    routine.Delete();
                }));
            }
        }

        public RoutineViewModel(): this(new Routine
        {
            Category = Category.Business,
            Stage = Stage.Today,
            Repeated = RepeatType.Day
        })
        {/* Empty */}

        public RoutineViewModel(RoutineViewModel routineVM) : this(routineVM.routine)
        {/* Empty */}

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
