using System;

namespace LibLite.Log.Lite.FileArchive.Formatter
{
    public class DateTimeArchiveFileNameFormatter : IArchiveFileNameFormatter
    {
        private readonly string _extension;

        public DateTimeArchiveFileNameFormatter(string extension = "txt")
        {
            _extension = extension;
        }

        public string Format()
        {
            var currentDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH.mm.ss");
            return $"{currentDateTime}.{_extension}";
        }
    }
}
