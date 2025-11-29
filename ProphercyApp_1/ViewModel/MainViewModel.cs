using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProphercyApp_1.Models;
using ProphercyApp_1.Services;
using ProphercyApp_1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProphercyApp_1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DataService dataService;
        public ICommand RegisterPageCommand { get; }

        public ICommand LoginPageCommand { get; }


        [ObservableProperty]
        private ObservableCollection<Prophecy> recentProphecies;

        [ObservableProperty]
        private string currentProphecy = "Нажми щоб отримати передбачення...";

        [ObservableProperty]
        private string userName = "Друже";

        private readonly string[] prophecyTemplates = new[]
        {
            "Тебе чекає великий успіх у найближчому майбутньому.",
            "Нові знайомства принесуть радість і натхнення.",
            "Твоя наполегливість приведе тебе до мети.",
            "Очікуй несподіваних можливостей на роботі.",
            "Любов може з'явитися там, де ти її найменше очікуєш.",
            "Зміни в житті принесуть нові перспективи.",
            "Твоя інтуїція допоможе прийняти важливе рішення.",
            "Фінансовий прибуток буде незабаром.",
            "Проведи більше часу з близькими - це принесе гармонію.",
            "Нові хобі відкриють для тебе нові таланти.",
            "Подорожі принесуть незабутні враження.",
            "Твоя творчість відкриє нові горизонти."
        };

        public MainViewModel()
        {
            dataService = new DataService();
            RecentProphecies = new ObservableCollection<Prophecy>();
            RegisterPageCommand = new Command(async () => await GoToRegisterPageAsync());
            LoginPageCommand = new Command(async () => await GoToLoginPageAsync());

        }

        [RelayCommand]
        private async Task GoToRegisterPageAsync()
        {
            // Використовуємо Shell навігацію
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        private async Task GoToLoginPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        public async Task InitializeAsync()
        {
            UserName = Preferences.Get("CurrentUserName", "Друже");
            await LoadPropheciesAsync();
        }

        [RelayCommand]
        private async void LoadUserDataAsync()
        {
            UserName = Preferences.Get("CurrentUserName", "Друже");

            await LoadPropheciesAsync();
        }

        [RelayCommand]
        private async Task LoadPropheciesAsync()
        {
            var email = Preferences.Get("CurrentUserEmail", string.Empty);
            if (string.IsNullOrEmpty(email))
                return;
            var allProphecies = await dataService.LoadPropheciesAsync();

            var userProphecies = allProphecies
                .Where(p => p.UserEmail == email)
                .OrderByDescending(p => p.Date)
                .Take(5)
                .ToList();
            RecentProphecies.Clear();
            foreach (var prophecy in userProphecies)
            {
                RecentProphecies.Add(prophecy);
            }
        }
        [RelayCommand]
        private async Task GenerateProphecyAsync()
        {
            var random = new Random();
            var index = random.Next(prophecyTemplates.Length);

            CurrentProphecy = prophecyTemplates[index];
            var prophecy = new Prophecy
            {
                Text = CurrentProphecy,
                Date = DateTime.Now,
                UserEmail = Preferences.Get("CurrentUserEmail", string.Empty)
            };

            var allProphecies = await dataService.LoadPropheciesAsync();
            allProphecies.Add(prophecy);

            await dataService.SavePropheciesAsync(allProphecies);
            await LoadPropheciesAsync();
        }

        public async Task LoadRecentPropheciesAsync(string userEmail)
        {
            var prophecies = await dataService.LoadPropheciesAsync();
            var userProphecies = prophecies
                .Where(p => p.UserEmail == userEmail)
                .OrderByDescending(p => p.Date)
                .Take(10);
            RecentProphecies = new ObservableCollection<Prophecy>(userProphecies);
        }

        [RelayCommand]
        private async Task GoToHistoryAsync()
        {
            await Shell.Current.GoToAsync(nameof(HistoryPage));
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            bool result = await Shell.Current.DisplayAlert("Вихід", "Ви впевнені, що хочете вийти?", "Так", "Ні");

            if (result)
            {
                Preferences.Clear();
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
            }
        }
    }
}
