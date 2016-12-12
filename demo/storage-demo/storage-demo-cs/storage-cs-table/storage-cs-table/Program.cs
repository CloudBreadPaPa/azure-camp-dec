using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace storage_cs_table
{
    // entity 추가
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }
        public CustomerEntity() { }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CreateTable();

             InsertEntity();

             ListEntity();

            return;
        }

        public static void CreateTable()
        {
            // 저장소 연결 문자열 처리
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // table 클라이언트 개체 생성
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // 테이블 참조 설정 
            CloudTable table = tableClient.GetTableReference("people");

            // 테이블이 존재하지 않으면 생성
            table.CreateIfNotExists();
        }

        public static void InsertEntity()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("people");
            CustomerEntity customer1 = new CustomerEntity("Harp", "Walter");
            customer1.Email = "Walter@contoso.com";
            customer1.PhoneNumber = "425-555-0101";
            TableOperation insertOperation = TableOperation.Insert(customer1);
            // entity 추가 수행
            table.Execute(insertOperation);
        }

        public static void ListEntity()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("people");

            // 전체 조회
            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>(); 

            // where 조건 조회
            //TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Harp"));
            foreach (CustomerEntity entity in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                    entity.Email, entity.PhoneNumber);
            }
        }
    }
}
