using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagement.DTOs.MainTask;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.ViewModels;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;
using static TaskManagement.Helpers.Messages.MainTaskMessages;

namespace TaskManagement.MVVM.Views.MainTask;

public partial class MainTasksPage : ContentPage
{
    private readonly IMainTaskService _mainTaskService;
    private readonly ISubTaskService _subTaskService;
    private CircularProgressDrawable _progressDrawable;
    private string _selectedValue;
    private MainTaskDetails _bottomSheet;
    public MainTasksPage(IMainTaskService mainTaskService, 
        ISubTaskService subTaskService)
    {
        InitializeComponent();

        _mainTaskService = mainTaskService;
        _subTaskService = subTaskService;

        BindingContext = new MainTaskViewModel(_mainTaskService);
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
        await Shell.Current.GoToAsync($"mainTask/addEditMainTask?maintaskid={null}");
    }

    private void btnFilter_Clicked(object sender, EventArgs e)
    {
        DismissBottomSheet();
        var viewModel = BindingContext as MainTaskViewModel;

        var dropdown = new DropdownPopup(new List<string> { StatusEnum.Ativo.ToString(), StatusEnum.Em_Atraso.ToString().Replace("_", " "), StatusEnum.Concluido.ToString(), "Todos" },
                       selectedValue =>
                       {
                           viewModel?.SearchAllMainTasksByStatus(selectedValue);
                       }, "Selecione um Status");

        this.ShowPopup(dropdown);
    }

    private void searchMainTasks_TextChanged(object sender, TextChangedEventArgs e)
    {
        DismissBottomSheet();

        if (string.IsNullOrEmpty((string)e.NewTextValue))
        {
            var viewModel = BindingContext as MainTaskViewModel;
            viewModel?.SearchAllMainTasks();
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

        var task = (MainTaskDTO)e.CurrentSelection.FirstOrDefault();
        if (task == null) return;

        _bottomSheet = new MainTaskDetails(task.Id, _mainTaskService);
        _bottomSheet.HasHandle = true;
        _bottomSheet.ShowAsync(Window);
    }

    private void DismissBottomSheet()
    {
        if(_bottomSheet != null && _bottomSheet.HasHandle)
            _bottomSheet.DismissAsync();
    }

    private async void GetAllMainTasks()
    {
        var binding = (MainTaskViewModel)BindingContext;
        await binding.GetAllMainTasks();
    }

    private async void OpenEditTask(Guid mainTaskId)
    {
        await Shell.Current.GoToAsync($"addEditMainTask?mainTaskId={mainTaskId.ToString()}");
    }

    private async void OpenSubTasksPageTask(Guid mainTaskId)
    {
        await Shell.Current.GoToAsync($"subTasks?mainTaskId={mainTaskId.ToString()}");
    }

    private void ManageEvents()
    {
        WeakReferenceMessenger.Default.Unregister<GetAllMainTasksMessage>(this);
        WeakReferenceMessenger.Default.Unregister<OpenEditFormMessage>(this);
        WeakReferenceMessenger.Default.Unregister<OpenSubtasksPageMessage>(this);

        WeakReferenceMessenger.Default.Register<GetAllMainTasksMessage>(this, (r, message) =>
        {
            GetAllMainTasks();
        });

        WeakReferenceMessenger.Default.Register<OpenEditFormMessage>(this, (r, message) =>
        {
            OpenEditTask(message.Value);
        });

        WeakReferenceMessenger.Default.Register<OpenSubtasksPageMessage>(this, (r, message) =>
        {
            OpenSubTasksPageTask(message.Value);
        });
    }
}