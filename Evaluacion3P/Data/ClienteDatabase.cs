using SQLite;
using Evaluacion3P.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics; 

namespace Evaluacion3P.Data;

public class ClienteDatabase
{
    SQLiteAsyncConnection Database;

    public ClienteDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        Debug.WriteLine($"Database path: {Constants.DatabasePath}");
        var result = await Database.CreateTableAsync<Cliente>();
        Debug.WriteLine($"CreateTableAsync<Cliente> result: {result}");
    }

    public async Task<List<Cliente>> GetClientesAsync()
    {
        await Init();
        try
        {
            var clientes = await Database.Table<Cliente>().ToListAsync();
            Debug.WriteLine($"Loaded {clientes.Count} clientes from database.");
            return clientes;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting clientes: {ex.Message}");
            return new List<Cliente>();
        }
    }

    public async Task<Cliente> GetClienteAsync(int id)
    {
        await Init();
        try
        {
            return await Database.Table<Cliente>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cliente by ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<int> SaveClienteAsync(Cliente cliente)
    {
        await Init();
        try
        {
            if (cliente.ID != 0)
            {
                Debug.WriteLine($"Updating cliente: {cliente.Nombre} (ID: {cliente.ID})");
                return await Database.UpdateAsync(cliente);
            }
            else
            {
                Debug.WriteLine($"Inserting new cliente: {cliente.Nombre}");
                return await Database.InsertAsync(cliente);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving cliente: {ex.Message}");
            return 0; // Indicate failure
        }
    }

    public async Task<int> DeleteClienteAsync(Cliente cliente)
    {
        await Init();
        try
        {
            Debug.WriteLine($"Deleting cliente: {cliente.Nombre} (ID: {cliente.ID})");
            return await Database.DeleteAsync(cliente);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting cliente: {ex.Message}");
            return 0; // Indicate failure
        }
    }
}

public static class Constants
{
    public const string DatabaseFilename = "ClientesSQLite.db3"; // Changed database name

    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    // Explicitly use Microsoft.Maui.Storage.FileSystem to resolve ambiguity
    public static string DatabasePath =>
        Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DatabaseFilename);
}

