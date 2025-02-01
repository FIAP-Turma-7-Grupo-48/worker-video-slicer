using Domain.ValueObjects;

namespace Domain.ValueObjects.Exceptions
{
    public class NullFileException : Exception
    {
        private const string DEFAULT_MESSAGE = "File Can't Be Null";

        private NullFileException() : base(DEFAULT_MESSAGE) { }

        public static void ThrowIfFileIsNull(FileModel? fileModel)
        {
            if (fileModel == null)
            {
                throw new NullFileException();
            }

            if (fileModel.Value.Data == null || fileModel.Value.Data.Length == 0)
            {
                throw new NullFileException();
            }

        }
    }
}
