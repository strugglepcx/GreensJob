/*
 * 拖拽与缩放
 * 需要'jquery'
 *
 * @author longweiguo
 * @version 20100428
 * @2011/10/17 modify for guazi
 */

GJ.add(
'js/util/dragdrop/dragdrop.js',
['jquery'],
function(){
     
     
    if (GJ.DragDrop) return; 
    
    GJ.DragDrop = function()
    {
         
        var countN=0;
        var dnr={}, $el, useProxy, proxyEl, onStop, onMove, pos={}, ret={}, doc=document, $doc=$(doc),

            getCss = function (name, def){
                return parseInt($el.css(name)) || def;
            },                                        

            drag = function (ev){
                
                if (!$el)return ;/////如果jq对象为空
                 
                ret.event = ev;
                
                if (useProxy && !proxyEl){
                    proxyEl = $('<div id="jqDnR_move" style="position:absolute; border:1px dotted #000;background:#ccc;opacity:0.5; filter:alpha(opacity=50);z-index:600000;"></div>');
                    $(doc.body).append(proxyEl);/////把proxyEl添加到body节点之下
                    
                    proxyEl.css({left:dnr.X, top:dnr.Y, width:dnr.W, height:dnr.H, marginLeft:dnr.mL, marginTop:dnr.mT});
                    
                }

                var sl = $doc.scrollLeft(), st = $doc.scrollTop();/////获取滚动条到左边距离和到上边距离

                
                if (Math.abs(ev.pageX - dnr.pX) > dnr.minMoveDistance || Math.abs(ev.pageY - dnr.pY) > dnr.minMoveDistance) {
                    
                    if (dnr.kind == 'drag') {
                        if (dnr.inViewPort) {/////设置是否允许源目标移出视图区域
                            
                            /////
                            var left = Math.max(dnr.minMoveDistance-dnr.mL+sl, dnr.X+ev.pageX-dnr.pX);
                            var top  = Math.max(dnr.minMoveDistance-dnr.mT+st, dnr.Y+ev.pageY-dnr.pY);
                            left = Math.min(left, dnr.vp.width-dnr.W-dnr.mL-dnr.minMoveDistance+sl);
                            top  = Math.min(top, dnr.vp.height-dnr.H-dnr.mT-dnr.minMoveDistance+st);
                        }
						else if(dnr.inViewElement) {/////如果设置了包含源目标的对象，则源目标只能在这个对象里面移动
                            
							var _$el = $(dnr.inViewElement),
								_off = _$el.position(),
								_w = _$el.width(),
								_h = _$el.height();
							var left = Math.max( _off.left , dnr.X+ev.pageX-dnr.pX);
							var top = Math.max( _off.top , dnr.Y+ev.pageY-dnr.pY);
							left = Math.min(left, _off.left + _w - dnr.W);
							top = Math.min(top, _off.top + _h - dnr.H);
						}
                        else {

                            var left = dnr.X+ev.pageX-dnr.pX;
                            var top  = dnr.Y+ev.pageY-dnr.pY;

                        }
                        pos = {left:left, top:top};
                    }
                    else {/////不是drag那么就是resize了
                                                
                        if (dnr.inViewPort) {
                            var width  = Math.max(ev.pageX-dnr.pX+dnr.W, 0);
                            var height = Math.max(ev.pageY-dnr.pY+dnr.H, 0);
                            width  = Math.min(width, dnr.vp.width+sl-dnr.mL-dnr.X-dnr.minMoveDistance);
                            height = Math.min(height, dnr.vp.height+st-dnr.mT-dnr.Y-dnr.minMoveDistance);
                        }
                        else if(dnr.inViewElement){/////在指定的元素内移动
                            var _$el=$(dnr.inViewElement);
                            var _off=_$el.position();                           
                            var offleft=dnr.X-_off.left;
                            var offtop=dnr.Y-_off.top;
                            var borderW=dnr.W-dnr.RW; ///
                            var borderH=dnr.H-dnr.RH;
                            var width  = Math.max(ev.pageX-dnr.pX+dnr.W, 0);
                            var height = Math.max(ev.pageY-dnr.pY+dnr.H, 0);
                            width  = Math.min(width, _$el.width()-offleft-borderW);
                            height = Math.min(height, _$el.height()-offtop-borderH);
                        }/////
                        else {
                            var width  = Math.max(ev.pageX-dnr.pX+dnr.W,0);
                            var height = Math.max(ev.pageY-dnr.pY+dnr.H,0);
                        }
                        pos = {width:width, height:height};
                    }
                    
                    if (proxyEl)proxyEl.css(pos);/////如果有代理对象，就设置代理对象的位置
                    else $el.css(pos); /////没有代理对象，设置源对象的位置

                    if (onMove) {
						var undef = undefined;
                        ret.left   = pos.left === undef? dnr.X : pos.left;
                        ret.top    = pos.top === undef? dnr.Y : pos.top;
                        ret.width  = pos.width === undef? dnr.W : pos.width;
                        ret.height = pos.height === undef? dnr.H : pos.height;
                        ret.moveWidth = Math.abs(ev.pageX - dnr.pX);
                        ret.moveHeight = Math.abs(ev.pageY - dnr.pY);
                        onMove(ret);
                    }
                }

                return false;
            },

            stop = function (ev){
                
                if (!$el)return ;
                ret.event = ev;
                if (onStop){

                    ret.left   = pos.left   || dnr.X;
                    ret.top    = pos.top    || dnr.Y;
                    ret.width  = pos.width  || dnr.RW;
                    ret.height = pos.height || dnr.RH;
                    ret.moveWidth = Math.abs(ev.pageX - dnr.pX);
                    ret.moveHeight = Math.abs(ev.pageY - dnr.pY);
                    onStop(ret);
                }
                else $el.css(pos);

                if (proxyEl) proxyEl.remove();

                pos = {};
                ret = {};
                dnr={};
                $el = null;
                useProxy = false;
                proxyEl = null;
                onStop = null;
                onMove = null;
                $(doc).unbind('mousemove',drag).unbind('mouseup',stop);
            },

            JqDnR = function($obj, o)
            {
                
                

                var $handle = o.handle ? $(typeof o.handle == 'string' ? '#'+o.handle : o.handle, $obj) : $obj;
                
                if (o.type == 'cancel'){  /////当调用GJ.DragDrop.cancel(),释放func()
                    
                    var _func = $obj.data("dragdrop_handler");/////jquery用data()来存储临时变量。

                    if (_func){
                        alert("取消绑定");
                        $handle.unbind('mousedown', _func);
                    }
                    return ;
                }
                
                var func = function (ev){
                    
                    ret = {
                        element : $obj,
                        handle  : $handle,
                        event   : ev
                    };
                    onStop = o.onStop || null;
                    onMove = o.onMove || null;
                    $el=$obj; /////全局变量存储当前操作对象

                    var p={};
                    if($el.css('position')=='absolute'){
                        try{ p = $el.position(); } catch(e){}
                        var mL = getCss('margin-left', 0);
                        var mT = getCss('margin-top', 0);
                    }
                    else {
                        p = {left:$el.offset().left,top:$el.offset().top};
                        var mL = 0;
                        var mT = 0;
                    }

                    dnr = {
                        X : p.left || getCss('left', 0),
                        Y : p.top  || getCss('top', 0),
                        W : $el.outerWidth(),  /////外边框的宽度(不包括margin)。
                        H : $el.outerHeight(),
                        RW: $el.width(),  /////$el的宽度(不包括边框)
                        RH: $el.height(), /////$el的宽度(不包括边框)
                        pX : ev.pageX,
                        pY : ev.pageY,
                        kind : o.type,
                        mL : mL,
                        mT : mT,
                        zIndex : getCss('z-index', 0),
                        vp : GJ.getViewPort(),  /////
                        inViewPort : o.inViewPort || false,
						inViewElement : o.inViewElement || null,
						minMoveDistance: o.minMoveDistance === undefined ? 5 : o.minMoveDistance
                    };
                 
                    useProxy = typeof o.useProxy === 'boolean' ? o.useProxy : true;

                    if (typeof o.onMouseDown == 'function') {
                        if (o.onMouseDown(ret) === false) return false;
                    }
                     
                     
                    $(doc).mousemove(drag).mouseup(stop);
                    return false;
                };
                 
                $obj.data('dragdrop_handler', func);/////jquery对象的data()
                $handle.bind('mousedown', func);
            };

        return {
            drag : function(obj, o){
                var o = o || {};
                o.type = 'drag';               
                JqDnR($(obj), o);  
            },
            resize : function(obj, o){
                var o = o || {};
                o.type = 'resize';
                JqDnR($(obj), o);
            },
            cancel : function(obj,o){
                var o = {};
                o.type = 'cancel';               
                JqDnR($(obj), {type : o});
                
            }
        }
    }();

    $.fn.GJ_drag=function (o){
        return this.each(function(){
            GJ.DragDrop.drag(this, o);
        });
    };

    $.fn.GJ_resize=function (o){
        return this.each(function(){
            GJ.DragDrop.resize(this, o);
        });
    };
   
});