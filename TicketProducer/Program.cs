using Azure.Storage.Queues;

Console.WriteLine("== PRODUCER ==");

var connectionString = "UseDevelopmentStorage=true";
var queueName = "tickets";

var client = new QueueClient(connectionString, queueName);

await client.CreateIfNotExistsAsync();

string message;
do
{
    Console.WriteLine("inserisci il prossimo messaggio o exit");
    message = Console.ReadLine().Trim();

    if (string.IsNullOrEmpty(message))
    {
        continue;
    }

    client.SendMessage(message);
} while (true);

Console.WriteLine("== END ==");