namespace ProphercyApp_1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ViewModel.MainViewModel vm)
            {
                await vm.InitializeAsync();
            }
        }

    }
}
