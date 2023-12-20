// See https://aka.ms/new-console-template for more information
using SkroutzLastMileClient;

Console.WriteLine("Hello, World!");

var credentials = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "APIKey.txt")).Split(Environment.NewLine);


var client = new SkroutzLastMileClient.SkroutzLastMileClient(new SkroutzLastMileCredentials(credentials[0], credentials[1]), true);

var lockersResponse = await client.GetAllSkroutzPointsAsync();

var modelTypes = lockersResponse.Result.Select(x => x.Model).Distinct().ToList();

var jsonConverter = new DayfOfWeekToStringJsonConverter();

Console.ReadLine();