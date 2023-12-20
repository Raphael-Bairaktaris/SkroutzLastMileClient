// See https://aka.ms/new-console-template for more information
using SkroutzLastMileClient;

Console.WriteLine("Hello, World!");

var credentials = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "APIKey.txt")).Split(Environment.NewLine);

var client = new SkroutzLastMileClient.SkroutzLastMileClient(new SkroutzLastMileCredentials(credentials[0], credentials[1]), true);

var lockersResponse = await client.GetAllSkroutzPointsAsync();

var modelTypes = lockersResponse.Result.Select(x => x.Model).Distinct().ToList();

//var shippingOrderResponse = await client.CreateShippingOrderAsync(new CreateShippingOrderRequestModel()
//{
//    PickupLocationCode = credentials[2],
//    CustomerReference = "598",
//    IsReturn = false,
//    NumberOfParcels = 1,
//    PickupDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
//    PickupTimeFrom = new TimeOnly(12, 0, 0),
//    PickupTimeTo = new TimeOnly(14, 0, 0),
//    PickupNotes = "This is a test note",
//    RecipientName = "Raphael Bairaktaris",
//    RecipientPhone = "6955100285",
//    SenderPhone = "6977594432",
//    SkroutzPointId = lockersResponse.Result.ElementAt(10).Id,
//    Weight = 5
//});

var orderId = "soid107617";//shippingOrderResponse.Result.OrderId;
var trackingId = "QRXD1NW7EYG95"; //shippingOrderResponse.Result.TrackingId;

var d = await client.DeleteOrderAsync("soid107620");
d = await client.DeleteOrderAsync("soid107618");

var existingShippingOrder = await client.TrackShipmentAsync(trackingId);

var base64PDF = await client.GetShippingOrderVoucherAsBase64StringAsync(trackingId, PaperSize.Thermal);

try
{
    var pdf = Convert.FromBase64String(base64PDF.Result);

    await File.WriteAllBytesAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Thermal.pdf"), pdf);
}
catch
{

}

var byteArrayPDF = await client.GetShippingOrderVoucherAsPDFAsync(trackingId, PaperSize.A4);

try
{
    await File.WriteAllBytesAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "A4.pdf"), byteArrayPDF.Result);
}
catch
{

}

var deleteResponse = await client.DeleteOrderAsync(orderId);

var jsonConverter = new DayfOfWeekToStringJsonConverter();

Console.ReadLine();