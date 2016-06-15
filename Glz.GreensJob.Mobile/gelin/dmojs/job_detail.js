var app = angular.module('jobDetailApp', ["ngCookies"]);
var kTitle = "职位详情";
var kDes = "应聘要求应聘要求应聘要求应聘要求应聘要求应聘要求应聘要求";
var kLink = "http://1.greensjob.applinzi.com/gelinapp/gelin/job_detail.html";
var kImgUrl = "http://demo.open.weixin.qq.com/jssdk/images/p2166127561.jpg";

wx.config({
	debug: true,
	appId: 'wxa81616d5a7e1a509',
	timestamp: 1460438519,
	nonceStr: 'Qj5JbrH4qe7mp1vV',
	signature: 'efde84f941f95a158bec10b7aef11fb87dcc86d6',

	jsApiList: [
		'checkJsApi'
	]
});
wx.ready(function() {
	wx.checkJsApi({
		jsApiList: [
			'getNetworkType',
			'previewImage'
		],
		success: function(res) {
			alert(JSON.stringify(res));
		}
	});

	wx.onMenuShareAppMessage({
		title: kTitle,
		desc: kDes,
		link: kLink,
		imgUrl: kImgUrl,
		trigger: function(res) {
			// 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
			alert('用户点击发送给朋友');
		},
		success: function(res) {
			alert('已分享');
		},
		cancel: function(res) {
			alert('已取消');
		},
		fail: function(res) {
			alert(JSON.stringify(res));
		}
	});

});

//-----------------------------------------------------------
angular.module("jobDetailApp", []).controller("telController", function($scope) {
	$scope.phoneNumber = {
		title: '137000000'
	};
	console.log($scope.phoneNumber.title);
	$scope.telModel;
});

//-----------------------------------------------------------
mui.init();
(function($, doc) {
	//			$.init({
	//				statusBarBackground: '#f7f7f7'
	//			});
	//检查 "登录状态/锁屏状态" 结束
	var shoucangButton = doc.getElementById('shoucangBtn');
	var notebookButton = doc.getElementById('notebookBtn');
	var phoneButton = doc.getElementById('phoneBtn');
	var complainButton = doc.getElementById('complainBtn');
	shoucangButton.addEventListener('tap', function(event) {
		mui.toast('欢迎收藏');
	});
	notebookButton.addEventListener('tap', function(event) {
		//		window.Location.href="job_search.html";
		window.location.href = "enrol_date.html";
	});

}(mui, document));