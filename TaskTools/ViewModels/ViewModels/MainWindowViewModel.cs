using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TaskTools.Data;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IUIMainWindowService windowService;
        private TasksCore core;
        private string openedFile;
        private ICommand createFile;
        private ICommand openFile;
        private ICommand closeFile;
        private ICommand exit;
        private ICommand showHelp;
        private ICommand createTask;
        private ICommand dropFinished;
        private ICommand showFinished;
        private ICommand showRoutineList;
        
        public List<TaskPanel> Tabs { get; private set; }
        public string OpenedFile
        {
            get { return openedFile; }
            set
            {
                SetProperty(ref openedFile, value);
            }
        }

        public ICommand CreateFile
        {
            get
            {
                return createFile ??
                (createFile = new DelegateCommand(() =>
                {
                    TaskReader fileHandler = new XMLTaskReader();
                    string fileName = windowService.SaveFileDialog(fileHandler.Extension);
                    if (fileName != null)
                    {
                        if (fileHandler.InitializeFile(fileName) &&
                            fileHandler.LoadFile(fileName))
                        {
                            SaveLastOpenedFile(fileName);
                            core.Storage = fileHandler;
                        }
                        else
                        {
                            windowService.ShowMessage("Can't create file.");
                        }
                    }
                }));
            }
        }
        public ICommand OpenFile
        {
            get
            {
                return openFile ??
                (openFile = new DelegateCommand(() =>
                {
                    TaskReader fileHandler = new XMLTaskReader();
                    string fileName = windowService.OpenFileDialog(fileHandler.Extension);
                    if (fileName != null)
                    {
                        if (fileHandler.LoadFile(fileName))
                        {
                            SaveLastOpenedFile(fileName);
                            core.Storage = fileHandler;
                        }
                        else
                        {
                            windowService.ShowMessage("Can't open file.");
                        }
                    }
                }));
            }
        }
        public ICommand CloseFile
        {
            get
            {
                return closeFile ??
                (closeFile = new DelegateCommand(() =>
                {
                    SaveLastOpenedFile(string.Empty);
                    core.Storage = null;
                }));
            }
        }

        public ICommand Exit
        {
            get
            {
                return exit ??
                (exit =  new DelegateCommand(() =>
                {
                    windowService.Shutdown();
                }));
            }
        }

        public ICommand ShowHelp
        {
            get
            {
                return showHelp ??
                (showHelp =new DelegateCommand(() =>
                {
                    string info = string.Format("CTaskTools: {0}\n Iconset by Eleken.",
                        CoreAssembly.Version);
                    windowService.ShowMessage(info);
                }));
            }
        }

        public ICommand CreateTask
        {
            get
            {
                return createTask ??
                (createTask = new DelegateCommand(() =>
                {
                    windowService.CreateEditor();
                }, () =>
                {
                    return !string.IsNullOrEmpty(OpenedFile);
                }).ObservesProperty(() => OpenedFile));
            }
        }

        public ICommand DropFinished
        {
            get
            {
                return dropFinished ??
                (dropFinished = new DelegateCommand<string>((day) =>
                {
                    DateTime today = DateTime.Today;
                    switch (day)
                    {
                        case "Week":
                            int shift = today.DayOfWeek == 0 ? 7 : (int)today.DayOfWeek;
                            DateTime lastSunday = today.AddDays(-shift);
                            core.DeleteTasksUpTo(lastSunday);
                            break;
                        case "Month":
                            DateTime currMonth = new DateTime(today.Year, today.Month, 1);
                            DateTime lastDay = currMonth.AddDays(-1);
                            core.DeleteTasksUpTo(lastDay);
                            break;
                        default:
                            DateTime yesterday = today.AddDays(-1);
                            core.DeleteTasksUpTo(yesterday);
                            break;
                    }
                }, (day) =>
                {
                    return !string.IsNullOrEmpty(OpenedFile);
                }).ObservesProperty(() => OpenedFile));
            }
        }

        public ICommand ShowFinished
        {
            get
            {
                return showFinished ??
                (showFinished = new DelegateCommand(() =>
                {
                    windowService.ShowMessage(string.Format("There are {0} finished tasks.",
                        core.FinishedPool.Count));
                }, () => 
                {
                    return !string.IsNullOrEmpty(OpenedFile);
                }).ObservesProperty(() => OpenedFile));
            }
        }

        public ICommand ShowRoutineList
        {
            get
            {
                return showRoutineList ??
                (showRoutineList = new DelegateCommand(() =>
                {
                    windowService.CreateRoutineList();
                }, () => 
                {
                    return !string.IsNullOrEmpty(OpenedFile);
                }).ObservesProperty(() => OpenedFile));
            }
        }

        public void EditTask(TDTaskViewModel taskViewModel)
        {
            windowService.CreateEditor(new TDTaskViewModel(taskViewModel));
        }

        public void LoadLastOpenedFile()
        {
            string fileName = windowService.GetConfig("LastFile");
            if (fileName != string.Empty)
            {
                TaskReader fileHandler = new XMLTaskReader();
                if (fileHandler.LoadFile(fileName))
                {
                    core.Storage = fileHandler;
                    OpenedFile = fileName;
                }
            }
        }

        public void SaveLastOpenedFile(string fileName)
        {
            OpenedFile = fileName;
            windowService.SetConfig("LastFile", fileName);
        }

        public MainWindowViewModel(IUIMainWindowService windowService)
        {
            this.windowService = windowService;
            core = TasksCore.Instance;
            Tabs = new List<TaskPanel>
            {
                new TodayTasks(),
                new Backlog(),
                new Waiting(),
                new Someday()
            };
            LoadLastOpenedFile();
        }
    }
}
