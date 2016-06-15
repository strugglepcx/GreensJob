

angular.module("commApp", []).controller("homeController", function($scope) {

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

//----------------------------------------------------------
//function itemOnClick(index) {
////	alert(index);
//	if (0 == index) {
//		window.location.href = "binding.html";
//	} else {
//		window.location.href = "job_detail.html";
//	}
//}