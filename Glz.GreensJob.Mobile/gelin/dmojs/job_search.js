var app = angular.module('jobSearchApp', ["ngCookies"]);
angular.module("jobSearchApp", []).controller("searchController", function($scope) {
	$scope.searchTitle0 = {
		title: '137000000'
	};
	$scope.searchTitle1 = {
		title: '137000000'
	};
	$scope.searchTitle2 = {
		title: '137000000'
	};
	$scope.searchTitle3 = {
		title: '137000000'
	};
	$scope.searchTitle4 = {
		title: '137000000'
	};
	$scope.searchTitle5 = {
		title: '137000000'
	};
	console.log($scope.searchTitle0.title);
	//				$scope.telModel;
});
//---------------------------------------------------------
mui.getJSON('data/search.json', null, function(data) {
	var jsonobj = eval(data);
	alert(jsonobj);
	var titleJson = jsonobj.titleJson;
	var appElement = document.querySelector('[ng-controller=searchController]');
	var $scope = angular.element(appElement).scope();
	$scope.searchTitle0 = {
		title: titleJson.title0
	};
	$scope.searchTitle1 = {
		title: titleJson.title1
	};
	$scope.searchTitle2 = {
		title: titleJson.title2
	};
	$scope.searchTitle3 = {
		title: titleJson.title3
	};
	$scope.searchTitle4 = {
		title: titleJson.title4
	};
	$scope.searchTitle5 = {
		title: titleJson.title5
	};
	$scope.$apply();
	jobInit(jsonobj.dataJson.scheduleJson, '.scheduleUl', 'checkboxOneInput0');
	jobInit(jsonobj.dataJson.typeJson, '.typeUl', 'checkboxOneInput1');
	jobInit(jsonobj.dataJson.payJson, '.payUl', 'checkboxOneInput2');
	jobInit(jsonobj.dataJson.settlementJson, '.settlementUl', 'checkboxOneInput3');
	jobInit(jsonobj.dataJson.distanceJson, '.distanceUl', 'checkboxOneInput4');
	jobInit(jsonobj.dataJson.workTimeJson, '.workTimeUl', 'checkboxOneInput5');
});

function jobInit(values, ulID, inputid) {
	var distanceTable = document.body.querySelector(ulID);
	for (var i = 0; i < values.length; i++) {
		var value = values[i].value;
		var inputID = inputid + i;
		var li = document.createElement('li');
		li.className = 'mui-table-view-cell mui-media';
		li.style = 'background: transparent;border: transparent;';
		//					li.onclick='distanceOnClick(0)';
		li.innerHTML = '<div class="checkbox-label">' + //
			'<div><input type="checkbox" value="1" id=' + inputID + ' />' +
			'<label for=' + inputID + '>' + value + '</label></div></div>';
		//下拉刷新，新纪录插到最前面；
		distanceTable.appendChild(li);
	}
}