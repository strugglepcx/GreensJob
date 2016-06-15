GJ.add('js/util/iframe/iframe.js', ['js/util/iframe/rpc3.js', 'jquery'], function(require, exports, module) {
    var IframeRPC = require('js/util/iframe/rpc3.js');
    var $ = require('jquery');
    var iframes = {};
    var createIframe = function(config) {
        var me = {};
        var $handler = $(me);
        me.on = function() {
            $handler.bind.apply($handler, Array.prototype.slice.call(arguments));
        }
        me.trigger = function() {
            $handler.trigger.apply($handler, Array.prototype.slice.call(arguments));
        }
        me.off = function() {
            $handler.unbind.apply($handler, Array.prototype.slice.call(arguments));
        }
        me.one = function() {
            $handler.one.apply($handler, Array.prototype.slice.call(arguments));
        }
        me.id = GJ.guid();

        // Default Config
        var defaultConfig = {
            containerId: '',
            url: '',
            scrolling: false,
            height: 0,
            onLoad: null,
            autoSetHeight: false,
            useBrowseCache: false,
            handlersForChild: {}, // 给子窗口调用的回调函数集
            paramsForChild: {}, // 传给子窗口的键值对，值不能是函数
            proxyUrl: 'http://sta.ganji.com/crossdomain.html'
        };
        GJ.mix(config, defaultConfig);

        // Container
        me.$container = typeof config.containerId === 'string' ?
            $("#" + config.containerId) :
            $(config.containerId);

        if (!me.$container.size()) {
            throw new Error('container not found');
        } else {
            me.container = me.$container[0];
        }
        if (config.autoSetHeight) {
            config.paramsForChild.autoSetHeight = true;
        }
        // config RPC
        var rpcConfig = {
            helper: config.proxyUrl,
            remote: config.url,
            container: me.container,
            isSameOrigin: false,
            props: {
                style: {
                    border: 0,
                    frameSpacing: 0,
                    frameBorder: 0,
                    padding: 0,
                    margin: 0,
                    width: config.width ? config.width + "px" : '100%',
                    height: config.height ? config.height + 'px' : '100%'
                },
                scrolling: config.scrolling ? 'yes' : 'no'
            },
            onReady: function() {
                iframes[me.id] = rpcConfig.iframe;
                me.childReady = me.loaded = true;
                me.isSameDomain = rpcConfig.isSameOrigin;
            },
            method: {
                getParams: function() {
                    return config.paramsForChild || {};
                },
                setParam: function(key, val) {
                    config.paramsForChild[key] = val;
                },
                setIframeHeight: function(height) {
                    me.$container.css('height', parseInt(height));
                    $(me.iframe).css('height', parseInt(height)).css('height', '100%');
                },
                redirect: function(url) {
                    me.iframe.src = url;
                },
                childReady: function() {
                    me.childReady = true;
                },
                parentReload: function() {
                    window.location.reload();
                },
                parentRedirect: function(url) {
                    window.location.href = url;
                }
            }
        }
        if (config.handlersForChild) {
            GJ.each(config.handlersForChild, function(fn, key) {
                rpcConfig.method[key] = fn;
            });
        }
        if (config.scrolling) {
            rpcConfig.props.style.overflowX = 'hidden';
        } else {
            rpcConfig.props.style.overflow = 'hidden';
        }
        me.destroy = me.close = function() {
            me.rpc.destroy();
        }
        me.setParamForChild = function(key, val) {
            config.paramsForChild[key] = val;
        }
        me.setHandlerForChild = function(funcName, func) {
            me.rpc.set(funcName, func);
        }
        me.callChildHandler = function(funcName) {
            var args = [].slice.call(arguments, 1);
            me.rpc(funcName, args);
        }
        me.setScroll = function(bool) {
            if (GJ.ua.chrome) return false;
            $(rpcConfig.iframe).attr("scrolling", bool ? "yes" : "no");
        }
        me.redirect = function(url) {
            me.iframe.src = url;
        }
        me.rpc = new IframeRPC(rpcConfig);
        me.iframe = me.rpc.iframe;
        $(me.iframe).bind('load', function() {
            if ($.isFunction(config.onLoad)) {
                config.onLoad();
            }
        });

        return me;
    }
    GJ.createIframe = createIframe;
    GJ.getIframe = function(id) {
        return iframes[id];
    }
});