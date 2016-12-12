# azure camp dec
이 repo는 2016년 12월 예정된 Azure dev camp 2일차 PaaS 과정에 대한 코드와 hands on 자료를 제공  

## Application 개발을 위한 Azure App Service  
### Web App / API app / Mobile App 생성과 배포

### Demo : 웹앱 생성  
- Azure Portal에 로그온 후 리소스 그룹을 생성  
https://azure.microsoft.com/en-us/documentation/articles/resource-group-portal/  

- Web App을 생성  
https://azure.microsoft.com/en-us/documentation/articles/app-service-web-get-started/  

### Source control
- 생성한 Web App에서 FTP, Git, Github 등으로 배포  
FTP : https://azure.microsoft.com/en-us/documentation/articles/web-sites-deploy/  
Git : https://azure.microsoft.com/en-us/documentation/articles/app-service-deploy-local-git/  
```
git init  
git add .  
git commit -m "initial commit"  
git remote add azure [URL for remote repository]  
git push azure master  
//코드 수정 작업 후  
git add .  
git commit -m "second commit"  
git push azure master  
//배포 확인  
```
Github : https://azure.microsoft.com/en-us/documentation/articles/app-service-web-arm-from-github-provision/  
```
repo를 github에 생성  
Azure portal에서 연속 배포 설정  
설정 후 github의 repo를 Azure Portal에서 지정  
```

### 다양한 언어 지원
- Web App과 C# - ASP.NET
https://azure.microsoft.com/en-us/documentation/articles/web-sites-dotnet-get-started/  
```
VS에서 ASP.NET 프로젝트 생성  
빌드, 디버그, 실행  
생성된 Project를 VS에서 게시 수행  
```

- Web App과 PHP  
https://azure.microsoft.com/en-us/documentation/articles/app-service-web-php-get-started/  
```
index.php 생성  
phpinfo() 코드 추가  
WebMatrix 또는 git을 이용해 publish 수행  
Web App 설정에서 php version 및 php 관련 cofnig 수행  
```


- Web App과 node.js  
https://azure.microsoft.com/en-us/documentation/articles/app-service-web-nodejs-get-started/  
```
app.js 생성  
console.log 코드 추가  
git 등을 이용해 publish 수행  
```

- Web App과 Python  
https://azure.microsoft.com/en-us/documentation/articles/web-sites-python-ptvs-django-mysql/  
```
//todo  
```
- Web App과 Java  
https://azure.microsoft.com/en-us/documentation/articles/web-sites-java-get-started/  
```
//todo  
```

### Demo : WebJobs  
백그라운드에서 수행되는 작업을 WebJob 으로 실행  
https://azure.microsoft.com/en-us/documentation/articles/web-sites-create-web-jobs/  

#### script로 수행
```
// http://requestb.in/ 사이트를 이용해 webjob을 inpection  
// req.php 파일 생성  
<?php  
    $result = file_get_contents('http://requestb.in/경로');  
    echo $result;  
?>  

// settings.job 파일 생성  
{  
  "schedule": "0 */2 * * * *"  
}  

// 두 파일을 zip으로 묶은 후 webjob으로 업로드  
```

#### C# 프로젝트로 데모 수행
Deploy WebJobs using Visual Studio  
https://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-deploy-webjobs/  

```
// demo\webjob-cs-with-webapp 프로젝트 참조  
// console 프로젝트 또는 Azure WebJob 프로젝트 템플릿으로 프로젝트 생성  
// ASP.NET web project 생성 후 추가로 console project 생성  
// console project에서 reqeustb.in을 이용해 요청 테스트  
// Web rpoject에서 console 프로젝트를 webjob으로 추가  
// Web App 배포 수행  
```

### 크기조절 및 테스트
Web App 크기조절  
https://azure.microsoft.com/en-us/documentation/articles/web-sites-scale/  
```
// Web App의 App Service Plan과 크기조절
```
참고정보 : Pattern & Practice - Transient Fault Handling  
https://msdn.microsoft.com/en-us/library/dn440719(v=pandp.60).aspx 

