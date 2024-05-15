using DataAccessLayer.DataBases;
using DataAccessLayer.Tools;

namespace DataAccessLayer.Entities;

public class Director : Worker
{
    public Director(string name, int accessLevel = WorkersDataBase.MaxAccessLevel)
        : base(name, accessLevel)
    {
    }

    public Report MakeReport(DateTime dateTime1, DateTime dateTime2)
    {
        return new Report(dateTime1, dateTime2);
    }
}