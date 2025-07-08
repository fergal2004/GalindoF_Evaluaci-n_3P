// Views/LogPage.xaml.cs
using Evaluacion3P.ViewModels;
using System.Diagnostics; // Added for Debug.WriteLine

namespace Evaluacion3P.Views; // Este namespace debe coincidir con x:Class en LogPage.xaml

public partial class LogPage : ContentPage // La clase debe ser 'partial'
{
    public LogPage(LogViewModel viewModel)
    {
        try
        {
            InitializeComponent(); 
            BindingContext = viewModel;
            Debug.WriteLine("LogPage: Constructor completed successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LogPage: Error in constructor: {ex.Message}");
            // Si la app se cierra, puedes descomentar la siguiente línea para ver una alerta
            // Shell.Current.DisplayAlert("Error", $"LogPage Constructor Error: {ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var viewModel = BindingContext as LogViewModel;
            if (viewModel != null)
            {
                viewModel.LoadLogsCommand.Execute(null);
                Debug.WriteLine("LogPage: OnAppearing - LoadLogsCommand executed.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LogPage: Error in OnAppearing: {ex.Message}");
            // Si la app se cierra, puedes descomentar la siguiente línea para ver una alerta
            // Shell.Current.DisplayAlert("Error", $"LogPage OnAppearing Error: {ex.Message}", "OK");
        }
    }
}