using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TaskTools.Data;
using TaskTools.Models;

namespace TaskTools.ViewModels
{
    class MainWindowViewModel
    {
        private IWindowFactory windowFactory;
        private TasksCore core;
        private ICommand createFile;
        private ICommand openFile;
        private ICommand closeFile;
        private ICommand exit;
        private ICommand showHelp;
        private ICommand createTask;
        

        public List<TaskPanel> Tabs { get; private set; }

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
                        fileHandler.InitializeFile(fileName);
                        fileHandler.LoadFile(fileName);
                        core.Storage = fileHandler;
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
                        fileHandler.LoadFile(openFileDialog.FileName);
                        core.Storage = fileHandler;
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
                    // TODO fix button
                    return core.Storage != null;
                }));
            }
        }

        public void EditTask(TDTaskViewModel taskViewModel)
        {
            windowFactory.CreateEditor(taskViewModel);
        }

        public MainWindowViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;
            core = TasksCore.Instance;
            Tabs = new List<TaskPanel>
            {
                new TodayTasks(core),
                new Backlog(core),
                new Waiting(core),
                new Someday(core)
            };
        }
    }
}
