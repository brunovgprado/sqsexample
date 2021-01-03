using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;

namespace SQSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SQSQueueStandard();
        }

        public static void SQSQueueStandard()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("*******___Amazon SQS___***********");
            Console.WriteLine("**********************************\n");

            //------- Creating a SQS standard queue
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.USEast1);

            var sqsRequest = new CreateQueueRequest
            {
                QueueName = "pradoqueue3"
            };

            var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;

            var myQueueUrl = createQueueResponse.QueueUrl; // Retrieving the queue URL

            //------- Retrieving all existing queues URL 
            var listQueuesRequest = new ListQueuesRequest();
            var listQueuesResponse = sqs.ListQueuesAsync(listQueuesRequest);

            Console.WriteLine("List of Amazon SQS Queues\n");
            foreach (var queue in listQueuesResponse.Result.QueueUrls)
            {
                Console.WriteLine($"QueueUrl: {queue}");
            }

            //------- Sending a message tho the created queue
            Console.WriteLine("Sending a message to the queue\n");
            var sqsMessageRequest = new SendMessageRequest
            {
                QueueUrl = myQueueUrl,
                MessageBody = "Email information"
            };

            sqs.SendMessageAsync(sqsMessageRequest);

            Console.WriteLine("Finished sending message to the SQS queue\n");

            //------- Reading the message sended above
            Console.WriteLine("Reading messages from the queue\n");

            var receiveMessageRequest = new ReceiveMessageRequest();

            receiveMessageRequest.QueueUrl = myQueueUrl;

            var receiveMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest);

            foreach (var message in receiveMessageResponse.Result.Messages)
            {
                Console.WriteLine(message.Body);  // Go to a method to process messages.
            }

            Console.ReadLine();
        }

        public static void SQSQueueFIFO()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("*******___Amazon SQS___***********");
            Console.WriteLine("**********************************\n");

            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.USEast1);

            var sqsRequest = new CreateQueueRequest
            {
                QueueName = "pradoqueue.fifo",
                Attributes = new Dictionary<string, string> {
                    { "FifoQueue", "true" }
                }
            };

            var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;

            var myQueueUrl = createQueueResponse.QueueUrl;

            var listQueuesRequest = new ListQueuesRequest();
            var listQueuesResponse = sqs.ListQueuesAsync(listQueuesRequest);

            Console.WriteLine("List of Amazon SQS Queues\n");
            foreach (var queue in listQueuesResponse.Result.QueueUrls)
            {
                Console.WriteLine($"QueueUrl: {queue}");
            }

            Console.WriteLine("Sending a message to the queue\n");
            var sqsMessageRequest = new SendMessageRequest
            {
                QueueUrl = myQueueUrl,
                MessageBody = "Email information"
            };

            sqs.SendMessageAsync(sqsMessageRequest);

            Console.WriteLine("Finished sending message to the SQS queue\n");

            Console.ReadLine();
        }
    }
}
