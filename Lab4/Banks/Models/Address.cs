using System.Reflection.Metadata.Ecma335;
using Banks.Tools;

namespace Banks.Models;

public class Address
{
    private const int AddressComponentsCount = 6;
    public Address(string fullAddress)
    {
        if (string.IsNullOrWhiteSpace(fullAddress))
        {
            throw AddressException.AddressIsNullException();
        }

        var address = fullAddress.Split(", ");
        if (address.Length < AddressComponentsCount)
        {
            throw AddressException.AddressIsNotFullException();
        }

        Country = address[0];
        Region = address[1];
        City = address[2];
        Street = address[3];
        House = int.Parse(address[4]);
        Flat = int.Parse(address[5]);
    }

    public string Country { get; }
    public string Region { get; }
    public string City { get; }
    public string Street { get; }
    public int House { get; }
    public int Flat { get; }

    public string StringAddress()
    {
        return $"{Country}, {Region}, {City}, {Street}, {House}, {Flat}";
    }
}