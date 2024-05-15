using BusinessLayer.Authentication;
using BusinessLayer.Services;
using DataAccessLayer.DataBases;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace PresentationLayer.Consoles;

public class WorkerConsole
{
    private static MessageService _service;
    public WorkerConsole()
    {
        for (int i = 0; i < 3; i++)
        {
            MessagesDataBase.GetInstance().AddSource(new EmailSource(3, "123@gmail.com"));
            MessagesDataBase.GetInstance().AddSource(new MessageSource(3, "@qwerty"));
            MessagesDataBase.GetInstance().AddSource(new SmsSource(3, "+79123456789"));
            var message = new Message("test message", new MessageSender("@yaaarsl_v"));
            var email = new Email("test email", new EmailSender("339009@niuitmo.ru"));
            var sms = new Sms("test sms", new SmsSender("+78005553535"));
            MessagesDataBase.GetInstance().AddMessage(message);
            MessagesDataBase.GetInstance().AddMessage(email);
            MessagesDataBase.GetInstance().AddMessage(sms);
            var director = new Director("Коллегов Коллега Коллегович");
            WorkersDataBase.GetInstance().SetDirector(director);
            WorkersDataBase.GetInstance().AddWorker(director, "directorLogin", "directorPassword");
            var worker = new Worker("Антиколлегов Антиколлега Антиколлегович", 3);
            WorkersDataBase.GetInstance().AddWorker(worker, "workerLogin", "workerPassword");
            worker.SetDevice(new Device("ekis bokis series ekis"));
        }
    }

    public static void Main()
    {
        var console = new WorkerConsole();
        Console.WriteLine("Enter \"q\" to close console");
        string login, password;
        Worker worker;

        while (true)
        {
            while (true)
            {
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(login))
                {
                    break;
                }

                Console.WriteLine("You entered invalid login!");
            }

            while (true)
            {
                Console.WriteLine("Enter your password:");
                password = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(password))
                {
                    break;
                }

                Console.WriteLine("You entered invalid password!");
            }

            _service = Authentication.Authenticate(login, password);
            if (_service is not null)
            {
                worker = WorkersDataBase.GetInstance().Accounts.SingleOrDefault(a => a.Login == login)?.Worker;
                break;
            }

            Console.WriteLine("You entered wrong login or password!");
        }

        if (worker?.AccessLevel == WorkersDataBase.MaxAccessLevel)
        {
            while (true)
            {
                Console.WriteLine("\nEnter \"/Make report\" to make new report");
                Console.WriteLine("Enter \"/See report\" to see report");
                Console.WriteLine("Enter \"/Close session\" to close session");
                console.DirectorParser(Console.ReadLine(), worker);
                Console.WriteLine();
            }
        }
        else
        {
            while (true)
            {
                Console.WriteLine("\nEnter \"/Get messages\" to get all unprocessed messages");
                Console.WriteLine("Enter \"/Close session\" to close session");
                console.WorkerParser(Console.ReadLine(), worker);
                Console.WriteLine();
            }
        }
    }

    private void WorkerParser(string command, Worker worker)
    {
        switch (command)
        {
            case "/Get messages":
                GetMessages(worker);
                Console.WriteLine();
                break;
            case "/Close session":
                CloseSession(worker);
                break;
            case "/q":
                Environment.Exit(0);
                break;
        }
    }

    private void DirectorParser(string command, Worker worker)
    {
        switch (command)
        {
            case "/Make report":
                MakeReport(worker);
                Console.WriteLine();
                break;
            case "/See report":
                SeeReport();
                Console.WriteLine();
                break;
            case "/Close session":
                CloseSession(worker);
                break;
            case "/q":
                Environment.Exit(0);
                break;
        }
    }

    private void MakeReport(Worker worker)
    {
        Console.WriteLine("Enter start date:");
        var startDateTime = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter end date:");
        var endDateTime = DateTime.Parse(Console.ReadLine());
        _service.MakeReport(worker, startDateTime, endDateTime);
    }

    private void SeeReport()
    {
        Console.WriteLine("Enter date of report:");
        var dateTime = DateTime.Parse(Console.ReadLine());
        var report = ReportDataBase.GetInstance().Reports[dateTime.Date];
        _service.PrintReport(report);
    }

    private void GetMessages(Worker worker)
    {
        var messages = _service.GetMessages(worker);
        Console.WriteLine("\nTo read message enter \"read\"\nTo reply message enter \"reply\"");
        foreach (var message in messages)
        {
            Console.WriteLine(message.Text);
            string command = Console.ReadLine();
            if (command == "read")
            {
                _service.Read(message);
            }
            else if (command == "reply")
            {
                Console.WriteLine("Enter text to reply:");
                string text = Console.ReadLine();
                _service.Reply(message, text);
            }
        }
    }

    private void CloseSession(Worker worker)
    {
        _service.CloseSession(worker);
        Environment.Exit(0);
    }
}