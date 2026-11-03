using Azure.Storage.Queues;

Console.WriteLine("== CONSUMER ==");

const int MIN_DELAY = 100;
const int MAX_DELAY = 10000;
int delay = MIN_DELAY;

var connectionString = "UseDevelopmentStorage=true";
var queueName = "tickets";

var client = new QueueClient(connectionString, queueName);

await client.CreateIfNotExistsAsync();

while (true)
{
    var message = await client.ReceiveMessageAsync();
    if (message is not null && message.Value is not null)
    {
        delay = MIN_DELAY;
        Console.WriteLine($"\nReceived message: {message.Value.Body.ToString()}");
        await client.DeleteMessageAsync(
            message.Value.MessageId,
            message.Value.PopReceipt);
    }
    else
    {
        if (delay < MIN_DELAY)
            delay = MIN_DELAY;
        await Task.Delay(delay);
        if (delay < MAX_DELAY)
            delay *= 2;
        if(delay > MAX_DELAY)
            delay = MAX_DELAY;
        Console.Write(".");
    }
}

Console.WriteLine("== END ==");