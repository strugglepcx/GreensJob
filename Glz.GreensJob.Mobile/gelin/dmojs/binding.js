var seconds = 120; //记数时间
var tmpSeconds = 120; //记数时间
var tmpVerificationCode;

var handle; //事件柄 
var state = false;
//开始记数器 
function startTimer() {
	handle = setInterval("timer()", 1000);
}
//结束记数器 
function stopTimer() {
	clearInterval(handle);
	seconds = tmpSeconds;
}
//计数器 
function timer() {
	if (seconds <= 0) {
		refresh();
	} else {
		seconds -= 1;
		$("#verification").text(seconds + "秒后获取");
	}
}

function refresh() {
	stopTimer();
	state = false;
	seconds = tmpSeconds;
	$("#verification").removeClass("gray");
	$("#verification").addClass("green");
	$("#verification").text("获取验证码");
}

function setState() {
	$("#verification").removeClass("green");
	$("#verification").addClass("gray");
	setTimeout("startTimer()", 0);
	state = true;
}
//获取手机验证码
function getVerification() {
	var phone = $('#phone').val();
	var patrn = /^1\d{10}$/;
	if (patrn.test(phone)) {
		setState();
		$.ajax({
			url: bApiUrl + "getverifycode",
			//						url: apiUrl + "sendverificationcode",
			type: "post",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '{userMobileNumber:' + phone + ',"platform":1}',
			success: function(data) {
				var jsonobj = eval(data);
				if (jsonobj.code == 1) {
					tmpVerificationCode = jsonobj.Data.verificationCode;
					tmpSeconds = jsonobj.Data.validDuration;
					seconds = tmpSeconds;
					mui.toast(tmpVerificationCode);
				} else {
					mui.toast(jsonobj.message);
				}
			},
			error: function(e) {
				refresh();
				//				mui.alert(verificationError);
				mui.toast(verificationError);
			},
			fail: function(e) {
				refresh();
				//				mui.alert(verificationError);
				mui.toast(verificationError);
			}
		});
	} else {
		//		mui.alert("请输入正确的手机号!");
		mui.toast("请输入正确的手机号!");
	}
}
$(document).ready(function() {
	$("#verification").click(function() {
		if (!state) {
			getVerification();
		} else {}
	});
});
$(document).ready(function() {
	$("#submit").click(function() {
		var code = $('#verificationCode').val();
		var phone = $('#phone').val();
		if (!$.trim(phone)) {
			//			mui.alert("请输入手机号");
			mui.toast("请输入手机号!");
		} else {
			if (!$.trim(code)) {
				//				mui.alert("请输入验证码");
				mui.toast("请输入验证码!");
			} else {
				if (tmpVerificationCode != code) {
					mui.toast("验证码错误！");
				} else {

					$.ajax({
						url: cApiUrl + "bindingMobile",
						type: "post",
						contentType: "application/json; charset=utf-8",
						dataType: "json",
						data: {
							userMobileNumber: phone,
							verificationCode: code,
							userID: openId,
						},
						success: function(data) {
							console.log(data);
							window.location.href = "home_gl.html";
						},
						error: function(e) {
							mui.toast(errorMessage);
						},
						fail: function(e) {
							mui.toast(errorMessage);
						}
					});
				}
			}
		}
	});
});