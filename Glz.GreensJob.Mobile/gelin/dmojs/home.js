var app = angular.module('commApp', []);
var jsonobj;
var pageIndex = 0;

mui.init({
	swipeBack: true //启用右滑关闭功能

});
var slider = mui("#slider");
slider.slider({
	interval: 3000
});

mui.getJSON('/gelin/data/home.json', null, function(data) {
	jsonobj = eval(data);
	//				console.log(jsonobj.distanceJson[0].name);
	//					var addressTable = document.body.querySelector('.mui-table-view-inverted');
	var distanceTable = document.body.querySelector('.distanceTable');
	var categoryTable = document.body.querySelector('.categoryTable');
	var timeTable = document.body.querySelector('.timeTable');
	var settlementTable = document.body.querySelector('.settlementTable');
	//				var cells = document.body.querySelectorAll('.distanceCell');
	for (var i = 0; i < jsonobj.addressJson.length; i++) {
		var kIndex = jsonobj.addressJson[i].index;
		var kName = jsonobj.addressJson[i].name;
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell addressCell';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<a href="javascript:;">' + kName + '</a>';
		//下拉刷新，新纪录插到最前面；
		//						addressTable.appendChild(li);
	}
	//-------------------------------------------------------
	for (var i = 0; i < jsonobj.distanceJson.length; i++) {
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell mui-media distanceCell';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<div class="item" onclick="distanceOnClick(' + jsonobj.distanceJson[i].index + ')"><a href="#" >' + jsonobj.distanceJson[i].name + '</a></div>';
		//下拉刷新，新纪录插到最前面；
		distanceTable.appendChild(li);
	}
	//-------------------------------------------------------
	for (var i = 0; i < jsonobj.categoryJson.length; i++) {
		var kIndex = jsonobj.categoryJson[i].index;
		var kName = jsonobj.categoryJson[i].name;
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell mui-media distanceCell';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<div class="item" onclick="categoryOnClick(' + kIndex + ')"><a href="#" >' + kName + '</a></div>';
		//下拉刷新，新纪录插到最前面；
		categoryTable.appendChild(li);
	}
	//-------------------------------------------------------
	for (var i = 0; i < jsonobj.timeJson.length; i++) {
		var kIndex = jsonobj.timeJson[i].index;
		var kName = jsonobj.timeJson[i].name;
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell mui-media distanceCell';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<div class="item" onclick="timeOnClick(' + kIndex + ')"><a href="#" >' + kName + '</a></div>';
		//下拉刷新，新纪录插到最前面；
		timeTable.appendChild(li);
	}
	//-------------------------------------------------------
	for (var i = 0; i < jsonobj.settlementJson.length; i++) {
		var kIndex = jsonobj.settlementJson[i].index;
		var kName = jsonobj.settlementJson[i].name;
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell mui-media distanceCell';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<div class="item" onclick="settlementOnClick(' + kIndex + ')"><a href="#" >' + kName + '</a></div>';
		//下拉刷新，新纪录插到最前面；
		settlementTable.appendChild(li);
	}
	//-------------------------------------------------------
	mui.getJSON('data/home.json', null, function(data) {
		var jsonobj = eval(data);
		//				console.log(jsonobj.distanceJson[0].name);
		console.log(jsonobj.jobListJson.length);
		var table = document.body.querySelector('.jobTable');
		var cells = document.body.querySelectorAll('.jobCell');
		for (var i = 0; i < jsonobj.jobListJson.length; i++) {
			var kTitle = jsonobj.jobListJson[i].title;
			var kDes = jsonobj.jobListJson[i].des;
			var kMoney = jsonobj.jobListJson[i].money;
			var kTime = jsonobj.jobListJson[i].time;
			var kIndex = jsonobj.jobListJson[i].index;
			//-----------------------------------------------------------------
			var li = document.createElement('li');
			li.className = 'mui-table-view-cell mui-media jobCell';
			li.innerHTML = '<div  onclick="itemOnClick(' + kIndex + ')"><a>' + //
				'<div class="mui-media-object mui-pull-left tableIcon"><span>测试</span></div>' + //
				'<div class="mui-media-body">' + kTitle + //
				'<p class=\'mui-ellipsis\'>' + kDes + '</p>' + //
				'<p class=\'mui-ellipsis\'> <span class="salary">' + kMoney + //
				'<span class="release-time"">' + kTime + '</span></p>' + //
				'</div></a></div>';
			table.appendChild(li);
		}
	});
});

angular.module("commApp", []).controller("homeController", function($scope) {
	$scope.addressTxt = {
		title: '武汉'
	};
	$scope.distanceTxt = {
		title: '距离'
	};
	$scope.categoryTxt = {
		title: '类别'
	};
	$scope.timeTxt = {
		title: '档期'
	};
	$scope.settlementTxt = {
		title: '结算'
	};
	console.log($scope.distanceTxt.title);
});

