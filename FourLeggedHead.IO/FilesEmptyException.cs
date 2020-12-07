using System;
using System.IO;
using System.Runtime.Serialization;

namespace FourLeggedHead.IO
{
    public class FileIsEmptyException : IOException
    {
        public FileIsEmptyException() { }
        public FileIsEmptyException(string? message) : base(message) { }
        public FileIsEmptyException(string? message, string? fileName) : base(message) { FileName = fileName; }
        public FileIsEmptyException(string? message, string? fileName, Exception? innerException) : base(message, innerException) { FileName = fileName; }
        public FileIsEmptyException(string? message, string? fileName, int hresult) : base(message, hresult) { FileName = fileName; }
        protected FileIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public string? FileName { get; }
        public override string Message
        {
            get
            {
                if (FileName == null) return $"File is empty. {base.Message}";
                return $"File {FileName} is empty. {base.Message}";
            }
        }
    }
}
