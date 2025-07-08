// Views/ClienteDetailPage.xaml.cs
using Evaluacion3P.ViewModels;
using System.Diagnostics; // Added for Debug.WriteLine

namespace Evaluacion3P.Views;

public partial class ClienteDetailPage : ContentPage
{
    public ClienteDetailPage(ClienteDetailViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
            Debug.WriteLine("ClienteDetailPage: Constructor completed successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ClienteDetailPage: Error in constructor: {ex.Message}");
            // Shell.Current.DisplayAlert("Error", $"ClienteDetailPage Constructor Error: {ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // The ViewModel's LoadCliente is triggered by QueryProperty, not directly here.
        Debug.WriteLine("ClienteDetailPage: OnAppearing executed.");
    }
}