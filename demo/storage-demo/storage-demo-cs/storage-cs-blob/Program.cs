using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace storage_cs_blob
{
    class Program
    {
        static void Main(string[] args)
        {
            // blob 컨테이너 생성
            //CreateContainer();

            // UploadBlob();

            ListBlob();

            return;
        }

        public static void CreateContainer()
        {
            // 저장소 연결 문자열 처리
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // client 개체 생성
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // 컨테이너 개체 참조
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // 컨테이너가 없으면 생성
            container.CreateIfNotExists();

            return;
        }

        public static void UploadBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");
            using (var fileStream = System.IO.File.OpenRead(@"c:\temp\azure_center.png"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            // Portal storage explorer 에서 체크
            // container 의 access policy 체크 기본 private
        }

        public static void ListBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }
    }
}
