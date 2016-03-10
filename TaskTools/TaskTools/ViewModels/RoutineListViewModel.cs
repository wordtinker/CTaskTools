using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    class RoutineListViewModel : BindableBase
    {
        private ICommand createRoutine;

        public ICommand CreateRoutine
        {
            get
            {
                return createRoutine ??
                (createRoutine = new DelegateCommand(() =>
                {
                    // TODO Fix. MVVM break.
                    Views.RoutineWindow window = new Views.RoutineWindow();
                    window.DataContext = new RoutineViewModel();
                    window.Show();
                    // TODO CanExecute opened file
                }));
            }
        }

        public IEnumerable<RoutineViewModel> Routines {
            get
            {
                // TODO
                return TasksCore.Instance.Routines.Select(r => new RoutineViewModel(r));
            }
        }
    }
}
