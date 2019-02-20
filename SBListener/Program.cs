using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;

namespace SBListener
{
    public class Program
    {
        const string SessionPrefix = "session-prefix";
        private const string SBConnectionString = "Endpoint=sb://{0}.servicebus.windows.net/;SharedAccessKeyName={1};SharedAccessKey={2}";


        public static async Task Main(string[] args)
        {
            var accountId = Environment.GetEnvironmentVariable("EXAMPLE_SB_ACCOUNT_ID");
            var accessKeyName = Environment.GetEnvironmentVariable("EXAMPLE_SB_ACCESS_KEY_NAME");
            var accessKey = Environment.GetEnvironmentVariable("EXAMPLE_SB_ACCESS_KEY");
            var queueName = Environment.GetEnvironmentVariable("EXAMPLE_SB_QUEUE_NAME");

            if (String.IsNullOrEmpty(accountId))
            {
                Console.WriteLine("Please add environment variable [EXAMPLE_SB_ACCOUNT_ID]");
                Console.WriteLine("Any key to exit.");
                Console.ReadKey();
                return;
            }

            if (String.IsNullOrEmpty(accessKeyName))
            {
                Console.WriteLine("Please add environment variable [EXAMPLE_SB_ACCESS_KEY_NAME]");
                Console.WriteLine("Any key to exit.");
                Console.ReadKey();
                return;
            }

            if (String.IsNullOrEmpty(accessKey))
            {
                Console.WriteLine("Please add environment variable [EXAMPLE_SB_ACCESS_KEY]");
                Console.WriteLine("Any key to exit.");
                Console.ReadKey();
                return;
            }

            if (String.IsNullOrEmpty(queueName))
            {
                Console.WriteLine("Please add environment variable [EXAMPLE_SB_QUEUE_NAME]");
                Console.WriteLine("Any key to exit.");
                Console.ReadKey();
                return;
            }

            var connectionString = String.Format(SBConnectionString, accountId, accessKeyName, accessKey);

            var bldr = new ServiceBusConnectionStringBuilder(connectionString)
            {
                EntityPath = queueName
            };

            var _client = new QueueClient(bldr, ReceiveMode.PeekLock, RetryPolicy.Default);

            _client.RegisterMessageHandler( (msg, token) =>
            {
                var message = System.Text.UTF8Encoding.UTF8.GetString(msg.Body);
                Console.WriteLine("Message Received:");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("-------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"To:\t{msg.To}");
                Console.WriteLine($"Label:\t{msg.Label}");
                Console.WriteLine($"MsgId:\t{msg.MessageId}");
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
                Console.ResetColor();

                return Task.FromResult(default(object));
            },
            (ex) =>
            {
                return Task.FromResult(default(object));
            });

            Console.WriteLine($"Listing for messages on {accountId}/{queueName}");
            Console.WriteLine("-----------------------------------------------------------------------");

            Console.ReadKey();
        }
    }
}
