using System;

namespace LibLite.Log.Lite.FileArchive.Formatter
{
    public class CustomArchiveFileNameFormatter : IArchiveFileNameFormatter
    {
        private readonly Func<string> _formatter;
        private readonly string _extension;

        public CustomArchiveFileNameFormatter(
            Func<string> formatter,
            string extension = "txt")
        {
            _formatter = formatter;
            _extension = extension;
        }

        public string Format()
        {
            return $"{_formatter.Invoke()}.{_extension}";
        }
    }
}
