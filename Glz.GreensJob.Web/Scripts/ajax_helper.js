GJ.add(
'js/app/pub/ajax_helper.js',
['jquery','js/util/string/string.js'],
function(){
	if (GJ.ajaxHelper) return;
	GJ.ajaxHelper = function(){
		var _postCache = {}, _postQueue = {};
		return {
			post : function( url, data, callback, type, o ){
					o = $.extend({
						life: 60 * 30 // 缓存时间 30 min
					}, o);
					if ( $.isFunction( data ) ) {
						type = callback;// fix the bug in $.post,没有参数data的情况
						callback = data;
						data = {};
					}
					var key  = GJ.format('{0},{1},{2}', url, type, $.param(data)),      
					    now = +new Date,	//事件戳
					    item = _postCache[key];
					if ( item && now-item.birthday<o.life*1000 ) {
						callback(item.data);
					} else {
						if (_postQueue[key] && _postQueue[key].length>0) {
							_postQueue[key].push(callback);
						} else {
							_postQueue[key] = [callback];
							$.post(url, data, function(data){
								_postCache[key] = {birthday:now, data:data};
								var queue = _postQueue[key], f;
								while( f=queue.shift() )
									f(data);
							}, type);							
						}
					}									
			}
		};
	}();	
});


