// 패키지 import 수행
var azure = require('azure-storage');

var blobService = azure.createBlobService('dwazurecampstor09', 'ls5oNGYKMk2QZxGz2kb3GkZI29UAmONGkpKgFJvIgAfpY67nLplQN/FL/68EtYSYfM80eGdvkgo0WtgVjxZbpw==');

// blob 리스트 수행
blobService.listBlobsSegmented('nodecontainer', null, function(error, result, response){
  if (!error) {
	console.log(response);
	console.log(result);
  } else {
	console.log(error);
  }
});
