using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TaskTools.Data;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private IWindowFactory windowFactory;
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

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = fileHandler.Extension;
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string fileName = saveFileDialog.FileName;
                        if (fileHandler.InitializeFile(fileName) &&
                            fileHandler.LoadFile(fileName))
                        {
                            SaveLastOpenedFile(fileName);
                            core.Storage = fileHandler;
                        }
                        else
                        {
                            MessageBox.Show("Can't create file.");
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

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = fileHandler.Extension;
                    if (openFileDialog.ShowDialog() == true)
                    {
                        if (fileHandler.LoadFile(openFileDialog.FileName))
                        {
                            SaveLastOpenedFile(openFileDialog.FileName);
                            core.Storage = fileHandler;
                        }
                        else
                        {
                            MessageBox.Show("Can't open file.");
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
                    App.Current.Shutdown();
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
                    // TODO
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
                    windowFactory.CreateEditor();
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
                }));
            }
        }

        public ICommand ShowFinished
        {
            get
            {
                return showFinished ??
                (showFinished = new DelegateCommand(() =>
                {
                    MessageBox.Show(string.Format("There are {0} finished tasks.",
                        core.FinishedPool.Count));
                }));
            }
        }

        public void EditTask(TDTaskViewModel taskViewModel)
        {
            windowFactory.CreateEditor(taskViewModel);
        }

        public void LoadLastOpenedFile()
        {
            string fileName = Config.ReadSetting("LastFile");
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
            Config.AddUpdateConfig("LastFile", fileName);
        }

        public MainWindowViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;
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
