using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types

namespace storage_cs_queue
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateQueue();
             InsertQueueMessage();
             GetQueueMessage();
        }

        public static void CreateQueue()
        {
            // 연결 문자열 처리
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // 큐 클라이언트 개체 생성
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // 큐 참조 설정
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // 큐가 없으면 생성
            queue.CreateIfNotExists();
        }

        public static void InsertQueueMessage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("myqueue");
            // queue.CreateIfNotExists();
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            
            // queue에 메세지 추가
            queue.AddMessage(message);
        }

        public static void GetQueueMessage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // queue에서 메세지 가져오기
            CloudQueueMessage retrievedMessage = queue.GetMessage();

            Console.WriteLine(retrievedMessage.AsString);

            // 기본 요건인 30초 내에 메세지를 처리하고 삭제 처리
            queue.DeleteMessage(retrievedMessage);
        }
    }
}
