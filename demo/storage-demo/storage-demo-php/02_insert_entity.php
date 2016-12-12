<?php
require_once 'vendor\autoload.php';
ini_set('display_errors', 1);
error_reporting(~0);

use WindowsAzure\Common\ServicesBuilder;
use WindowsAzure\Common\ServiceException;
use WindowsAzure\Table\Models\Entity;       //추가
use WindowsAzure\Table\Models\EdmType;      //추가

// Storage의 connection string 제공
$connectionString = "DefaultEndpointsProtocol=http;AccountName=dwazurecampstor09;AccountKey=ls5oNGYKMk2QZxGz2kb3GkZI29UAmONGkpKgFJvIgAfpY67nLplQN/FL/68EtYSYfM80eGdvkgo0WtgVjxZbpw==";

// Azure의 table storage를 위한 REST proxy 생성
$tableRestProxy = ServicesBuilder::getInstance()->createTableService($connectionString);

/////////////////////////////////////////////////////////////////
// 02 테이블에 엔터티 추가
/////////////////////////////////////////////////////////////////
//rowkey를 위해 guid 생성
function getGUID(){
    if (function_exists('com_create_guid')){
        return com_create_guid();
    }else{
        mt_srand((double)microtime()*10000);    //optional for php 4.2.0 and up.
        $charid = strtoupper(md5(uniqid(rand(), true)));
        $hyphen = chr(45);// "-"
        $uuid = chr(123)// "{"
            .substr($charid, 0, 8).$hyphen
            .substr($charid, 8, 4).$hyphen
            .substr($charid,12, 4).$hyphen
            .substr($charid,16, 4).$hyphen
            .substr($charid,20,12)
            .chr(125);// "}"
        return $uuid;
    }
}

$dwguid = getGUID(); //guid 처리
$entity = new Entity();
$entity->setPartitionKey("VSTechUp");
$entity->setRowKey($dwguid);
$entity->addProperty("Description", null, "VS가 쵝오에요~");
$entity->addProperty("DueDate",
                     EdmType::DATETIME,
                     new DateTime("2015-11-14T08:15:00-08:00")); 
$entity->addProperty("Location", EdmType::STRING, "광화문");

try{
    $tableRestProxy->insertEntity("phptable", $entity);      //테이블명
}
catch(ServiceException $e){
    $code = $e->getCode();
    $error_message = $e->getMessage();
}

?>