namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class User
    {
        public static readonly Guid UserId = Guid.NewGuid();
        public static readonly string ConnectionId = Guid.NewGuid().ToString();
        public const string Username = "username";

        // public static string FullNameFromGiveIndex(int index)
        //         => $"{FullName} {index}";
        //
        //     public static string AddressFromGivenIndex(int index)
        //         => $"{Address} {index}";
        //
        //     public static DateTime BirthdayFromGivenIndex(int index)
        //         => Birthday.AddYears(index);
        // }
    }
}