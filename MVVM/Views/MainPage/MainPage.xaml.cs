using TaskManagement.MVVM.ViewModels.MainPage;
using TaskManagement.MVVM.Views.MainTask;
using TaskManagement.Services.Interfaces;

namespace TaskManagement
{
    public partial class MainPage : ContentPage
    {
        private readonly IMainTaskService _mainTaskService;
        private readonly ISubTaskService _subTaskService;

        public MainPage(IMainTaskService mainTaskService, ISubTaskService subTaskService)
        {
            InitializeComponent();
            _mainTaskService = mainTaskService;
            _subTaskService = subTaskService;

            BindingContext = new MainPageViewModel(mainTaskService);
        }

        private async void OnTasksTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("mainTask");
        }

        private void OnNotesTapped(object sender, EventArgs e)
        {
            // Ação a ser executada ao clicar no Frame
            DisplayAlert("Ação", "O Frame foi clicado!", "OK");
        }
    }
}
