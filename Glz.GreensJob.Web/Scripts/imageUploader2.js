// 应用发布页的图片上传组件
//
// DOM struct:
// <div class="ui-uploader">
//     <div class="ui-uploader-action">
//         <div class="ui-uploader-iframe-container">
//             {{iframe: uploader.html}}
//         </div>
//         <span class="ui-uploader-message"></span>
//     </div>
//     <ul class="ui-uploader-thumbImages">
//         {{缩略图 DOM struct}}
//     </ul>
// </div>

GJ.add(
    'tool/uploader2/imageUploader/imageUploader2.js', ['jquery', 'tool/uploader2/css/imageUploader.css'],
    function(require, exports, module) {
        var $ = require('jquery');
        var ImageUploader = {
            height: 24,
            width: 80,
            button_image_url: 'http://sta.ganji.com/src/image/icon/upload.png',
            enableCloudPhoto: false,
            init: function() {
                var config = this.config;
                this.config.containerEl = $(config.$btnContainer);
                this.$message = $(config.$messageContainer);
                this.$input = $(config.$input);
                this.$thumbImages = $(config.$thumbImagesContainer);

                if (!GJ.isFunction(this.config.setCoverFn)) {
                    this.config.setCoverFn = function() {
                        return true;
                    };
                }
                if (config.setCover) {
                    this.$thumbImages.addClass('ui-uploader-sortable');
                }

                this.setMessage = function(msg) {
                    this.$message.html(msg);
                };
                this.fileEls = {};
                this.setMessage('正在载入上传控件...');
                var t = this;
                var recommanded = false;
                this.on('Upload::error', function() {
                    if (recommanded || t.uploaderType === 'ajax' || t.uploaderType === 'html5') {
                        return;
                    } else {
                        recommanded = true;
                    }
                    var $switchLink = $('<a href="###" style="font-size: 12px">【切换到通用模式】</a>');
                    $switchLink.bind('click', function() {
                        t.config.uploaderTip = "";
                        t.forceAJAX();
                        $(this).remove();
                        return false;
                    });
                    t.$message.after($switchLink);
                });
                GJ.use('log_tracker', function() {
                    var timeoutFn;
                    t.on('Upload::start', function() {
                        clearTimeout(timeoutFn);
                        timeoutFn = setTimeout(function() {
                            GJ.LogTracker.trackEvent('uploader/click@atype=click');
                        }, 100);
                    });
                });

                if (config.enableCloudPhoto && $(config.$cloudPhotoBtn).size()) {
                    this.$cloudPhotoBtn = $(config.$cloudPhotoBtn);
                    this.cloudPhotoes = {};
                    this.cloudPhotoesCount = 0;
                    this.toDeleteCloudPhotoes = [];
                    this.on('Upload::delete', function(e, fileInfo) {
                        if (t.cloudPhotoes[fileInfo.id]) {
                            t.toDeleteCloudPhotoes.push(fileInfo);
                        }
                    });

                    var $cloudPhotoBtn = this.$cloudPhotoBtn;

                    GJ.use('js/util/modal/modal.js, js/util/modal/modal.css', function(Modal) {
                        var modal = new Modal({
                            $triggerEl: $cloudPhotoBtn,
                            title: '手机传图',
                            width: 770,
                            isScroll: false,
                            isClick: false
                        });

                        function showCloudPhotoCode(code) {
                            var content = '<div class="ui-uploader-cloud-photo">' +
                                        '<div class="ui-uploader-cloud-photo-checkcode">' + code + '</div>' +
                                        '<div class="ui-uploader-cloud-photo-QR-code">' +
                                        '<img src="http://www.ganji.com/ajax.php?module=qr_code&size=4&level=L&code=' + code + '" alt="' + code + '">' +
                                        '</div>' +
                                        '<div class="ui-uploader-cloud-photo-download-link">' +
                                        '<a href="http://mobile.ganji.com/ganji/iphone" target="_blank">下载赶集生活iPhone客户端</a>' +
                                        '或者<a href="http://mobile.ganji.com/ganji/android" target="_blank">Android客户端</a>（2.9.0以上版本），立即体验手机传图功能！' +
                                        '</div>' +
                                        '</div>';

                            modal.setContents(content);
                            modal.show();
                        }

                        $cloudPhotoBtn.click(function() {
                            if (t.cloudPhotoCode) {
                                showCloudPhotoCode(t.cloudPhotoCode);
                            } else {
                                var puid = $(this).data('puid');
                                var url = 'http://www.ganji.com/ajax.php?module=get_cloudy_code';
                                if (puid) {
                                    url += "&puid=" + puid;
                                }
                                $.ajax({
                                    url: url,
                                    type: 'GET',
                                    dataType: 'json',
                                    success: function(data) {
                                        t.cloudPhotoCode = data.code;
                                        showCloudPhotoCode(t.cloudPhotoCode);
                                        setTimeout(function() {
                                            poolingCloudPhoto.call(t);
                                        }, 3000);
                                    }
                                });
                            }
                            return false;
                        });
                    });
                }
            },
            onReady: function(type) {
                var t = this;
                if (t.config.uploaderTip) {
                    t.setMessage(t.config.uploaderTip);
                } else if (t.config.showMessage) {
                    var text = t.config.uploaderTip || "";
                    var sizeInfo = t.getFormatSize(t.postParams.MAX_FILE_SIZE);
                    if (text === "") {
                        if (t.config.maxNum > 1) {
                            text += "最多" + t.config.maxNum + "张，";
                            if (type === 'flash' || type === 'html5' && t.config.maxNum > 1) {
                                text += '可多选上传，';
                            }
                            if (t.config.postParams.uploadDir == 'wanted' || t.config.postParams.uploadDir == 'parttime_wanted') {
                                text += "最大" + sizeInfo[0] + sizeInfo[1] + "。";
                            } else {
                                text += "最大" + sizeInfo[0] + sizeInfo[1] + "。有图可使浏览量增加3倍，并会在推荐位显示。";
                            }

                        }
                    }
                    t.config.uploaderTip = text;
                    t.setMessage(text);
                } else {
                    t.setMessage('');
                }
            },
            onStart: function(fileInfo) {
                var t = this,
                    $file = thumbImage(this.config.postParams.thumbWidth, this.config.postParams.thumbHeight);
                $file.attr('id', t.id + "_" + fileInfo.id);
                this.fileEls[fileInfo.id] = $file;
                if (this.$thumbImages.find(".ui-uploader-iframe-container2").length > 0) {
                    this.$thumbImages.find(".ui-uploader-iframe-container2").before($file);
                } else {
                    this.$thumbImages.append($file);
                }

                this.$thumbImages.trigger('addImage', [$file, fileInfo.description]);
                $file.$status.text('正在等待');
                $file.addClass('ui-uploader-inProgress');
                if (t.config.hasDescription) {
                    var placeholder = t.config.descriptionPlaceholder;
                    $file.addClass('ui-uploader-hasDescription');
                    if (!$('<input>')[0].placeholder) {
                        $file.$description.text(placeholder).addClass('ui-uploader-placeholder');
                        $file.$description.css({
                            width: t.config.postParams.thumbWidth - 2
                        });
                        $file.$description.bind({
                            'focus': function() {
                                if ($(this).val() === placeholder) {
                                    $(this).removeClass('ui-uploader-placeholder').val("");
                                }
                            },
                            'blur': function() {
                                if ($(this).val() === "") {
                                    $(this).val(placeholder).addClass('ui-uploader-placeholder');
                                }
                            }
                        });
                    } else {
                        $file.$description.attr('placeholder', placeholder);
                    }
                    var preValue = "";
                    $file.$description.bind({
                        "focus": function() {
                            if ($(this).val() === placeholder) {
                                preValue = "";
                            } else {
                                preValue = $(this).val();
                            }
                        },
                        "blur": function() {
                            var val = $(this).val();
                            if (val === placeholder) {
                                val = "";
                            }

                            GJ.each(t.fileQueue.getUploadedFiles(), function(file) {
                                if (file.id === fileInfo.id) {
                                    file.description = val;
                                    t.trigger('Upload::change');
                                }
                            });

                        }
                    });
                }
                $file.$close.bind('click', function() {
                    t.cancelFile(fileInfo.id);
                    $file.remove();
                    var $firstFile = t.$thumbImages.find('li:nth-child(1)');
                    if (!$firstFile.hasClass('ui-uploader-inProgress')) {
                        $firstFile.find('.ui-uploader-status').text("封面").removeClass('ui-uploader-clickable');
                        if (t.config.setCover && t.config.setCoverFn()) {
                            $firstFile.find('.ui-uploader-bar').show();
                        } else {
                            $firstFile.find('.ui-uploader-bar').hide();
                        }
                    }
                    return false;
                });
                $file.$status.click(function(e) {
                    e.preventDefault();
                    var $status = $(this);
                    if ($status.hasClass('ui-uploader-clickable')) {
                        t.goFirst(fileInfo.id);
                        $status.text("封面").removeClass('ui-uploader-clickable');
                        var $firstFile = t.$thumbImages.find('li:nth-child(1)');
                        $firstFile.before($file);
                        if (!$firstFile.hasClass('ui-uploader-inProgress') && !$firstFile.hasClass('ui-uploader-hasError')) {
                            $firstFile.find('.ui-uploader-status').text('设为封面').addClass('ui-uploader-clickable');
                        }
                        return false;
                    }
                });
                $file.$goLeft.click(function(e) {
                    e.preventDefault();
                    if (t.$thumbImages.find('li').size() === 1) {
                        return;
                    }

                    t.goUp(fileInfo.id);
                    var index = $file.index();
                    var $another;
                    if (index === 0) { // the first one
                        $another = t.$thumbImages.find('li:nth-child(2)');
                        $file.$status.addClass('ui-uploader-clickable').text('设为封面');
                        $another.find('.ui-uploader-status').text('封面').removeClass('ui-uploader-clickable');
                        t.$thumbImages.append($file);
                    } else {
                        $another = t.$thumbImages.find('li:nth-child(' + (index) + ')'); // prev one
                        if (index === 1) { // to be the first one
                            $file.$status.text('封面').removeClass('ui-uploader-clickable');
                            $another.find('.ui-uploader-status')
                                .text('设为封面').addClass('ui-uploader-clickable');
                        }
                        $another.before($file);
                    }
                });
                $file.$goRight.click(function(e) {
                    e.preventDefault();
                    if (t.$thumbImages.find('li').size() === 1) {
                        return;
                    }

                    t.goDown(fileInfo.id);
                    var index = $file.index();
                    var $another;
                    if (index === t.$thumbImages.find('li').size() - 1) { // the last one
                        $another = t.$thumbImages.find('li:nth-child(1)');
                        $another.find('.ui-uploader-status').text('设为封面').addClass('ui-uploader-clickable');
                        $file.$status.text('封面').removeClass('ui-uploader-clickable');
                        $another.before($file);
                    } else {
                        $another = t.$thumbImages.find('li:nth-child(' + (index + 2) + ')'); // next one
                        if (index === 0) {
                            $file.$status.text('设为封面').addClass('ui-uploader-clickable');
                            $another.find('.ui-uploader-status').text('封面').removeClass('ui-uploader-clickable');
                        }
                        $another.after($file);
                    }
                });
            },
            onSuccess: function(fileInfo) {
                var t = this,
                    $file = this.fileEls[fileInfo.id];
                $file.addClass('success');

                if (fileInfo.thumbUrl) {
                    fileInfo.thumb_image = fileInfo.thumbUrl;
                }
                if (fileInfo.url) {
                    fileInfo.image = fileInfo.url;
                }
                $file.$close.unbind();
                $file.removeClass('ui-uploader-inProgress');
                $file.$progress.hide();

                var absoluteUrl = t.config.attachmentRootUrl + fileInfo.thumb_image;
                var thumbWidth = t.config.postParams.thumbWidth;
                var thumbHeight = t.config.postParams.thumbHeight;

                if (formatImgUrl(fileInfo.image).ext === '.swf') {
                    var flashId = $file.$flash.data('id');
                    GJ.use('flash', function() {
                        swfobject.embedSWF(absoluteUrl, flashId, thumbWidth, thumbHeight, "9.0.0", '', {
                            "loading": "加载中……"
                        }, {
                            "wmode": "transparent"
                        });
                        $file.$img.hide();
                    });

                } else {
                    $file.$img.one('error', function() {
                        setTimeout(function() {
                            $file.$img.attr('src', absoluteUrl);
                        }, 2000);
                    });

                    $file.$img.attr('src', absoluteUrl);
                    $file.$img.css({
                        width: thumbWidth,
                        height: thumbHeight
                    });
                    $file.$flash.hide();
                }

                $file.$status.text('上传成功');

                if ($file.index() === 0) {
                    $file.$status.text("封面").removeClass('ui-uploader-clickable');
                } else {
                    $file.$status.text("设为封面").addClass('ui-uploader-clickable');
                }
                if (t.config.setCover && t.config.setCoverFn()) {
                    $file.$bar.show();
                } else {
                    $file.$bar.hide();
                }
                if (t.config.hasDescription && fileInfo.description) {
                    $file.$description.val(fileInfo.description);
                }
                $file.$close.unbind().click(function() {
                    if ($file.index() === 0) {
                        var $next_file = t.$thumbImages.find('li:nth-child(2)');
                        if ($next_file.size() && !$next_file.hasClass('ui-uploader-hasError') && !$next_file.hasClass('ui-uploader-inProgress')) {
                            $next_file.find('.ui-uploader-status').text("封面").removeClass('ui-uploader-clickable');
                        }
                    }

                    t.deleteFile(fileInfo.id);
                    return false;
                });

                if (formatImgUrl(fileInfo.image).ext === '.gif') {
                    $file.$editBar.hide();
                }

                $file.$editBar
                    .one('click', '.ui-uploader-edit', function() {
                        $file.$editBar.addClass('loading');
                        GJ.use('tool/uploader2/image_croper.js', function(Croper) {
                            var url = getImgUrl(fileInfo.image);

                            var imgCroper = new Croper(url);
                            imgCroper.ready(function() {
                                $file.$editBar
                                    .removeClass('loading')
                                    .addClass('loaded')
                                    .on('click', '.ui-uploader-edit', function() {
                                        $file.$editBar.toggleClass('loaded');
                                    })
                                    .on('click', '.ui-uploader-rotate-left', function() {
                                        rotate(-90);
                                    })
                                    .on('click', '.ui-uploader-rotate-right', function() {
                                        rotate(90);
                                    });
                            });

                            function rotate(deg) {
                                $file.$editBar
                                    .removeClass('loaded')
                                    .addClass('loading');

                                imgCroper
                                    .rotate(deg)
                                    .upload('http://image.ganji.com/upload_2.php')
                                    .done(function(data) {
                                        data = GJ.jsonDecode(data)[0];
                                        var thumbURL = getImgUrl(data.url, t.config.postParams.thumbWidth, t.config.postParams.thumbHeight);
                                        var f = t.fileQueue.get(fileInfo.id);
                                        var img = new Image();
                                        img.onload = function() {
                                            $file.$editBar
                                                .removeClass('loading')
                                                .addClass('loaded');
                                        };
                                        $file.$img.attr('src', thumbURL);
                                        img.src = thumbURL;
                                        f.image = data.url;
                                        f.thumb_image = thumbURL;
                                        f.is_new = true;
                                        t.fileQueue.update(f);
                                        t.trigger('Upload::change');
                                    });
                            }

                            function getImgUrl(url, w, h) {
                                url = formatImgUrl(url);
                                url.host = url.host || 'http://image.ganji.com/';
                                url.width = w || 0;
                                url.height = h || 0;
                                url.quality = url.quality || 9;
                                url.version = url.version || 0;
                                if (w || h) {
                                    url.height += 'c';
                                }

                                return url.host + url.id + '_' + url.width + '-' + url.height + '_' + url.quality + '-' + url.version + url.ext;
                            }
                        });

                        return false;
                    });

                function formatImgUrl(url) {
                    var match = /^(http:\/\/.*\..*\.com\/)?(.*?)?(_(\d*)-(\d*)_([0-9])-([0-9]))?(\..*)$/.exec(url);
                    if (!match) {
                        return {};
                    }
                    return {
                        host: match[1],
                        id: match[2],
                        width: match[4],
                        height: match[5],
                        quality: match[6],
                        version: match[7],
                        ext: match[8]
                    };
                }
            },
            onCheckError: function(fileInfo, msg) {
                var t = this,
                    $file = thumbImage(t.config.postParams.thumbWidth, t.config.postParams.thumbHeight),
                    $link = $('<a href="###" title="' + msg + '" id="' + t.id + '_' + fileInfo.id + '-errorMsg">查看原因</a>'),
                    $tooltip = {};

                $file.attr('id', t.id + "_" + fileInfo.id);
                this.fileEls[fileInfo.id] = $file;

                $file.addClass('ui-uploader-hasError');
                $file.$status.text('上传失败');
                t.$thumbImages.append($file);
                $link.click(function() {
                    return false;
                }).css({
                    'line-height': (t.config.postParams.thumbHeight - 20) + "px"
                });
                $file.$error.append($link);

                GJ.use('js/util/tooltip/tooltip.js', function(ToolTip) {
                    $tooltip = new ToolTip({
                        $btn: $('#' + t.id + '_' + fileInfo.id + '-errorMsg'),
                        placement: 'bottom',
                        type: 'hover',
                        content: $link.attr('title')
                    });
                });

                function remove() {
                    if ($file.index() === 0) {
                        var $next_file = t.$thumbImages.find('li:nth-child(2)');
                        if ($next_file.size() && !$next_file.hasClass('ui-uploader-hasError') && !$next_file.hasClass('ui-uploader-inProgress')) {
                            $next_file.find('.ui-uploader-status').text("封面").removeClass('ui-uploader-clickable');
                        }
                    }
                    $file.remove();
                }
                var timer = setTimeout(function() {
                    remove();
                    if ($tooltip.hide) {
                        $tooltip.hide();
                    }
                }, 10000);
                $file.$close.bind('click', function() {
                    clearTimeout(timer);
                    remove();
                    if ($tooltip.hide) {
                        $tooltip.hide();
                    }
                    return false;
                });
                $file.find('.ui-uploader-sorter').hide();
            },
            onError: function(fileInfo, msg) {
                var t = this,
                    $file = this.fileEls[fileInfo.id],
                    $link = $('<a href="###" title="' + msg + '" id="' + t.id + '_' + fileInfo.id + '-errorMsg">查看原因</a>'),
                    $tooltip = {};

                $file.removeClass('ui-uploader-inProgress').addClass('ui-uploader-hasError');

                $link.click(function() {
                    return false;
                }).css({
                    'line-height': (t.config.postParams.thumbHeight - 20) + "px"
                });
                $file.$status.text('上传失败').show();
                $file.$error.append($link);

                function remove() {
                    if ($file.index() === 0) {
                        var $next_file = t.$thumbImages.find('li:nth-child(2)');
                        if ($next_file.size() && !$next_file.hasClass('ui-uploader-hasError') && !$next_file.hasClass('ui-uploader-inProgress')) {
                            $next_file.find('.ui-uploader-status').text("封面").removeClass('ui-uploader-clickable');
                        }
                    }
                    $file.remove();
                }
                var timer = setTimeout(function() {
                    if ($tooltip.hide) {
                        $tooltip.hide();
                    }
                    remove();
                }, 10000);
                $file.$close.unbind().bind('click', function() {
                    clearTimeout(timer);
                    if ($tooltip.hide) {
                        $tooltip.hide();
                    }
                    remove();
                    return false;
                });

                GJ.use('js/util/tooltip/tooltip.js', function(ToolTip) {
                    $tooltip = new ToolTip({
                        $btn: $('#' + t.id + '_' + fileInfo.id + '-errorMsg'),
                        placement: 'bottom',
                        type: 'hover',
                        content: $link.attr('title')
                    });
                });
            },
            onProgress: function(fileInfo, loaded, total) {
                var t = this;
                var $file = this.fileEls[fileInfo.id];
                $file.$progress.text(parseInt((loaded / total) * 100, 10) + "%");
                $file.$status.text("正在上传");
                if (this.uploaderType === 'ajax' && loaded === 80) {
                    var $msg = $('<span style="color: red">如果长时间没有反应，建议［<a href="###" style="color: blue">取消</a>］，然后选择更小的文件。</span>');
                    t.$message.html('').append($msg);
                    $msg.find('a').bind('click', function() {
                        t.cancelFile(fileInfo.id);
                        $msg.remove();
                        return false;
                    });
                }
            },
            onResize: function(fileInfo, loaded, total) {
                var $file = this.fileEls[fileInfo.id];
                $file.$progress.text(parseInt((loaded / total) * 100, 10) + "%");
                $file.$status.text("正在压缩");
            },
            onChange: function() {
                var value;
                if (this.config.hasDescription) {
                    value = this.toJSON(['image', 'thumb_image', 'row_id', 'width', 'height', 'id', 'is_new', 'description']);
                } else {
                    value = this.toJSON(['image', 'thumb_image', 'row_id', 'width', 'height', 'id', 'is_new']);
                }
                this.$input.val(value);
                this.$input.trigger('change');
                this.setMessage(this.getStatusMessage());
            },
            onDelete: function(fileInfo) {
                this.fileEls[fileInfo.id].remove();
            },
            onCancel: function(fileInfo) {
                this.fileEls[fileInfo.id].remove();
            }
        };

        function thumbImage(width, height, readonly) {
            var $this = $('<li></li>');
            if (readonly) {
                $this.addClass('readonly');
            }
            var randNum = new Date().getTime() + "" + GJ.rand(1, 10000);
            $this.$close = $('<a href="###" class="ui-uploader-close">&nbsp;&nbsp;&nbsp;</a>');
            $this.$content = $('<div class="ui-uploader-content"></div>');
            $this.$progress = $('<span class="ui-uploader-progress"></span>');
            $this.$error = $('<div class="ui-uploader-error"></div>');
            $this.$img = $('<img>');
            $this.$flash = $('<div id="flash-' + randNum + '" data-id="flash-' + randNum + '">');
            $this.$bar = $('<div style="width: ' + width + 'px" class="ui-uploader-bar">');
            $this.$status = $('<a href="###" class="ui-uploader-status">封面</a>');
            $this.$description = $('<textarea class="ui-uploader-description" placeholder=""></textarea>');
            $this.$goLeft = $('<a href="###" class="ui-uploader-sorter pull-left" data-direction="left">&nbsp;</a>');
            $this.$goRight = $('<a href="###" class="ui-uploader-sorter pull-right" data-direction="right">&nbsp;</a>');
            $this.$editBar = $('<div class="ui-uploader-edit-bar"></div>');
            $this.$editBar
                .append('<a href="javascript:;" class="ui-uploader-rotate-left">&nbsp;</a>')
                .append('<a href="javascript:;" class="ui-uploader-rotate-right">&nbsp;</a>')
                .append('<a href="javascript:;" class="ui-uploader-edit">&nbsp;</a>');

            $this.append($this.$content).append($this.$close).append($this.$description);
            $this.$content.append($this.$progress).append($this.$error).append($this.$img).append($this.$flash).append($this.$editBar).append($this.$bar);
            $this.$bar.append($this.$status);
            $this.$status.before($this.$goRight).before($this.$goLeft);
            $this.$content.css({
                width: width,
                height: height
            });
            $this.$progress.css('line-height', (height - 20) + "px");
            $this.$error.css({
                'text-align': 'center'
            });
            $this.$content.css('background-position', ((width - 33) / 2) + "px " + ((height - 20 - 33) / 2) + "px");

            return $this;
        }

        function poolingCloudPhoto() {
            var self = this;
            var data = {
                code: this.cloudPhotoCode
            };
            if (self.toDeleteCloudPhotoes.length) {
                GJ.each(self.toDeleteCloudPhotoes, function(photo) {
                    delete self.cloudPhotoes[photo.id];
                    self.cloudPhotoesCount--;
                });
                data.mobil_pic = GJ.jsonEncode(GJ.map(this.cloudPhotoes, function(photo) {
                    return photo.id;
                }));
                self.toDeleteCloudPhotoes = [];
            }
            data.mobil_count = this.cloudPhotoesCount;
            data.pc_count = this.fileQueue.getLength() - this.cloudPhotoesCount;

            $.ajax({
                url: 'http://www.ganji.com/ajax.php?module=get_mobile_pic_by_code',
                type: 'POST',
                data: data,
                dataType: 'json',
                success: function(cloudPhotoes) {
                    if (data.mobil_pic) {
                        // Note: 由于数据库主从延迟，因此删除图片时立马查询会导致数据不正确的情况
                        //       因此抛弃此次请求。
                        return setTimeout(function() {
                            poolingCloudPhoto.call(self);
                        }, 3000);
                    }
                    GJ.each(self.cloudPhotoes, function(photo) {
                        if ($.inArray(photo.id, cloudPhotoes) === -1) {
                            self.deleteFile(photo);
                            delete self.cloudPhotoes[photo];
                            self.cloudPhotoesCount--;
                        }
                    });
                    GJ.each(cloudPhotoes, function(photo) {
                        if (!self.cloudPhotoes[photo] && $.inArray(photo, self.toDeleteCloudPhotoes) === -1) {
                            var fileInfo = self.cloudPhotoes[photo] = {
                                id: photo,
                                url: photo,
                                thumbUrl: photo,
                                is_new: true
                            };
                            self.cloudPhotoesCount++;
                            self.trigger('Upload::start', [fileInfo]);
                            self.trigger('Upload::success', [fileInfo]);
                            self.trigger('Upload::complete', [fileInfo]);
                        }
                    });
                    setTimeout(function() {
                        poolingCloudPhoto.call(self);
                    }, 3000);
                }
            });
        }
        GJ.ImageUploader2 = ImageUploader;
        module.exports = ImageUploader;
    });