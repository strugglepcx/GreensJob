/**
 * 创建跨域上传组件
 *
 * @module uploader2
 * @file tool/uploader2/create_uploader.js
 * @author chan.jianbin@gmail.com
 * @version 2012-02-20
 */
GJ.add(
    'tool/uploader2/createUploader.js', ['jquery', 'js/util/event/event.js'],
    function(require, exports, module) {
        var EventEmitter = require('js/util/event/event.js');
        var $ = require('jquery');
        var Uploader = function(config) {
            var startTimestamp = +new Date();
            var defaultConfig = { // 上传控件的配置参数
                localHelper: 'http://sta.ganji.com/crossdomain.html',
                attachmentRootUrl: 'http://image.ganji.com/',
                uploadHost: 'http://image.ganji.com', // uploaderUrl 和 uploadUrl 所在的域
                uploaderUrl: '/uploader2.html?v=201207301726',
                uploadUrl: '/upload.php',

                containerEl: '',
                uploaderTip: '', // 默认提示
                showMessage: true, // 是否提示消息
                maxNum: 5, // 最多上传文件数
                type: 'gif,jpg,jpeg,png,bmp',

                height: 0,
                width: 0,
                button_image_url: '', // 按钮图片

                init: null, // 初始化函数

                onReady: null, // 上传控件加载成功  function(type)
                onProgress: null, // 正在上传中     function(fileInfo, loaded, total)
                onSuccess: null, // 上传成功      function(fileInfo, fileinfo)
                onError: null, // 上传出错      function(fileInfo, msg)
                onChange: null, // 添加或者删除文件时执行
                onStart: null, // 当有文件开始上传时   function(fileInfo)
                onResize: null, // 正在缩图时
                onDelete: null, // 当有文件被删除时  function(fileInfo)
                onCancel: null, // 当文件正上传时被取消 function(fileInfo)
                onCheckError: null, // 当文件不能通过检查时 function(fileInfo, msg);

                uploadedFiles: [], // 已上传的文件
                postParams: { // 随着文件上传时附带的数据
                    maxSize: 0, // 设为0则按控件默认行为控制文件大小限制{ swf: 10mb, html: 10mb, ajax: 2mb}
                    resizeImage: true,
                    resizeWidth: 600,
                    resizeHeight: 600,
                    resizeCutEnable: false,

                    createThumb: false,
                    thumbWidth: 80,
                    thumbHeight: 80,
                    thumbCutEnable: true,

                    minWidth: 0,
                    minHeight: 0
                }
            };
            var self = this;
            config = GJ.mix(defaultConfig, config, true, true);
            var postParams = this.postParams = config.postParams;
            this.config = config;
            config.uploadUrl = config.uploadHost + config.uploadUrl;
            if (GJ.getCookie('useAjaxUploader')) {
                config.uploadType = 'ajax';
            }

            EventEmitter(this);

            var uploaderConfig = {
                config: {
                    url: config.uploadUrl,
                    button_image_url: config.button_image_url,
                    type: config.type,
                    height: config.height,
                    width: config.width,

                    maxNum: config.maxNum,
                    maxSize: postParams.MAX_FILE_SIZE,
                    uploadedFileCount: config.uploadedFiles.length,
                    uploadedFiles: [],
                    uploadType: config.uploadType
                },
                postParams: postParams
            };
            GJ.each(config.uploadedFiles, function(f) {
                uploaderConfig.config.uploadedFiles.push({
                    id: f.id
                });
            });
            var STATUS = {
                REMOVED: -1,
                UPLOADING: 0,
                UPLOADED: 1
            };
            var fileQueue = (function() {
                var queue = [];
                var status = {};

                return {
                    push: function(file) {
                        queue.push(file);
                    },
                    remove: function(id) {
                        var temp = [],
                            file;
                        GJ.each(queue, function(v) {
                            if (v.id === id) {
                                file = v;
                            } else {
                                temp.push(v);
                            }
                        });
                        queue = temp;
                        if (file._is_new !== true && status[file.id] === STATUS.UPLOADED) {
                            self.deletedFiles.push(file);
                        }
                        status[id] = STATUS.REMOVED;
                        return file;
                    },
                    get: function(id) {
                        var ret;
                        GJ.each(queue, function(v) {
                            if (v.id === id) {
                                ret = v;
                                return false;
                            }
                        });
                        return ret;
                    },
                    getLength: function() {
                        return queue.length;
                    },
                    update: function(file) {
                        GJ.each(queue, function(v, i) {
                            if (v.id === file.id) {
                                queue[i] = file;
                            }
                        });
                    },
                    clear: function() {
                        var ret = queue;
                        queue = [];
                        return ret;
                    },
                    clone: function() {
                        var ret = [];
                        GJ.each(queue, function(file) {
                            ret.push(file);
                        });
                        return ret;
                    },
                    getUploadedFiles: function() {
                        var uploadedFiles = [];
                        GJ.each(queue, function(v, i) {
                            if (status[v.id] === STATUS.UPLOADED) {
                                uploadedFiles.push(v);
                            }
                        });
                        return uploadedFiles;
                    },
                    getUploadingFiles: function() {
                        var uploadingFiles = [];
                        GJ.each(queue, function(v, i) {
                            if (status[v.id] === STATUS.UPLOADING) {
                                uploadingFiles.push(v);
                            }
                        });
                        return uploadingFiles;
                    },
                    setUploading: function(id) {
                        GJ.each(queue, function(v) {
                            if (v.id === id) {
                                v.startTime = +new Date();
                            }
                        });
                        status[id] = STATUS.UPLOADING;
                    },
                    setUploaded: function(id) {
                        status[id] = STATUS.UPLOADED;
                    },
                    goFirst: function(id) {
                        var index = -1,
                            temp = [];
                        GJ.each(queue, function(v, i) {
                            if (v.id === id) {
                                index = i;
                            } else {
                                temp.push(v);
                            }
                        });
                        if (index === -1) {
                            return false;
                        }
                        if (index !== 0) {
                            temp.unshift(queue[index]);
                            queue = temp;
                        }
                    },
                    goUp: function(id) {
                        if (queue[0].id === id) {
                            queue.push(queue.shift());
                        } else {
                            GJ.each(queue, function(v, i) {
                                if (queue[i + 1] && queue[i + 1].id === id) {
                                    var temp = queue[i];
                                    queue[i] = queue[i + 1];
                                    queue[i + 1] = temp;
                                    return false;
                                }
                            });
                        }
                    },
                    goDown: function(id) {
                        if (queue[queue.length - 1].id === id) {
                            queue.unshift(queue.pop());
                        } else {
                            GJ.each(queue, function(v, i) {
                                if (queue[i].id === id) {
                                    var temp = queue[i];
                                    queue[i] = queue[i + 1];
                                    queue[i + 1] = temp;
                                    return false;
                                }
                            });
                        }
                    }
                };
            })();

            // for log
            var uaInfo = GJ.ua,
                uaInfoString = "";
            for (var i in uaInfo) {
                if (uaInfo.hasOwnProperty(i) && uaInfo[i]) {
                    uaInfoString += i + "_" + uaInfo[i] + "-";
                }
            }

            var userInfo = {
                flashSupport: GJ.checkFlashPlayer("10.0.0"),
                ua: uaInfoString,
                uploaderType: 'noLoaded',
                startTime: +new Date()
            };
            this.fileQueue = fileQueue;
            this.deletedFiles = [];
            var logTimer = setTimeout(function() {
                GJ.use('log_tracker', function() {
                    GJ.LogTracker.trackEvent("/uploader@atype=view" +
                        "@flashSupport=" + userInfo.flashSupport +
                        "@ua=" + userInfo.ua +
                        "@uploaderType=" + userInfo.uploaderType +
                        "@ok=0" +
                        "@message=LOAD_TIME_OUT"
                    );
                });
            }, 8000);

            var imgsServerBack = ['http://i1.ganji.com', 'http://i2.ganji.com', 'http://i3.ganji.com'];
            var backupIndex = 0;
            this.on({
                'Uploader::ready': function(e, type) {
                    clearTimeout(logTimer);
                    self.uploaderType = userInfo.uploaderType = type;
                    var usedTime = +new Date() - userInfo.startTime;
                    GJ.use('log_tracker', function() {
                        GJ.LogTracker.trackEvent("/uploader@atype=view" +
                            "@flashSupport=" + userInfo.flashSupport +
                            "@ua=" + userInfo.ua +
                            "@uploaderType=" + userInfo.uploaderType +
                            "@ok=1" +
                            "@usedTime=" + usedTime
                        );
                    });
                    if (!self.postParams.maxSize) {
                        if (type === 'flash') {
                            self.postParams.MAX_FILE_SIZE = self.postParams.maxSize = 1024 * 1024 * 10;
                        } else if (type === 'html5') {
                            self.postParams.MAX_FILE_SIZE = self.postParams.maxSize = 1024 * 10240;
                        } else {
                            self.postParams.MAX_FILE_SIZE = self.postParams.maxSize = 1024 * 1024 * 5;
                        }
                    } else {
                        self.postParams.MAX_FILE_SIZE = self.postParams.maxSize;
                    }
                    if (type === 'ajax') {
                        self.on({
                            "Upload::start": function() {
                                self.disable();
                            }
                        });
                    }

                    if (GJ.isFunction(config.onReady)) {
                        config.onReady.call(self, type);
                    }

                    //如果在初始化上传组件前有文件已经上传
                    if (self.config.uploadedFiles.length) {
                        var uploadedFiles = self.config.uploadedFiles;
                        GJ.each(uploadedFiles, function(fileInfo) {
                            self.trigger('Upload::start', [fileInfo]);
                            self.trigger('Upload::success', [fileInfo]);
                            self.trigger('Upload::complete', [fileInfo]);
                        });
                        self.trigger('Upload::change');
                    }
                },
                'Check::error': function(e, fileInfo, msg) {
                    if (GJ.isFunction(config.onCheckError)) {
                        config.onCheckError.call(self, fileInfo, msg);
                    }
                },
                'Upload::start': function(e, fileInfo, type) {
                    self.disable();
                    fileQueue.push(fileInfo);
                    fileQueue.setUploading(fileInfo.id);
                    if (GJ.isFunction(config.onStart)) {
                        config.onStart.call(self, fileInfo);
                    }

                    if (type === 'ajax') {
                        fakeOnProgress.call(self, fileInfo);
                    }
                    if (fileQueue.getLength() >= self.config.maxNum) {
                        self.disable();
                    } else {
                        self.enable();
                    }
                },
                'Upload::resize': function(e, fileInfo, loaded, total) {
                    if (GJ.isFunction(config.onResize)) {
                        config.onResize.call(self, fileInfo, loaded, total);
                    }
                },
                'Upload::progress': function(e, fileInfo, loaded, total) {
                    if (GJ.isFunction(config.onProgress)) {
                        config.onProgress.call(self, fileInfo, loaded, total);
                    }
                },
                'Upload::error': function(e, fileInfo, msg) {
                    var file = fileQueue.get(fileInfo.id) || {};
                    GJ.use('log_tracker', function() {
                        var httpRe = /HTTP (\d+)/;
                        var log = "/uploader@atype=view" +
                            "@flashSupport=" + userInfo.flashSupport +
                            "@ua=" + userInfo.ua +
                            "@uploaderType=" + userInfo.uploaderType +
                            "@ok=0" +
                            "@server=" + encodeURIComponent(self.config.uploadHost) +
                            "@message=" + msg +
                            "@startTime=" + file.startTime +
                            "@usedTime=" + (+new Date() - file.startTime) +
                            "@magicID=" + file.guid +
                            "@city=" + (GJ.getCookie('citydomain') || "unknown") +
                            "@status=" + (httpRe.test(msg) ? msg.match(httpRe)[1] : "-1");
                        GJ.LogTracker.trackEvent(log);
                    });
                    fileQueue.remove(fileInfo.id);
                    if (GJ.isFunction(config.onError)) {
                        config.onError.call(self, fileInfo, msg);
                    }

                    $(self.config.containerEl).children().remove();
                    // 发生错误时尝试使用备份机器
                    var bakServer = imgsServerBack[backupIndex];
                    self.config.uploadHost = bakServer;
                    self.config.attachmentRootUrl = bakServer + '/';
                    self.config.uploadUrl = self.config.uploadHost + '/upload.php';
                    uploaderConfig.config.url = self.config.uploadUrl;
                    checkType();
                    backupIndex++;
                    if(backupIndex > imgsServerBack.length - 1) {
                        backupIndex = 0;
                    }
                },
                'Upload::cancel': function(e, fileInfo) {
                    if (GJ.isFunction(config.onCancel)) {
                        config.onCancel.call(self, fileInfo);
                    }
                },
                'Upload::success': function(e, fileInfo) {
                    fileQueue.update(fileInfo);
                    fileQueue.setUploaded(fileInfo.id);

                    if (fileInfo.image_info && !fileInfo.width && !fileInfo.height) {
                        fileInfo.width = fileInfo.image_info[0];
                        fileInfo.height = fileInfo.image_info[1];
                    }

                    if (GJ.isFunction(config.onSuccess)) {
                        config.onSuccess.call(self, fileInfo);
                    }
                },
                'Upload::complete': function(e, fileInfo) {
                    if (fileQueue.getLength() >= self.config.maxNum) {
                        self.disable();
                    } else {
                        self.enable();
                    }
                    self.trigger('Upload::change', fileInfo);
                },
                'Upload::delete': function(e, fileInfo) {
                    if (GJ.isFunction(config.onDelete)) {
                        config.onDelete.call(self, fileInfo);
                    }
                    if (fileQueue.getLength() >= self.config.maxNum) {
                        self.disable();
                    } else {
                        self.enable();
                    }
                    self.trigger('Upload::change', fileInfo);
                },
                'Upload::change': function(e, fileInfo) {
                    if (GJ.isFunction(config.onChange)) {
                        config.onChange.call(self, fileInfo);
                    }
                }
            });
            // 调用外部的init函数
            if (GJ.isFunction(config.init)) {
                config.init.call(this);
            }

            var uploadType = 'ajax';
            if (GJ.checkFlashPlayer('10.0.0') && GJ.ua.os === 'windows') {
                uploadType = 'flash';
            } else if (typeof FileList !== 'undefined' && typeof FormData !== 'undefined') {
                uploadType = 'html5';
            }
            if (config.uploadType && {
                flash: 1,
                html5: 1,
                ajax: 1
            }.hasOwnProperty(config.uploadType)) {
                uploadType = config.uploadType;
            }

            //判断sta.ganji.com是否可用
            var domainArr = ['sta', 's1', 's2', 's3'];
            var domain;
            var defer = $.Deferred();
            var timer = setTimeout(function() {
                defer.reject();
            }, 2000);

            for(var i=0; i<domainArr.length; i++){
                var img = new Image();
                img.onload = function() {
                    img.onload = null;
                    if(defer.state() !== 'resolved'){
                        clearTimeout(timer);
                        domain = this.src.replace('http://','').replace('/sysmonitor.gif','');
                        defer.resolve(domain);
                    }
                };
                img.src = 'http://' + domainArr[i] + '.ganji.com/sysmonitor.gif';
            }

            defer.fail(function() {
                config.localHelper = 'http://sta1.ganji.com/crossdomain.html';
            }).done(function(url){
                config.localHelper = 'http://'+ url +'/crossdomain.html';
            }).always(function(){
                uploaderConfig.config.checkedDomain = domain;
                checkType();
            })

            function checkType(){
                if (uploadType === 'flash') {
                    GJ.use('tool/uploader2/swfuploader.js', function(SWFUploader) {
                        var config = uploaderConfig.config;
                        var postParams = uploaderConfig.postParams;
                        config.containerEl = $(self.config.containerEl)[0];
                        self.uploader = new SWFUploader(config, postParams);
                        // 避免config在jsonEncode时会引起的错误
                        delete config.containerEl;
                        var timeoutTimer = setTimeout(function() {
                            try {
                                self.uploader.unbind();
                                $(self.config.containerEl).children().remove();
                            } catch (ex) {} // 当SwfUploader未初始化完成时可能触发这个问题;
                            if (self.uploader.destory) {
                                self.uploader.destory();
                            }
                            self.uploader = createRpcUploader();
                        }, 8000);
                        self.uploader.on({
                            'Flash::ready': function() {
                                clearTimeout(timeoutTimer);
                                self.trigger('Uploader::ready', 'flash');
                            },
                            'Check::error': function(e, fileInfo, msg) {
                                self.trigger('Check::error', [fileInfo, msg]);
                            },
                            'Upload::progress': function(e, fileInfo, loaded, total) {
                                self.trigger('Upload::progress', [fileInfo, loaded, total]);
                            },
                            'Upload::resize_progress': function(e, fileInfo, loaded, total) {
                                self.trigger('Upload::resize', [fileInfo, loaded, total]);
                            },
                            'Upload::success': function(e, fileInfo) {
                                fileInfo.is_new = true;
                                fileInfo._is_new = true;
                                self.trigger('Upload::success', [fileInfo]);
                            },
                            'Upload::error': function(e, fileInfo, message) {
                                self.trigger('Upload::error', [fileInfo, message]);
                            },
                            'Upload::start': function(e, fileInfo, type) {
                                self.trigger('Upload::start', [fileInfo, type]);
                            },
                            'Upload::cancel': function(e, fileInfo) {
                                self.trigger('Upload::cancel', [fileInfo]);
                            },
                            'Upload::complete': function(e, fileInfo) {
                                self.trigger('Upload::complete', [fileInfo]);
                            }
                        });
                    });
                } else {
                    self.uploader = createRpcUploader();
                }
            }

            this.forceAJAX = function() {
                $(this.uploader).unbind();
                if (this.uploader.destory) {
                   this.uploader.destory();
                }
                this.uploader = createRpcUploader();
            };

            function createRpcUploader() {
                var remoteReady = false;
                var rpc = function() {};
                GJ.use('js/util/iframe/rpc3.js', function(RPC) {
                    rpc = new RPC({
                        remote: config.uploadHost + config.uploaderUrl,
                        container: $(config.containerEl)[0],
                        props: {
                            style: {
                                border: "0",
                                height: config.height + 'px',
                                width: config.width + 'px',
                                overflow: 'hidden',
                                padding: '0',
                                margin: '0'
                            },
                            scrolling: 'no'
                        },
                        method: {
                            //由子窗口调用，通知子窗口已加载完成
                            onRemoteReady: function() {
                                if (self.remoteReady) return;
                                self.remoteReady = true;
                                self.trigger('Remote::load');
                            },
                            onUploaderReady: function(type) {
                                this.uploaderType = type;
                                self.trigger('Uploader::ready', type);
                            },
                            //由子窗口调用，获取配置参数
                            getParam: function() {
                                return uploaderConfig;
                            },
                            onCheck: function(fileInfo, type, msg) {
                                if (type === 'error') {
                                    self.trigger('Check::error', [fileInfo, msg]);
                                } else {
                                    self.trigger('Check::success', [fileInfo]);
                                }
                            },
                            onStart: function(fileInfo, uploadType) {
                                self.trigger('Upload::start', [fileInfo, uploadType]);
                            },
                            onResize: function(fileInfo, loaded, total) {
                                self.trigger('Upload::resize', [fileInfo, loaded, total]);
                            },
                            onProgress: function(fileInfo, loaded, total) {
                                self.trigger('Upload::progress', [fileInfo, loaded, total]);
                            },
                            onError: function(fileInfo, msg) {
                                self.trigger('Upload::error', [fileInfo, msg]);
                            },
                            onCancel: function(fileInfo) {
                                self.trigger('Upload::cancel', [fileInfo]);
                            },
                            onSuccess: function(fileInfo) {
                                fileInfo.is_new = true;
                                fileInfo._is_new = true;
                                if (self.fileQueue.getUploadedFiles().length < self.config.maxNum) {
                                    self.trigger('Upload::success', [fileInfo]);
                                }
                            },
                            onComplete: function(fileInfo) {
                                self.trigger('Upload::complete', [fileInfo]);
                            }
                        },
                        serializer: {
                            parse: GJ.jsonDecode,
                            stringify: GJ.jsonEncode
                        }
                    });
                });

                return {
                    disable: function() {
                        rpc('disable');
                    },
                    enable: function() {
                        rpc('enable');
                    },
                    cancel: function(id) {
                        rpc('cancel', [id]);
                    },
                    deleteFile: function(id) {
                        rpc('deleteFile', [id]);
                    }
                };
            }
        };
        Uploader.prototype = {
            constructor: Uploader,
            cancelFile: function(id) {
                this.fileQueue.remove(id);
                this.uploader.cancel(id);
            },
            disable: function() {
                this.uploader.disable();
            },
            enable: function() {
                this.uploader.enable();
            },
            deleteFile: function(id) {
                var temp = [],
                    file;
                file = this.fileQueue.remove(id);

                if (!file) {
                    throw new Error('file not found');
                }

                if (this.fileQueue.getLength() < this.config.maxNum) {
                    this.enable();
                }
                this.uploader.deleteFile(id);

                this.trigger('Upload::delete', [file]);

                return file;
            },
            getStatusMessage: function() {
                var sizeInfo = this.getFormatSize(this.postParams.MAX_FILE_SIZE);
                var uploadedFiles = this.fileQueue.getUploadedFiles();
                if (this.config.maxNum === 1 && uploadedFiles.length !== 1) {
                    return "请选择大小不超过<span style='color: red'> " + sizeInfo[0] + " </span>" + sizeInfo[1] + "的文件上传。";
                }

                var html = "";
                if (uploadedFiles.length === 0) {
                    if (this.config.uploaderTip) {
                        html += this.config.uploaderTip;
                    }
                } else if (uploadedFiles.length >= this.config.maxNum) {
                    if (this.config.maxNum === 1) {
                        html += "若要重新上传，请删掉当前文件";
                    } else {
                        html += "您已达到数量上限，若要继续上传，请删掉一些现有文件。";
                    }
                } else {
                    html += "您已上传<span style='color:red'> " + uploadedFiles.length + " </span>个文件，";
                    html += "还能上传<span style='color:red'> " + (this.config.maxNum - uploadedFiles.length) + " </span>个文件。 ";
                }
                return html;
            },
            getFormatSize: function(size) {
                var unit = "B",
                    count = size.toFixed(2);
                if (size >= 1024 * 1024) {
                    unit = "MB";
                    count = (count / (1024 * 1024)).toFixed(2);
                } else if (size >= 1024) {
                    unit = "KB";
                    count = (count / 1024).toFixed(2);
                }
                if (count.substr(count.length - 3, count.length) === ".00") {
                    count = count.substr(0, count.length - 3);
                }

                return [count, unit];
            },
            toJSON: function(keys) { // keys 指定输出的字段
                var uploadedFiles = [],
                    deletedFiles = [];
                GJ.each(this.fileQueue.getUploadedFiles(), function(file) {
                    var tmp = {};
                    if (keys) {
                        GJ.each(keys, function(k) {
                            if (file.hasOwnProperty(k)) {
                                tmp[k] = file[k];
                            }
                        });
                    } else {
                        tmp = file;
                    }
                    uploadedFiles.push(tmp);
                });

                GJ.each(this.deletedFiles, function(file) {
                    var tmp = {};
                    if (keys) {
                        GJ.each(keys, function(k) {
                            if (file.hasOwnProperty(k)) {
                                tmp[k] = file[k];
                            }
                        });
                    } else {
                        tmp = file;
                    }
                    deletedFiles.push(tmp);
                });
                return GJ.jsonEncode([uploadedFiles, deletedFiles]);
            },
            goFirst: function(id) {
                this.fileQueue.goFirst(id);
                this.trigger('Upload::go-first', [id]);
                this.trigger('Upload::change');
            },
            goUp: function(id) {
                this.fileQueue.goUp(id);
                this.trigger('Upload::go-up', [id]);
                this.trigger('Upload::change');
            },
            goDown: function(id) {
                this.fileQueue.goDown(id);
                this.trigger('Upload::go-down', [id]);
                this.trigger('Upload::change');
            }
        };

        function fakeOnProgress(fileInfo) {
            var self = this;
            var percent = 0,
                tap = 0,
                timer;
            timer = setInterval(function() {
                tap++;
                percent = 100 - 1 / (0.001 * tap + 0.01);
                self.trigger('Upload::progress', [fileInfo, percent, 100]);
            }, 300);
            this.on('Upload::complete', function(e, f) {
                if (fileInfo.id === f.id) {
                    clearInterval(timer);
                }
            });
        }
        GJ.createUploader = function(config) {
            return new Uploader(config);
        };
        module.exports = Uploader;
    });