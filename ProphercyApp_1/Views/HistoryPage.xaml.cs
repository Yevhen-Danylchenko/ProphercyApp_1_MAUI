using ProphercyApp_1.ViewModel;

namespace ProphercyApp_1.Views;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HistoryViewModel vm)
        {
            vm.LoadHistoryAsyncCommand.Execute(null);
        }
    }

}