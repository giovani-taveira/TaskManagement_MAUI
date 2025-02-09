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

    public SubTasksPage(ISubTaskService subTaskService, 
        IMainTaskService mainTaskService)
    {
        _subTaskService = subTaskService;
        _mainTaskService = mainTaskService;

        InitializeComponent();
        BindingContext = new SubTaskViewModel(subTaskService);

        ManageEvents();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Shell.Current.Navigating += Shell_Navigating;
    }

    private void Shell_Navigating(object sender, ShellNavigatingEventArgs e) => DismissBottomSheet();

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        DismissBottomSheet();
        var viewModel = (SubTaskViewModel)BindingContext;
        await Shell.Current.GoToAsync($"addEditSubTask?subTaskId={null}&mainTaskId={viewModel.MainTaskId}");
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

    private async void OpenEditSubTask(Guid subTaskId)
    {
        var viewModel = (SubTaskViewModel)BindingContext;
        await Shell.Current.GoToAsync($"addEditSubTask?subTaskId={subTaskId.ToString()}&mainTaskId={viewModel.MainTaskId}");
    }

    private void btnInfo_Clicked(object sender, EventArgs e)
    {
        var viewModel = (SubTaskViewModel)BindingContext;
        var popup = new CustomPopup(new MainTaskInfo(Guid.Parse(viewModel.MainTaskId), _mainTaskService));
        this.ShowPopup(popup);
    }

    private void ManageEvents()
    {
        WeakReferenceMessenger.Default.Unregister<GetAllSubTasksMessage>(this);
        WeakReferenceMessenger.Default.Unregister<OpenEditSubTaskFormMessage>(this);

        WeakReferenceMessenger.Default.Register<GetAllSubTasksMessage>(this, (r, message) =>
        {
            GetAllSubTasks();
        });

        WeakReferenceMessenger.Default.Register<OpenEditSubTaskFormMessage>(this, (r, message) =>
        {
            OpenEditSubTask(message.Value);
        });
    }
}