// ViewModels/ClienteDetailViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Evaluacion3P.Models;
using Evaluacion3P.Data;
using System.Diagnostics;

namespace Evaluacion3P.ViewModels;

[QueryProperty(nameof(ClienteId), nameof(ClienteId))]
public partial class ClienteDetailViewModel : ObservableObject
{
    private readonly ClienteDatabase _database;
    private readonly LogManager _logManager;

    [ObservableProperty]
    Cliente cliente;

    [ObservableProperty]
    int clienteId;

    public ClienteDetailViewModel(ClienteDatabase database, LogManager logManager)
    {
        _database = database;
        _logManager = logManager;
        Cliente = new Cliente(); // Initialize a new Cliente by default
        Debug.WriteLine("ClienteDetailViewModel: Constructor completed.");
    }

    partial void OnClienteIdChanged(int value)
    {
        LoadCliente(value);
    }

    public async void LoadCliente(int clienteId)
    {
        try
        {
            if (clienteId == 0)
            {
                Cliente = new Cliente();
                Debug.WriteLine("ClienteDetailViewModel: Loading new client form (ID is 0).");
                return;
            }
            var loadedCliente = await _database.GetClienteAsync(clienteId);
            if (loadedCliente != null)
            {
                Cliente = loadedCliente;
                Debug.WriteLine($"ClienteDetailViewModel: Loaded client: {Cliente.Nombre} (ID: {Cliente.ID})");
            }
            else
            {
                Debug.WriteLine($"ClienteDetailViewModel: Client with ID {clienteId} not found. Resetting to new client.");
                Cliente = new Cliente();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ClienteDetailViewModel: Error in LoadCliente: {ex.Message}");
            // Shell.Current.DisplayAlert("Error", $"LoadCliente Error: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task SaveClienteAsync()
    {
        if (Cliente == null || string.IsNullOrWhiteSpace(Cliente.Nombre) || string.IsNullOrWhiteSpace(Cliente.Empresa))
        {
            await Shell.Current.DisplayAlert("Error", "Nombre y Empresa no pueden estar vacíos.", "OK");
            return;
        }

        if (Cliente.AntiguedadMeses * 10 > 1500)
        {
            await Shell.Current.DisplayAlert("Error de Validación", "No se puede guardar una empresa con más de 1500 días de antigüedad (considerando meses de 10 días).", "OK");
            return;
        }

        int result = await _database.SaveClienteAsync(Cliente);
        if (result > 0)
        {
            await _logManager.AppendLog($"Cliente guardado: {Cliente.Nombre} ({Cliente.Empresa})");
            Debug.WriteLine($"Client saved successfully: {Cliente.Nombre}");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo guardar el cliente.", "OK");
            Debug.WriteLine($"Failed to save client: {Cliente.Nombre}");
        }
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        Debug.WriteLine("Cancelling client operation and navigating back.");
        await Shell.Current.GoToAsync("..");
    }
}

