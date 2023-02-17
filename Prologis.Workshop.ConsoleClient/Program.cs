using LabelServiceClient;

Console.WriteLine("Start Client");

var httpClient = new HttpClient();
var client = new Client("http://localhost:5260", httpClient);

var labels = await client.LabelAllAsync();

foreach (var label in labels)
{
  Console.WriteLine($"Label: {label.Id} {label.Name} {label.TrackingId} {label.Address} {label.DeviceId}");
}

Console.WriteLine("End Client");
Console.ReadLine();