<?php
require_once 'vendor\autoload.php';
//ini_set('display_errors', 1);
//error_reporting(~0);

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Common\ServiceException;

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=dwazurecampstor09;AccountKey=ls5oNGYKMk2QZxGz2kb3GkZI29UAmONGkpKgFJvIgAfpY67nLplQN/FL/68EtYSYfM80eGdvkgo0WtgVjxZbpw==";

// Azure의 table storage를 위한 REST proxy 생성
$tableRestProxy = ServicesBuilder::getInstance()->createTableService($connectionString);

/////////////////////////////////////////////////////////////////
// 03 SELECT 1건 조회 처리
/////////////////////////////////////////////////////////////////
try {
    // 조회
    $result = $tableRestProxy->getEntity("phptable", "VSTechUp", "{207530FD-B4EC-477E-1617-2516A51B872A}");      //guid값
}
catch(ServiceException $e){
    $code = $e->getCode();
    $error_message = $e->getMessage();
    echo $code.": ".$error_message."<br />";
}

//결과 조회
$entity = $result->getEntity();
echo $entity->getPartitionKey().":".$entity->getRowKey();

?>