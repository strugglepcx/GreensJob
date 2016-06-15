function readHomeData() {

	var jsonobj;
	mui.getJSON('data/home.json', null, function(data) {
		jsonobj = eval(data);
		//				console.log(jsonobj.distanceJson[0].name);
		var distanceTable = document.body.querySelector('.distanceTable');
		var categoryTable = document.body.querySelector('.categoryTable');
		var timeTable = document.body.querySelector('.timeTable');
		var settlementTable = document.body.querySelector('.settlementTable');
		//				var cells = document.body.querySelectorAll('.distanceCell');
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
	});
	//---------------------------------------------------------------------------------------------
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
}