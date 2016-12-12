// 참고 링크 : https://github.com/CloudBreadPaPa/mogakko-9th
// node.js call 예제 링크 : https://blogs.msdn.microsoft.com/bigdatasupport/2016/02/18/how-to-call-a-azure-machine-learning-web-service-from-nodejs/

//maml-server.js
var http = require('http');
var https = require('https');
var querystring = require('querystring');
var fs = require('fs');

function getPred(data) {
console.log('===getPred()===');
var dataString = JSON.stringify(data)
var host = 'asiasoutheast.services.azureml.net'	//제공하는 HOST 경로
var path = 'HOST 이후 URL Path 정보'
var method = 'POST'
var api_key = 'API Key 정보'
var headers = {'Content-Type':'application/json', 'Authorization':'Bearer ' + api_key};
var options = {
host: host,
port: 443,
path: path,
method: 'POST',
headers: headers
};

console.log('data: ' + data);
console.log('method: ' + method);
console.log('api_key: ' + api_key);
console.log('headers: ' + headers);
console.log('options: ' + options);

var reqPost = https.request(options, function (res) {
console.log('===reqPost()===');
console.log('StatusCode: ', res.statusCode);
console.log('headers: ', res.headers);

res.on('data', function(d) {
process.stdout.write(d);
});
});

// Would need more parsing out of prediction from the result
reqPost.write(dataString);
reqPost.end();
reqPost.on('error', function(e){
console.error(e);
});
}

//Could build feature inputs from web form or RDMS. This is the new data that needs to be passed to the web service.
function buildFeatureInput(){
console.log('===performRequest()===');
var data = {
'Inputs': {
'input1': {
'ColumnNames': ["idx", "나이", "프로모션참여수", "식별자", "일평균게임플레이분", "90일내아이템구매수", "게임레벨범위", "보유크리스탈", "유입경로", "인종", "성별", "가입코드", "구매번호", "주당접속수", "가입국가", "이탈여부"],
'Values': [ [ "0", "10", "100", "99", "120", "40", "0", "0", "value", "value", "value", "0", "0", "0", "value", "value" ], ]
},
},
'GlobalParameters': {}
}
getPred(data);
}

function send404Reponse(response) {
response.writeHead(404, {'Context-Type': 'text/plain'});
response.write('Error 404: Page not Found!');
response.end();
}

function onRequest(request, response) {
if(request.method == 'GET' && request.url == '/' ){
response.writeHead(200, {'Context-Type': 'text/plain'});
fs.createReadStream('./index.html').pipe(response);
}else {
send404Reponse(response);
}
}

http.createServer(onRequest).listen(8050);
console.log('Server is now running on port 8050');
buildFeatureInput();
