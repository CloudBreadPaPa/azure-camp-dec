<?php
// https://azure.microsoft.com/en-us/documentation/articles/storage-php-how-to-use-table-storage/


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