# Log-Lite

Log-Lite is a small size, high performance tool for logging events. Library is written in C#,has no external dependencies and rely on in-code configuration.

# Table of contents
- [Features](#Features)
- [Examples](#Examples)
- [Todos](#Todos)

## Features

  - 4 log levels (info, warning, error, fatal)
  - Custom logs formatting
  - Writing to Console
  - Writing to file
  - Writing to multiple sources at once
  - Archiving log files by given criteria
  - Selecting log levels to handle
 
## Examples

```CSharp
// creating basic logger that will write to file logs.txt
var fileLogWriter = new FileLogWriter();
var logger = new Logger(fileLogWriter);
```

```CSharp
// creating basic logger that will write to console
var consoleLogWriter = new ConsoleLogWriter();
var logger = new Logger(consoleLogWriter);
```

```CSharp
// loggers can have multiple log writers
var fileLogWriter = new FileLogWriter();
var consoleLogWriter = new ConsoleLogWriter();
var logger = new Logger(fileLogWriter, consoleLogWriter);
```

```CSharp
// writing logs
logger.Debug("MESSAGE");
logger.Info("MESSAGE");
logger.Warning("MESSAGE");
logger.Error("MESSAGE");
logger.Fatal("MESSAGE");
```

```CSharp
// default log format can be changed using a formatter
// every writer can have it's own formatter
// in a FileLogWriter good way to set it is by using a builder
var simpleFormatter = new CustomLogFormatter((i) => $"{i.Level} {DateTime.Now} {i.Message}");
var fileLogWriter = FileLogWriter.Builder()
    .Formatter(simpleFormatter)
    .Build();
var logger = new Logger(fileLogWriter);
```

```CSharp
// we can specify which file to log and what levels to log
var errorsFileInfo = new SystemFileInfo("errors.txt");
var logLevels = new LogLevel[] { LogLevel.ERROR, LogLevel.FATAL };
var errorsFileLogWriter = FileLogWriter.Builder()
    .SetFileInfo(errorsFileInfo)
    .SetAllowedLogLevels(logLevels)
    .Build();
var logger = new Logger(errorsFileLogWriter);
// now only ERRORS and FATALS will be logging to errors.txt file
```

```CSharp
// to keep log file small and useful we would want to archive it on some point
var fileInfo = new SystemFileInfo("logs.txt");
var checker = new SizeArchiveNecessityChecker(fileInfo, 1, MemoryUnit.MB);
var fileNameFormatter = new MillisecondsArchiveFileNameFormatter("json");
var archiver = new FileArchiver(fileInfo, "Archive", checker, fileNameFormatter: fileNameFormatter);
var fileLogWriter = FileLogWriter.Builder()
    .SetFileInfo(fileInfo)
    .SetFileArchiver(archiver)
    .Build();
// now if logs.txt will have more than 1MB data it will be moved to Archive directory with name changed to current timestamp as milliseconds
// new fresh logs.txt file will be created
// log files can be also archived based on their age using TimeArchiveNecessityChecker
```

```CSharp
// my personal favourite setup
// global writer for all kinds of logs, writes to logs.txt and archives them in Archive directory when they are older then 1 day
var fileInfo = new SystemFileInfo("logs.txt");
var checker = new TimeArchiveNecessityChecker(fileInfo, 1, TimeUnit.DAYS);
var archiver = new FileArchiver(fileInfo, "Archive", checker);
var fileWriter = FileLogWriter.Builder()
    .SetFileInfo(fileInfo)
    .SetFileArchiver(archiver)
    .Build();

// errors writer for separating dangerous logs, writes to errors.txt archives them in Archive_Errors directory when they reach 1MB size
var errorsFileInfo = new SystemFileInfo("errors.txt");
var errorsChecker = new SizeArchiveNecessityChecker(errorsFileInfo, 1, MemoryUnit.MB);
var errorsArchiver = new FileArchiver(errorsFileInfo, "Archive_Errors", errorsChecker);
var logLevels = new LogLevel[] { LogLevel.ERROR, LogLevel.FATAL };
var errorsFileWriter = FileLogWriter.Builder()
    .SetFileInfo(errorsFileInfo)
    .SetFileArchiver(errorsArchiver)
    .SetAllowedLogLevels(logLevels)
    .Build();

// console writer for simple logging and instant debugging
var simpleFormatter = new CustomLogFormatter((i) => $"{i.Message}");
var consoleWriter = new ConsoleLogWriter(simpleFormatter);

var logger = new Logger(fileWriter, errorsFileWriter, consoleWriter);
```

### Todos

 - Write MORE Tests
 - Add HTTP client for sending logs to a server
 - Add custom archive file name formatter


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>
