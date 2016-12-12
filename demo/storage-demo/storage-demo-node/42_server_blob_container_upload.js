// 패키지 import 수행
var azure = require('azure-storage');
var blobService = azure.createBlobService('dwazurecampstor09', 'ls5oNGYKMk2QZxGz2kb3GkZI29UAmONGkpKgFJvIgAfpY67nLplQN/FL/68EtYSYfM80eGdvkgo0WtgVjxZbpw==');

// blob 업로드 수행
blobService.createBlockBlobFromLocalFile('nodecontainer', 'AzureDC', 'azure_center.png', function(error, result, response) {
  publicAccessLevel: 'blob'
}, function(error, result, response) {
  if (!error) {
	console.log(response);
  } else {
	console.log(error);
  }
});
