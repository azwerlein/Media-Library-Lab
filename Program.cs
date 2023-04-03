using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config;";

var Logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
