using Banks.Tools;

namespace Banks.Models;

public class PhoneNumber
{
    private const int MinPhoneNumberLength = 11;
    private const int MaxPhoneNumberLength = 12;
    public PhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw PhoneNumberException.PhoneNumberIsNullException();
        }

        CheckPhoneNumber(phoneNumber);
        Phone = phoneNumber;
    }

    public string? Phone { get; }

    private void CheckPhoneNumber(string? phoneNumber)
    {
        if ((phoneNumber?[0] == '+' && phoneNumber.Length != MaxPhoneNumberLength) ||
            (phoneNumber?[0] == '8' && phoneNumber.Length != MinPhoneNumberLength) ||
            phoneNumber?.Length < MinPhoneNumberLength || phoneNumber?.Length > MaxPhoneNumberLength)
        {
            throw PhoneNumberException.PhoneNumberIsInvalidException();
        }
    }
}