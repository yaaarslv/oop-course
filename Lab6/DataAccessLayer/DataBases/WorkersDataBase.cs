using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataAccessLayer.Entities;
using DataAccessLayer.Tools;

namespace DataAccessLayer.DataBases;

public class WorkersDataBase
{
    public const int MaxAccessLevel = 15;
    private static WorkersDataBase _instance;
    private List<Worker> _workers;
    private List<Account> _accounts;
    private Dictionary<string, string> _passwords;

    private WorkersDataBase()
    {
        _workers = new List<Worker>();
        _accounts = new List<Account>();
        _passwords = new Dictionary<string, string>();
    }

    public Director Director { get; private set; }
    public IReadOnlyCollection<Worker> Workers => _workers;
    public IReadOnlyCollection<Account> Accounts => _accounts;
    public IReadOnlyDictionary<string, string> Passwords => _passwords;

    public static WorkersDataBase GetInstance()
    {
        if (_instance is null)
        {
            _instance = new WorkersDataBase();
        }

        return _instance;
    }

    public Worker AddWorker(Worker worker, string login, string password)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (string.IsNullOrWhiteSpace(login))
        {
            throw AccountException.LoginIsNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw AccountException.PasswordIsNullException();
        }

        if (CheckWorkerExistence(worker))
        {
            throw WorkerException.WorkerAlreadyExistsException();
        }

        _workers.Add(worker);
        CreateAccount(worker, login, password);
        return worker;
    }

    public void RemoveWorker(Worker worker)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (!CheckWorkerExistence(worker))
        {
            throw WorkerException.WorkerNotExistsException();
        }

        _workers.Remove(worker);
    }

    public bool CheckAccountExistence(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw AccountException.LoginIsNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw AccountException.PasswordIsNullException();
        }

        return _passwords.ContainsKey(login) && _passwords[login] == MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).ToString();
    }

    public void SetDirector(Director director)
    {
        if (director is null)
        {
            throw WorkerException.DirectorIsNullException();
        }

        Director = director;
    }

    public void SaveConfiguration(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw WorkerException.PathIsNullException();
        }

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}workersDatabase_configuration.json", FileMode.OpenOrCreate);
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

        FileStream fileStream = new FileStream($"{path}{Path.DirectorySeparatorChar}workersDatabase_configuration.json", FileMode.OpenOrCreate);
        WorkersDataBase workersDataBase = await JsonSerializer.DeserializeAsync<WorkersDataBase>(fileStream);
        _accounts = workersDataBase?._accounts;
        _passwords = workersDataBase?._passwords;
        _workers = workersDataBase?._workers;
        fileStream.Close();
    }

    private Account CreateAccount(Worker worker, string login, string password)
    {
        if (worker is null)
        {
            throw WorkerException.WorkerIsNullException();
        }

        if (string.IsNullOrWhiteSpace(login))
        {
            throw AccountException.LoginIsNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw AccountException.PasswordIsNullException();
        }

        var account = new Account(worker, login, password);
        _accounts.Add(account);
        _passwords.Add(login, MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).ToString());
        return account;
    }

    private bool CheckWorkerExistence(Worker worker)
    {
        return _workers.Any(w => w == worker);
    }
}