## Azure 데이터 저장소(Azure Data Storage)  
### Azure blob 저장소
Azure Storage 개발에 유용한 Storage Explorer 도구 다운로드 
http://storageexplorer.com/  

Storage Exploerer 설치 후 Azure Portal에서 저장소 계정(Storage Account)의 엑세스 키 - 연결문자열을 넣으면 해당 저장소 계정에 Storage Explorer로 바로 접근 가능합니다.  

#### C# 코드  
참고링크 : https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/  
```
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
```

#### node.js
```
// 아래 링크에서 정보 확인 https://azure.microsoft.com/en-us/documentation/articles/storage-nodejs-how-to-use-blob-storage/

// 설치
// npm install azure-storage

// 패키지 import 수행
var azure = require('azure-storage');

// connection string 작업 
// environment variable을 구성 | Azure 위에서 구성도 가능
var tableService = azure.createTableService('<저장소이름>', '<저장소키>');

// 테이블 생성
tableService.createTableIfNotExists('nodetable', function(error, result, response) {
  if (!error) {
    // 수행완료
	console.log('ok');
	console.log(response);
  } else {
	  console.log(error);
  }
});
```
#### PHP
```
<?php
// composer를 이용해 PHP Azure client를 설치
// https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-blobs/

require_once 'vendor\autoload.php';

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Blob\Models\CreateContainerOptions;
use WindowsAzure\Blob\Models\PublicAccessType;
use WindowsAzure\Common\ServiceException;

// https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-blobs/

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=<저장소이름>;AccountKey=<저장소키>";

// REST proxy 생성
$blobRestProxy = ServicesBuilder::getInstance()->createBlobService($connectionString);

$createContainerOptions = new CreateContainerOptions();

//setPublicAccess 접근 정책 설정

// CONTAINER_AND_BLOBS:
// 전체 엑세스 권한
//
// BLOBS_ONLY:
// blob들에 대해서만 읽기 권한. 
$createContainerOptions->setPublicAccess(PublicAccessType::CONTAINER_AND_BLOBS);

// 컨테이너 메타데이터 설정
$createContainerOptions->addMetaData("VSTechUp", "Visual Studio");
$createContainerOptions->addMetaData("Azure", "Cloud");

try {
    // 컨테이너 생성
    $blobRestProxy->createContainer("phpcontainer", $createContainerOptions);
}
catch(ServiceException $e){
    // 에러 핸들링
    $code = $e->getCode();
    $error_message = $e->getMessage();
    echo $code.": ".$error_message."<br />";
}

?>

```

### Azure table 저장소
#### C# 코드  
참고링크 : https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-tables/
```
// 저장소 연결 문자열 처리
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));

// table 클라이언트 개체 생성
CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

// 테이블 참조 설정 
CloudTable table = tableClient.GetTableReference("people");

// 테이블이 존재하지 않으면 생성
table.CreateIfNotExists();
```
#### node.js
```
// 아래 링크에서 정보 확인 https://azure.microsoft.com/en-us/documentation/articles/storage-nodejs-how-to-use-blob-storage/

// 설치
// npm install azure-storage

// 패키지 import 수행
var azure = require('azure-storage');

// connection string 작업 
// environment variable을 구성 | Azure 위에서 구성도 가능
var tableService = azure.createTableService('<저장소계정>', '<저장소키>');

// 테이블 생성
tableService.createTableIfNotExists('nodetable', function(error, result, response) {
  if (!error) {
    // 수행완료
	console.log('ok');
	console.log(response);
  } else {
	  console.log(error);
  }
});
```

#### PHP
```
<?php
// https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-table-storage/
require_once 'vendor\autoload.php';
//ini_set('display_errors', 1);
//error_reporting(~0);

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Common\ServiceException;

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=<저장소계정>;AccountKey=<어카운트키>";

// Azure의 table storage를 위한 REST proxy 생성
$tableRestProxy = ServicesBuilder::getInstance()->createTableService($connectionString);

/////////////////////////////////////////////////////////////////
// 01 테이블 생성
/////////////////////////////////////////////////////////////////
try {
    // 테이블 생성
    $tableRestProxy->createTable("phptable");
}
catch(ServiceException $e){
    $code = $e->getCode();
    $error_message = $e->getMessage();
    echo $code.": ".$error_message."<br />";
}
?>
```

