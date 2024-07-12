namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Chat
    {
        public static readonly Guid ChatId = Guid.NewGuid();
        public const string ChatName = "The name of the chat";
        
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