using System.Text.Json;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using DataAccessLayer.Tools;

namespace DataAccessLayer.DataBases;

public class ReportDataBase
{
    private static ReportDataBase _instance;
    private Dictionary<DateTime, Report> _reports;

    private ReportDataBase()
    {
        _reports = new Dictionary<DateTime, Report>();
    }

    public IReadOnlyDictionary<DateTime, Report> Reports => _reports;

    public static ReportDataBase GetInstance()
    {
        if (_instance is null)
        {
            _instance = new ReportDataBase();
        }

        return _instance;
    }

    public Report AddReport(DateTime dateTime, Report report)
    {
        if (report is null)
        {
            throw ReportException.ReportIsNullException();
        }

        _reports.Add(dateTime, report);
        return report;
    }

    public void SaveConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}reportDatabase_configuration.json", FileMode.OpenOrCreate);
        var options = new JsonSerializerOptions { WriteIndented = true };
        JsonSerializer.Serialize(fileStream, this, options);
        fileStream.Close();
    }

    public async void LoadConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}reportDatabase_configuration.json", FileMode.OpenOrCreate);
        ReportDataBase reportDataBase = await JsonSerializer.DeserializeAsync<ReportDataBase>(fileStream);
        _reports = reportDataBase?._reports;
        fileStream.Close();
    }
}