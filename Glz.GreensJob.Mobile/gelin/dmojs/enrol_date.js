// Creating some 'sample' dates 
var datesArray = [];
var d = new Date();
for (i = 2; i < 7; i++) {
	var tempDay = new Date();
	tempDay.setHours(0, 0, 0, 0);
	tempDay.setDate(d.getDate() + i);
	datesArray.push(tempDay.getTime());
}

$(function() {
	$('.multiple').pickmeup({
		flat: true,
		mode: 'multiple',
		hide_on_select: false,
		// Before rendering each date, deselect dates if in the array
		render: function(date) {
			if ($.inArray(date.getTime(), datesArray) > -1) {
				return {
					//					disabled: false,
					//					class_name: 'pmu-disabled'

					//--------------------------------------------------------------------
					//					selected: true
					//					,
					//					class_name: 'pmu-selected'

				}
			}
		}
	});
});
// Little hack to deselect current day: PickMeUp forces you to have a default date :(
//$('.pmu-today').click();