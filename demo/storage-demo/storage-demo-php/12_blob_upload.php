<?php

require_once 'vendor\autoload.php';

ini_set('display_errors', 1);
error_reporting(~0);

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Common\ServiceException;

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=<저장소계정>;AccountKey=<어카운트키>";

// REST proxy 생성
$blobRestProxy = ServicesBuilder::getInstance()->createBlobService($connectionString);

$content = fopen("azure_center.png", "r");      //파일 접근 권한 주의 fopen 참조
$blob_name = "PHPUpblob";   // blob 이름 지정

try {
    //Upload blob
    $blobRestProxy->createBlockBlob("phpcontainer", $blob_name, $content);
}
catch(ServiceException $e){
    // Handle exception based on error codes and messages.
    // Error codes and messages are here:
    // http://msdn.microsoft.com/library/azure/dd179439.aspx
    $code = $e->getCode();
    $error_message = $e->getMessage();
    echo $code.": ".$error_message."<br />";
}

?>
