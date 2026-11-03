using Azure.Storage.Queues;

Console.WriteLine("== CONSUMER ==");

var connectionString = "UseDevelopmentStorage=true";
var queueName = "tickets";

var client = new QueueClient(connectionString, queueName);

await client.CreateIfNotExistsAsync();

while (true)
{
    var message = await client.ReceiveMessageAsync();
    if (message is not null && message.Value is not null)
    {
        Console.WriteLine($"\nReceived message: {message.Value.Body.ToString()}");
        await client.DeleteMessageAsync(
            message.Value.MessageId,
            message.Value.PopReceipt);
    }
    else
    {
        await Task.Delay(1000);
        Console.Write(".");
    }
}

Console.WriteLine("== END ==");