using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.DTOs.SubTask;
using TaskManagement.MVVM.ViewModels.SubTasks;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;
using static TaskManagement.Helpers.Messages.SubTaskMessages;

namespace TaskManagement.MVVM.Views.SubTask;

public partial class SubTasksPage : ContentPage
{
    private SubTaskDetails _bottomSheet;
    private readonly ISubTaskService _subTaskService;
    private readonly IMainTaskService _mainTaskService;
    private readonly Guid _mainTaskId;

    public SubTasksPage(Guid mainTaskId, 
        ISubTaskService subTaskService, 
        IMainTaskService mainTaskService)
    {
        _subTaskService = subTaskService;
        _mainTaskId = mainTaskId;
        _mainTaskService = mainTaskService;

        InitializeComponent();
        BindingContext = new SubTaskViewModel(subTaskService, mainTaskId);

        WeakReferenceMessenger.Default.Register<GetAllSubTasksMessage>(this, (r, message) =>
        {
            GetAllSubTasks();
        });

        WeakReferenceMessenger.Default.Register<OpenEditSubTaskFormMessage>(this, (r, message) =>
        {
            OpenEditSubTask(message.Value);
        });
    }

    protected override bool OnBackButtonPressed()
    {
        DismissBottomSheet();
        return false;
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        DismissBottomSheet();
        await Navigation.PushAsync(new AddEditSubTask(_subTaskService, null, _mainTaskId));
    }

    private void searchSubTasks_TextChanged(object sender, TextChangedEventArgs e)
    {
        DismissBottomSheet();

        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            var viewModel = BindingContext as SubTaskViewModel;
            viewModel?.SearchSubTasks(e.NewTextValue);
        }
    }

    private void OnTaskSelected(object sender, SelectionChangedEventArgs e)
    {
        DismissBottomSheet();

        var collectionView = sender as CollectionView;
        if (collectionView != null)
        {
            collectionView.SelectedItem = null;
        }

        var subTask = (SubTaskDTO)e.CurrentSelection.FirstOrDefault();
        if (subTask == null) return;

        _bottomSheet = new SubTaskDetails(subTask.Id, _subTaskService);
        _bottomSheet.HasHandle = true;
     
        _bottomSheet.ShowAsync(Window);
    }

    private void DismissBottomSheet()
    {
        if (_bottomSheet != null && _bottomSheet.HasHandle)
            _bottomSheet.DismissAsync();
    }

    private async void GetAllSubTasks()
    {
        var binding = (SubTaskViewModel)BindingContext;
        await binding.GetAllSubTasks();
    }

    private void OpenEditSubTask(Guid subTaskId)
    {
        Navigation.PushAsync(new AddEditSubTask(_subTaskService, subTaskId, _mainTaskId));
    }

    private void btnInfo_Clicked(object sender, EventArgs e)
    {
        var popup = new CustomPopup(new MainTaskInfo(_mainTaskId, _mainTaskService));
        this.ShowPopup(popup);
    }
}