### Azure queue 저장소
#### C# 참고링크
https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-queues/  
```
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
```

#### node.js 참고링크
https://azure.microsoft.com/en-us/documentation/articles/storage-nodejs-how-to-use-queues/  
```
todo  
```

#### PHP 참고링크
https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-queues/  
```
todo  
```

### Azure files
참고링크 : https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-files/  
주의사항으로, outbound 445 가 열려 있어야 하는 제한이 있으며, ISP에 따라서 다름.  

## Azure SQL 데이터 베이스(SQL Database)  
### SQL Database  
소개 및 생성  
VS 또는 SSMS에서 연결  
SQL Database vs VM  

#### C# 코드
참고링크 : https://msdn.microsoft.com/library/mt718320.aspx  
```
using System;
using C = System.Data.SqlClient;

namespace azure_sql_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new C.SqlConnection(
                "Server=tcp:서버명.database.windows.net,1433;Initial Catalog=DB명;Persist Security Info=False;User ID=유저아이디;Password=비밀번호;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    ))  // 연결 문자열을 복사하고 Azure SQL Database 생성시 지정한 user id와 pwd로 변경. 서버명과 DB명은 자동으로 연결 문자열에 지정
            {
                connection.Open();
                Console.WriteLine("Connected successfully.");

                Console.WriteLine("Press any key to finish...");
                Console.ReadKey(true);
            }
        }
    }
}
```
#### PHP
참고링크 : https://msdn.microsoft.com/library/mt720665.aspx  
```
<?php 
function OpenConnection()  
{  
	try  
	{  
		$serverName = "tcp:서버명.database.windows.net,1433";  
		$connectionOptions = array("Database"=>"DB명",  
			"Uid"=>"유저아이디", "PWD"=>"비밀번호");  
		$conn = sqlsrv_connect($serverName, $connectionOptions);  
		if($conn == false)  
			die(FormatErrors(sqlsrv_errors()));  
	}  
	catch(Exception $e)  
	{  
		echo("Error!");  
	}  
}  
?>
```

#### node
참고링크 :  
https://msdn.microsoft.com/library/mt715784.aspx   
https://msdn.microsoft.com/library/mt652094.aspx   

```
npm install tedious
```
```
var Connection = require('tedious').Connection;  
var config = {  
	userName: 'yourusername',  
	password: 'yourpassword',  
	server: 'yourserver.database.windows.net',  
	// If you are on Microsoft Azure, you need this:  
	options: {encrypt: true, database: 'AdventureWorks'}  
};  
var connection = new Connection(config);  
connection.on('connect', function(err) {  
// If no error, then good to proceed.  
	console.log("Connected");  
});  
```

- Elastic Pool  
- OSS에서 연결 : https://azure.microsoft.com/en-us/documentation/articles/sql-database-libraries/  

### DocumnetDB
소개  
NoSQL tutorial: Build a DocumentDB C# console application  
https://azure.microsoft.com/en-us/documentation/articles/documentdb-get-started-quickstart/

#### C# 코드
참고링크 : https://azure.microsoft.com/en-us/documentation/articles/documentdb-get-started-quickstart/#quickstart  
```
// app.config 파일의 내용을 key 값을 생성한 documentdb의 정보로 수정
...
<appSettings>
	<add key="EndpointUri" value="엔드포인트URI로변경" />
	<add key="PrimaryKey" value="프라이머리키로 변경" />
</appSettings>
...
```

#### node.js
참고링크 : https://azure.microsoft.com/en-us/documentation/articles/documentdb-nodejs-get-started/  
```
npm install  
```
```
// config.js 파일의 정보를 수정
...
config.endpoint = "엔드포인트URI";
config.primaryKey = "프라이머리키";
...
```
이후 코드를 실행하면 console에 documentdb의 작업 내용을 source 흐름대로 처리


