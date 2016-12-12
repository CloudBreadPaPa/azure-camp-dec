<?php

require_once 'vendor\autoload.php';

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Common\ServiceException;

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=<저장소계정>;AccountKey=<어카운트키>";

// REST proxy 생성
$blobRestProxy = ServicesBuilder::getInstance()->createBlobService($connectionString);

try {
    // 여러 blob 리스팅
    $blob_list = $blobRestProxy->listBlobs("phpcontainer");
    $blobs = $blob_list->getBlobs();

    foreach($blobs as $blob)
    {
        echo $blob->getName().": ".$blob->getUrl()."<br />";
    }
}
catch(ServiceException $e){
    $code = $e->getCode();
    $error_message = $e->getMessage();
    echo $code.": ".$error_message."<br />";
}

?>
