using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProphercyApp_1.Services;
using ProphercyApp_1.Views;
using ProphercyApp_1.Models;
using System.Threading.Tasks;

namespace ProphercyApp_1.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DataService dataService;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        public LoginViewModel()
        {
            dataService = new DataService();
        }
        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
               string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }
            var users = await dataService.LoadUsersAsync();
            var user = users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user == null)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
                return;
            }
            Preferences.Set("CurrentUserEmail", user.Email);
            Preferences.Set("CurrentUserName", user.Name);
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }

        [RelayCommand]
        private async Task GoToRegisterAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