//----------------------------------------------------------------------------
function distanceOnClick(index) {
	//					alert(index);
	var name = jsonobj.distanceJson[index].name;
	var appElement = document.querySelector('[ng-controller=homeController]');
	var $scope = angular.element(appElement).scope();
	$scope.distanceTxt = {
		title: name
	};
	$scope.$apply();
	mui('#distancePopover').popover('hide');
}

function categoryOnClick(index) {
	var name = jsonobj.categoryJson[index].name;
	var appElement = document.querySelector('[ng-controller=homeController]');
	var $scope = angular.element(appElement).scope();
	$scope.categoryTxt = {
		title: name
	};
	$scope.$apply();
	mui('#categoryPopover').popover('hide');
}

function timeOnClick(index) {
	var name = jsonobj.timeJson[index].name;
	var appElement = document.querySelector('[ng-controller=homeController]');
	var $scope = angular.element(appElement).scope();
	$scope.timeTxt = {
		title: name
	};
	$scope.$apply();
	mui('#timePopover').popover('hide');
}

function settlementOnClick(index) {
	var name = jsonobj.settlementJson[index].name;
	var appElement = document.querySelector('[ng-controller=homeController]');
	var $scope = angular.element(appElement).scope();
	$scope.settlementTxt = {
		title: name
	};
	$scope.$apply();
	mui('#settlementPopover').popover('hide');
}

//----------------------------------------------------------------------------
$("document").ready(function() {
	var kArray = [
		'checkboxOneInput',
		'checkboxOneInput1',
		'checkboxOneInput2',
		'checkboxOneInput3',
		'checkboxOneInput4',
		'checkboxOneInput5'
	];
	for (var i = 0; i < kArray.length - 1; i++) {
		var name = kArray[i];
		$("#" + name).click(function() {
			var value = this.value;
			var isCheck = this.checked;
			checkboxListener(isCheck, value);
		})
	}
})

function checkboxListener(isCheck, value) {
	alert(value);
	switch (value) {
		case 1:
			break;
	}
	mui('#cityPopover').popover('hide');
}
//------------------------------------------------------------
//function itemOnClick(index) {
//	//	alert(index);
//	if (0 == index) {
//		window.location.href = "binding.html";
//	} else {
//		window.location.href = "job_detail.html";
//	}
//}
//------------------------------------------------------------
// dropload
var dropload = $('.inner').dropload({
	domUp: {
		domClass: 'dropload-up',
		domRefresh: '<div class="dropload-refresh">↓下拉刷新</div>',
		domUpdate: '<div class="dropload-update">↑释放更新</div>',
		domLoad: '<div class="dropload-load"><span class="loading"></span>加载中...</div>'
	},
	domDown: {
		domClass: 'dropload-down',
		domRefresh: '<div class="dropload-refresh">↑上拉加载更多</div>',
		domUpdate: '<div class="dropload-update">↓释放加载</div>',
		domLoad: '<div class="dropload-load"><span class="loading"></span>加载中...</div>'
	},
	loadUpFn: function(me) {
		var table = document.body.querySelector('.jobTable');
		table.innerHTML = '';
		pageIndex = 0;
		addJobCell(pageIndex);
		me.resetload();
	},
	loadDownFn: function(me) {
		pageIndex++;
		addJobCell(pageIndex);
		me.resetload();
	}
});
//-----------------------------------------------------------------------------------

function addJobCell(index) {
	//	var kJson = '{"distance": 0,"class": 0,"schedule": 0,"payMethod": 0,"keyword": "",' +
	//		'"lat": "47.605049","lng": "-122.336106","pageSize": 7,"openId": "sample string 8,"pageIndex": "' + index + '}';
	var kJson = '{"distance": 0,"class": 0,"schedule": 0,"payMethod": 0,"keyword": "", "lat": "47.605049","lng": "-122.336106","pageIndex": 1,"pageSize": 7,"openId": "sample string 8"}';

	postAction('getJobs', kJson);
}

function successCallBack(kJson) {
	var Data = kJson.Data.Data;
	var length = Data.length;
	var table = document.body.querySelector('.jobTable');
	var cells = document.body.querySelectorAll('.jobCell');
	var index = 0;
	for (var i = cells.length, len = i + length; i < len; i++) {
		var li = document.createElement('li');
		index = i - cells.length;
		li.className = 'mui-table-view-cell mui-media jobCell';
		li.innerHTML = '<div  onclick="itemOnClick(' + i + ')"><a>' + //
			'<div class="mui-media-object mui-pull-left tableIcon"><span>测试</span></div>' + //
			'<div class="mui-media-body">' + Data[index].JobTitle + //
			'<p class=\'mui-ellipsis\'>' + Data[index].jobDes + '</p>' + //
			'<p class=\'mui-ellipsis\'> <span class="salary">' + Data[index].salary + //
			'</span><span class="release-time">' + Data[index].releaseTime + //
			'</span> </p></div></a></div>';
		table.appendChild(li);
	}
}
//--------------------------------------------------------------------------
//var openId = $('#openId').val();
//if (openId == undefined || openId == '') {
//	window.location.href = "../OAuth2";
//} else {
//		alert(openId);
//}