
GJ.add(
'js/util/datepicker/datepicker.js',
['js/util/jquery/ui/i18n/jquery.ui.datepicker-zh-CN.js'],
function() {
	
	GJ.datepicker	= function(settings) {
		
		if (typeof settings != 'object')	return false;
		
		if (typeof settings.containerId == 'undefined')	return false;
		
	
		var params	= {
			'dateFormat'	: 'yy-mm-dd' 
		};
		
		if (settings.showButton === true) {
			params.showOn			= 'button';
			params.buttonImage		= '/images/vip/member_time.gif';
			params.buttonImageOnly	= true;
			params.buttonText		= '';
		}
		
		if (settings.buttonText) {
			params.buttonText	= settings.buttonText; 
		}
		
		if (settings.maxDate) {
			params.maxDate	= settings.maxDate; 
		}
		
		if (settings.minDate) {
			params.minDate	= settings.minDate; 
		}
		
		if (typeof settings.onShow == 'function') {
			params.beforeShow	= settings.onShow;
		}
		
		if (typeof settings.onSelect == 'function') {
			params.onSelect	= settings.onSelect;
		}
		
		$('#' + settings.containerId).datepicker(params);
				
	}
	
});