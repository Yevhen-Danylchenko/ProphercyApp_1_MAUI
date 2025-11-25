using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProphercyApp_1.Models;
using ProphercyApp_1.Services;
using ProphercyApp_1.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProphercyApp_1.ViewModel
{
    public partial class HistoryViewModel: ObservableObject
    {
        private readonly DataService dataService;

        [ObservableProperty]
        private ObservableCollection<History> historyEntries = new();

        public HistoryViewModel()
        {
            dataService = new DataService();
        }

        [RelayCommand]
        private async Task LoadHistoryAsync()
        {
            var email = Preferences.Get("CurrentUserEmail", string.Empty);
            if (string.IsNullOrEmpty(email))
            {
                await Shell.Current.DisplayAlert("Помилка", "Користувач не увійшов у систему.", "OK");
                return;
            }

            var allHistory = await dataService.LoadHistoryAsync();
            var filtered = allHistory.Where(h => h.UserEmail == email);

            HistoryEntries.Clear();
            foreach (var entry in filtered)
                HistoryEntries.Add(entry);
        }
    }
}
