using Banks.Models;
using Banks.Tools;

namespace Banks.Entities;

public class ClientBuilder
{
    private readonly string? _fullName;
    private Address? _address;
    private Passport? _passport;
    private PhoneNumber? _phoneNumber;
    public ClientBuilder(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw ClientException.ClientNameIsNullException();
        }

        _fullName = name;
    }

    public void AddAddress(Address? address)
    {
        _address = address;
    }

    public void AddPassportData(Passport? passport)
    {
        _passport = passport;
    }

    public void AddPhoneNumber(PhoneNumber? phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public Client CreateClient()
    {
        return new Client(_fullName, _address, _passport, _phoneNumber);
    }
}