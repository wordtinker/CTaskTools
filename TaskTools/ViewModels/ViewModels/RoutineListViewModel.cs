using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    public class RoutineListViewModel : BindableBase
    {
        private ICommand createRoutine;
        private IUIRoutineListService windowService;

        public ICommand CreateRoutine
        {
            get
            {
                return createRoutine ??
                (createRoutine = new DelegateCommand(() =>
                {
                    windowService.CreateEditor();
                }));
            }
        }

        public IEnumerable<RoutineViewModel> Routines {
            get
            {
                return TasksCore.Instance.Routines.Select(r => new RoutineViewModel(r));
            }
        }

        public void EditRoutine(RoutineViewModel routineVM)
        {
            windowService.CreateEditor(new RoutineViewModel(routineVM));
        }

        public RoutineListViewModel(IUIRoutineListService windowService)
        {
            this.windowService = windowService;
            TasksCore.Instance.PropertyChanged += (sender, e) =>
            {
                // Raise every property
                OnPropertyChanged(string.Empty);
            };
        }
    }
}
