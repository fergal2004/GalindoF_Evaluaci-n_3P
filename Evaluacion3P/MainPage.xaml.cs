// Views/MainPage.xaml.cs
using Evaluacion3P.ViewModels;
using System.Diagnostics; 

namespace Evaluacion3P.Views;

public partial class MainPage : ContentPage
{
    public MainPage(ClienteListViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
            Debug.WriteLine("MainPage: Constructor completed successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MainPage: Error in constructor: {ex.Message}");
            // Consider displaying an alert here if needed for debugging on device
            // Shell.Current.DisplayAlert("Error", $"MainPage Constructor Error: {ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var viewModel = BindingContext as ClienteListViewModel;
            if (viewModel != null)
            {
                viewModel.LoadClientesCommand.Execute(null);
                Debug.WriteLine("MainPage: OnAppearing - LoadClientesCommand executed.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MainPage: Error in OnAppearing: {ex.Message}");
            // Shell.Current.DisplayAlert("Error", $"MainPage OnAppearing Error: {ex.Message}", "OK");
        }
    }
}