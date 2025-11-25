using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProphercyApp_1.Models;
using ProphercyApp_1.Services;
using System.Xml.Linq;

namespace ProphercyApp_1.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DataService dataService;

        [ObservableProperty]
        private string name = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private string confirmPassword = string.Empty;

        public RegisterViewModel()
        {
            dataService = new DataService();
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Email) ||
               string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }
            if (Password.Length < 6)
            {
                await Shell.Current.DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var users = await dataService.LoadUsersAsync();

            if (users.Any(u => u.Email == Email))
            {
                await Shell.Current.DisplayAlert("Error", "An account with this email already exists.", "OK");
                return;
            }
            var newUser = new User
            {
                Name = Name,
                Email = Email,
                Password = Password
            };

            users.Add(newUser);
            await dataService.SaveUserAsync(users);
            await Shell.Current.DisplayAlert("Success", "Registration successful!", "OK");
            await Shell.Current.GoToAsync("..");

        }

        [RelayCommand]
        private async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
