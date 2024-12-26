using CommunityToolkit.Maui.Views;
using TaskManagement.Helpers.Enums;
using TaskManagement.MVVM.ViewModels;
using TaskManagement.MVVM.Views._Components;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.MVVM.Views.MainTask;

public partial class MainTasksPage : ContentPage
{
    private readonly IMainTaskService _mainTaskService;
    private CircularProgressDrawable _progressDrawable;
    private string _selectedValue;
    public MainTasksPage(IMainTaskService mainTaskService)
    {       
        InitializeComponent();

        _mainTaskService = mainTaskService;

        BindingContext = new MainTaskViewModel(_mainTaskService);
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditMainTask(_mainTaskService)); 
    }

    private void searchMainTasks_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty((string)e.NewTextValue))
        {
            var viewModel = BindingContext as MainTaskViewModel;
            viewModel?.SearchAllMainTasks();
        }
    }

    private void btnFilter_Clicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MainTaskViewModel;

        var dropdown = new DropdownPopup(new List<string> { StatusEnum.Ativo.ToString(), StatusEnum.Em_Atraso.ToString().Replace("_", " ") , StatusEnum.Concluido.ToString(), "Todos" },
                       selectedValue =>
                       {
                           viewModel?.SearchAllMainTasksByStatus(selectedValue);
                       }, "Selecione um Status");

        this.ShowPopup(dropdown);
    }
}