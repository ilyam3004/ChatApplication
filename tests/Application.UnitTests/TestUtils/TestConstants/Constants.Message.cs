namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Message
    {
        public static readonly Guid MessageId = Guid.NewGuid();
        public static readonly DateTime Date = DateTime.Now;
        public const string MessageText = "Some text";
    }
}