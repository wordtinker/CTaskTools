namespace TaskTools.ViewModels
{
    public interface IUIMainWindowService
    {
        void CreateEditor();
        void CreateEditor(TDTaskViewModel taskVM);
        void CreateRoutineList();
        void ShowMessage(string message);
        string OpenFileDialog(string fileExtension);
        string SaveFileDialog(string fileExtension);
        void SetConfig(string key, string value);
        string GetConfig(string key);
        void Shutdown();
    }

    public interface IUIRoutineListService
    {
        void CreateEditor();
        void CreateEditor(RoutineViewModel routine);
    }
}
