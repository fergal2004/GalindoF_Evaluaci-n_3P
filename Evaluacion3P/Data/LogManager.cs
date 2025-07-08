using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Maui.Storage; 
using Path = System.IO.Path;

namespace Evaluacion3P.Data;

public class LogManager
{
    private readonly string _logFilePath;

    public LogManager()
    {
        string lastName = "Galindo";
        string logFileName = $"Logs_{lastName}.txt";
        _logFilePath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, logFileName); // Explicitly specify FileSystem
        Debug.WriteLine($"Log file path: {_logFilePath}");
    }

    public async Task AppendLog(string message)
    {
        try
        {
            string logEntry = $"Se incluyó el registro {message} el {DateTime.Now:dd/MM/yyyy hh:mm}]";
            await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
            Debug.WriteLine($"Log appended: {logEntry}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error appending log: {ex.Message}");
        }
    }

    public async Task<string> ReadLogs()
    {
        try
        {
            if (File.Exists(_logFilePath))
            {
                string logs = await File.ReadAllTextAsync(_logFilePath);
                Debug.WriteLine($"Logs read:\n{logs}");
                return logs;
            }
            Debug.WriteLine("Log file does not exist.");
            return "No hay registros de logs todavía.";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading logs: {ex.Message}");
            return $"Error al leer logs: {ex.Message}";
        }
    }
}
