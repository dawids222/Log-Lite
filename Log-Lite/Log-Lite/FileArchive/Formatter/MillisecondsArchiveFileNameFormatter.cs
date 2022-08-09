using System;

namespace LibLite.Log.Lite.FileArchive.Formatter
{
    public class MillisecondsArchiveFileNameFormatter : IArchiveFileNameFormatter
    {
        private readonly string _extension;

        public MillisecondsArchiveFileNameFormatter(string extension = "txt")
        {
            _extension = extension;
        }

        public string Format()
        {
            var milliseconds = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            return $"{milliseconds}.{_extension}";
        }
    }
}
