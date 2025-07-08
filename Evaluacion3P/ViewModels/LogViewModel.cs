// ViewModels/LogViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Evaluacion3P.Data;
using System.Diagnostics;

namespace Evaluacion3P.ViewModels;

public partial class LogViewModel : ObservableObject
{
    private readonly LogManager _logManager;

    [ObservableProperty]
    string logContent;

    public LogViewModel(LogManager logManager)
    {
        _logManager = logManager;
        LogContent = "Cargando logs..."; // Initial message
        Debug.WriteLine("LogViewModel: Constructor completed.");
    }

    [RelayCommand]
    async Task LoadLogsAsync()
    {
        Debug.WriteLine("LogViewModel: Loading logs from file...");
        LogContent = await _logManager.ReadLogs();
        Debug.WriteLine("LogViewModel: Logs loaded.");
    }
}