// 패키지 import 수행
var azure = require('azure-storage');

var blobService = azure.createBlobService('dwazurecampstor09', 'ls5oNGYKMk2QZxGz2kb3GkZI29UAmONGkpKgFJvIgAfpY67nLplQN/FL/68EtYSYfM80eGdvkgo0WtgVjxZbpw==');

// blob 컨테이너 생성, Access Level은 blob
blobService.createContainerIfNotExists('nodecontainer', {
  publicAccessLevel: 'blob'
}, function(error, result, response) {
  if (!error) {
	console.log(response);
  } else {
	console.log(error);
  }
});
