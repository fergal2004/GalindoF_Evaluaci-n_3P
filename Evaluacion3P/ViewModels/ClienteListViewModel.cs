// ViewModels/ClienteListViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Evaluacion3P.Models;
using Evaluacion3P.Data;
using Evaluacion3P.Views;
using System.Diagnostics;

namespace Evaluacion3P.ViewModels;

public partial class ClienteListViewModel : ObservableObject
{
    public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();

    private readonly ClienteDatabase _database;
    private readonly LogManager _logManager;

    public ClienteListViewModel(ClienteDatabase database, LogManager logManager)
    {
        _database = database;
        _logManager = logManager;
        Debug.WriteLine("ClienteListViewModel: Constructor completed.");
    }

    [RelayCommand]
    async Task LoadClientesAsync()
    {
        Clientes.Clear();
        var clientes = await _database.GetClientesAsync();
        Debug.WriteLine($"ClienteListViewModel: Loaded {clientes.Count} clientes from database.");
        foreach (var cliente in clientes)
        {
            Clientes.Add(cliente);
        }
        Debug.WriteLine($"ClienteListViewModel: Clientes collection now has {Clientes.Count} items.");
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Cliente cliente)
    {
        if (cliente == null) return;
        Debug.WriteLine($"Navigating to ClienteDetailPage for ID: {cliente.ID}");
        // Corrected: Use absolute path for Shell navigation
        await Shell.Current.GoToAsync($"///{nameof(ClienteDetailPage)}?ClienteId={cliente.ID}");
    }

    [RelayCommand]
    async Task AddNewClienteAsync()
    {
        Debug.WriteLine("Navigating to ClienteDetailPage to add new client.");
        // Corrected: Use absolute path for Shell navigation
        await Shell.Current.GoToAsync($"///{nameof(ClienteDetailPage)}");
    }

    [RelayCommand]
    async Task DeleteClienteAsync(Cliente cliente)
    {
        if (cliente == null) return;
        Debug.WriteLine($"Attempting to delete client: {cliente.Nombre} (ID: {cliente.ID})");
        await _database.DeleteClienteAsync(cliente);
        await _logManager.AppendLog($"Cliente eliminado: {cliente.Nombre}");
        await LoadClientesAsync();
    }
}

