var url = "http://120.26.211.168";
var port = "8081";
var cApiUrl = url + ":" + port + "/api/mobile/v1/";
var bApiUrl = url + ":" + port + "/api/business/v1/";
var verificationError = "验证码获取失败";
var errorMessage = "网络连接异常";
var requestError = "网络连接失败";
var requestFail = "网络连接错误";

//-------------------------------------------------------------------------
//var sessionId = $.cookie('greensjob_mobile_user_cookies');
var sessionId;
var openID = '';

function sessionIdAction(method, kJson) {
	if (sessionId == undefined) {
		window.location.href = "../OAuth2/Index";
		//		alert('sessionId == undefined');
	} else {
		//		alert('getOpenId:' + sessionId);
		$.ajax({
			//					url: "http://172.16.2.40:57802/api/business/v1/sendverificationcode",
			url: cApiUrl + "getOpenId",
			type: "post",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: "{ 'sessionId': '" + sessionId + "' }",
			success: function(data) {
				//              $("#result").val(data);
				if (data.code == 1) {
					openID = data.Data;
					httpPost(method, kJson);
					alert(openID);
				} else {
					alert(data.message);
					window.location.href = "../OAuth2/Index";
				}
			},
			error: function(e) {
				//              $("#result").val("error:" + e.responseText);
				alert("错误");
			},
			fail: function(e) {
				//              $("#result").val(e);
				alert("失败");
			}
		});
	}

}

function shareAction(shareUrl) {

	var tmpUrl = shareUrl.split('?');
	var kUrl;
	if (tmpUrl.length > 1) {
		kUrl = tmpUrl[0]
	} else {
		kUrl = shareUrl;
	}

	alert(kUrl);
	$.ajax({
		url: "../OAuth2/GetJsSdkUiPackage",
		type: "post",
		dataType: "json",
		data: {url:kUrl},
		success: function(data) {
			//              $("#result").val(data);
			alert(data.AppId);
			alert(data.Timestamp);
			alert(data.NonceStr);
			alert(data.Signature);

			weixinCallBack(data);
		},
		error: function(e) {
			//              $("#result").val("error:" + e.responseText);
			alert("错误");
		},
		fail: function(e) {
			//              $("#result").val(e);
			alert("失败");
		}
	});
}

function postAction(method, kJson) {
	//	sessionIdAction(method, kJson);
	httpPost(method, kJson);
}

function httpPost(method, kJson) {

	$.ajax({
		url: cApiUrl + method,
		//						url: apiUrl + "sendverificationcode",
		type: "post",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: kJson,
		success: function(data) {
			var jsonobj = eval(data);
			if (jsonobj.code == 1) {
				successCallBack(jsonobj);
			} else {
				mui.toast(jsonobj.message);
			}
		},
		error: function(e) {
			mui.toast(requestError);
			errorCallBack();
		},
		fail: function(e) {
			mui.toast(requestFail);
			failCallBack();
		}
	});
}

function weixinCallBack(data) {

}

function successCallBack(data) {

}

function errorCallBack() {

}

function failCallBack() {

}