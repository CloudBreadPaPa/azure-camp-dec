<?php

require_once 'vendor\autoload.php';

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Blob\Models\CreateContainerOptions;
use WindowsAzure\Blob\Models\PublicAccessType;
use WindowsAzure\Common\ServiceException;

// https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-blobs/

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=<저장소계정>;AccountKey=<저장소키>";

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
