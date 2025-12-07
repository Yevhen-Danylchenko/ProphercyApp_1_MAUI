using ProphercyApp_1.ViewModel;
using System.Threading.Tasks;

namespace ProphercyApp_1.Views;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HistoryViewModel vm)
        {
            await vm.LoadHistoryAsyncCommand.ExecuteAsync(null);
        }
    }

}