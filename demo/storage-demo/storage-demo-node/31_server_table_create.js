
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