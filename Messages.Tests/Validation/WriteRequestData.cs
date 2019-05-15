using Xunit;

namespace MessagesTests.Validation
{
    public class InvalidWriteRequestData : TheoryData<string>
    {
        public InvalidWriteRequestData()
        {
            Add(null);
            Add(string.Empty);
            Add(new string('a', 513));
        }
    }

    public class ValidWriteRequestData : TheoryData<string>
    {
        public ValidWriteRequestData()
        {
            Add("a");
            Add("some valid message");
            Add(new string('a', 512));
        }
    }
}