### HDInsight
소개  
Hadoop tutorial: Get started using Hadoop in HDInsight on Windows  
https://azure.microsoft.com/en-us/documentation/articles/hdinsight-hadoop-tutorial-get-started-windows/  
Hadoop tutorial: Get started using Linux-based Hadoop in HDInsight  
https://azure.microsoft.com/en-us/documentation/articles/hdinsight-hadoop-linux-tutorial-get-started/  


## Azure Machine Learning
### Machine Learning 소개
Azure Machine Learning 참고링크 : https://azure.microsoft.com/en-us/documentation/services/machine-learning/  
### 모델 생성
기본 제공되는 Adult census income 데이터를 이용해 classification ML을 수행하는 모델 생성

### 학습 모델
필요한 property를 선정해 traning 시키고 결과를 도출

### 서비스 생성 및 API로 사용
노출된 ML API를 C#코드, R 또는 Python으로 활용하는 방법   
C#, node.js + (Python) 을 이용한 Azure Machine Learning API 데모 코드   

이 Repository는 모각코x모두 아홉번째 모임 발표에 사용된 demo 코드.
https://github.com/CloudBreadPaPa/mogakko-9th

### C#을 이용한 Visual Studio 실행 절차
ml-demo\cs 폴더에서 프로젝트 실행
코드의 API KKey를 자신의 Azure ML API Key로 수정

```
...
const string apiKey = "API Key"; // Azure ML이 제공하는 API Key
...
client.BaseAddress = new Uri("Azure ML이 제공하는 API URL");
...
```

### node.js로 ML API를 호출하는 절차
ml-demo\node\app.js 파일의 API Key를 자신의 API Key로 수정

```
...
var host = 'asiasoutheast.services.azureml.net'	//제공하는 HOST 경로
var path = 'HOST 이후 URL Path 정보'
...
var api_key = 'API Key 정보'
...
```

node.js로 Azure ML을 call 예제 링크 :
https://blogs.msdn.microsoft.com/bigdatasupport/2016/02/18/how-to-call-a-azure-machine-learning-web-service-from-nodejs/

node reqeust package를 이용하면 더 쉽게 node에서 Azure ML을 호출 가능할 것으로 예상됨.
참고링크 : https://github.com/request/request

### Python을 이용한 Azure ML 호출 코드
Python을 호출하기 위한 데모
기본 code는 Python 2.7.x 기준. 아래 코드 데모는 아마도 한글로 인해 unicode 처리가 필요할 수 있음.

```
import urllib2
# If you are using Python 3+, import urllib instead of urllib2

import json 


data =  {

        "Inputs": {

                "input1":
                {
                    "ColumnNames": ["idx", "나이", "프로모션참여수", "식별자", "일평균게임플레이분", "90일내아이템구매수", "게임레벨범위", "보유크리스탈", "유입경로", "인종", "성별", "가입코드", "구매번호", "주당접속수", "가입국가", "이탈여부"],
                    "Values": [ [ "0", "0", "0", "0", "0", "0", "0", "0", "value", "value", "value", "0", "0", "0", "value", "value" ], [ "0", "0", "0", "0", "0", "0", "0", "0", "value", "value", "value", "0", "0", "0", "value", "value" ], ]
                },        },
            "GlobalParameters": {
}
    }

body = str.encode(json.dumps(data))

url = 'AzureML이 제공하는 URL'
api_key = 'AzureML이 제공하는 APIKey' # Replace this with the API key for the web service
headers = {'Content-Type':'application/json', 'Authorization':('Bearer '+ api_key)}

req = urllib2.Request(url, body, headers) 

try:
    response = urllib2.urlopen(req)

    # If you are using Python 3+, replace urllib2 with urllib.request in the above code:
    # req = urllib.request.Request(url, body, headers) 
    # response = urllib.request.urlopen(req)

    result = response.read()
    print(result) 
except urllib2.HTTPError, error:
    print("The request failed with status code: " + str(error.code))

    # Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
    print(error.info())

    print(json.loads(error.read()))

```

마찬가지로, API Key 변수를 자신의 Key로 변경하고 수행 해야 함.

```
...
url = 'AzureML이 제공하는 URL'
api_key = 'AzureML이 제공하는 APIKey' # Replace this with the API key for the web service
...
```

이부분을 변경하고 수행

문서의 끝
