angular.module('services', [])
    .service('currentVideoService', function() {
        var visible = false;
        var currentVideo = null;

        return {
            visible: function() { return visible; },
            currentVideo: function() { return currentVideo; },
            setCurrentVideo: function(video) {
                currentVideo = video;
                visible = true;
            },
            hide: function() { visible = false; }
        };
    })
    .service('scrolledIntoViewService', function() {
        var listeners = [];

        $(window).scroll(function() {
            determineInView();
        });

        var determineInView = function() {
            angular.forEach(listeners, function(listener) {
                $(listener.selector).each(function() {
                    var scrollTop = $(window).scrollTop();
                    var windowHeight = $(window).height();
                    var thisTop = $(this).offset().top;
                    var thisHeight = $(this).height();

                    if (thisTop + thisHeight >= scrollTop && thisTop <= scrollTop + windowHeight)
                        $(this).trigger("scrolledIntoView", []);
                });
            });
        };

        return {
            addListener: function(selector, callback) {
                listeners.push({ selector: selector, callback: callback });
                $(document).on("scrolledIntoView", selector, function(e) {
                    callback($(this));
                });
            },
            refresh: function() {
                determineInView();
            }
        };
    });

angular.module('controllers', [])
    .controller("ThumbnailsController", ['$scope', '$http', '$rootElement', '$timeout', 'currentVideoService', 'scrolledIntoViewService',
        function($scope, $http, $rootElement, $timeout, currentVideoService, scrolledIntoViewService) {
            $scope.videos = [];

            $http.get('/files/').success(function(data) {
                $scope.videos = data.files;
                $timeout(scrolledIntoViewService.refresh, 0);
            });

            $scope.setCurrentVideo = function(videoObject) {
                currentVideoService.setCurrentVideo(videoObject);
            };
        }])
    .controller("VideoController", ['$scope', '$rootElement', 'currentVideoService', function($scope, $rootElement, currentVideoService) {
        $scope.visible = false;
        $scope.currentVideo = null;

        $scope.close = function() {
            currentVideoService.hide();
        };

        $scope.$watch(currentVideoService.visible, function(newValue) {
            $scope.visible = newValue;
        });

        $scope.$watch(currentVideoService.currentVideo, function(newValue, oldValue) {
            if (newValue === null)
                return;

            $scope.currentVideo = newValue;
        });
    }]);

angular.module('directives', [])    
    .directive('video', function() {
        return {
            scope: false,
            restrict: 'C',
            template: '<div></div>',
            replace: true,
            link: function(scope, element, attrs) {
                scope.$watch(attrs.videoObject, function(video) {
                    if (video === null || video === undefined)
                        return;

                    var width = parseInt(attrs.width);
                    var height = parseInt(attrs.height);
                    var videoElement = $('<video width="' + width + '" height="' + height + '"></video>');
                    element.html($('<div>').append(videoElement));
                    var player = new MediaElementPlayer(videoElement, {
                        type: video.contentType,
                        defaultVideoWidth: width,
                        defaultVideoHeight: height,
                        pluginWidth: width,
                        pluginHeight: height,
                        features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],

                        success: function(mediaElement) {
                            var sources = [
                                { src: video.mediaUrl, type: video.contentType }
                            ];

                            mediaElement.setSrc(sources);
                            mediaElement.load();
                            mediaElement.play();
                        }
                    });
                    player.play();
                });
            }
        };
    })
    .directive('thumbnail', ['scrolledIntoViewService', function(scrolledIntoViewService) {
        scrolledIntoViewService.addListener('.thumbnail', function(thumbnail) {
            if (thumbnail.data('ready') && !thumbnail.data('loaded')) {
                thumbnail.data('loaded', true);
                thumbnail.addClass('loading');
                var img = $('<img src="' + thumbnail.data('thumbnail-url') + '" />');
                img.css({ position: 'absolute', left: '-1000px' });
                img.on("load", function() {
                    img.hide();
                    thumbnail.append(img);
                    img.css({ position: 'static', left: '0px' });
                    img.show();
                });
                $('body').append(img);
            }
        });

        return {
            scope: false,
            restrict: 'C',
            link: function(scope, element, attrs) {
                attrs.$observe('thumbnailUrl', function() {
                    element.data('ready', true);
                });
            }
        };
    }])    
    .directive('fluidGrid', ['scrolledIntoViewService',
        function(scrolledIntoViewService) {
            return {
                scope: false,
                restrict: 'C',
                link: function(scope, element, attrs) {
                    var CHILD_MAX_WIDTH = parseInt(attrs.childMaxWidth);
                    var CHILD_MAX_HEIGHT = parseInt(attrs.childMaxHeight);
                    var CHILD_MARGIN = parseInt(attrs.childMargin);
                    var CHILD_ASPECT_RATIO = CHILD_MAX_WIDTH / CHILD_MAX_HEIGHT;

                    scope.getWidth = function() {
                        return element.width();
                    };

                    scope.getChildCount = function() {
                        return element.children().length;
                    };

                    scope.$watch(scope.getWidth, function(width) {
                        resizeChildren(width);
                    });

                    scope.$watch(scope.getChildCount, function() {
                        resizeChildren(element.width());
                    });

                    var resizeChildren = function(elementWidth) {
                        var childWidth = calculateChildWidth(elementWidth);
                        var childHeight = calculateChildHeight(childWidth);

                        element.children()
                            .width(childWidth)
                            .height(childHeight);

                        scrolledIntoViewService.refresh();
                    };

                    window.onresize = function() {
                        scope.$apply();
                    };

                    var calculateChildWidth = function(parentWidth) {
                        var parentInnerWidth = parentWidth - CHILD_MARGIN;
                        var childrenPerRow = Math.ceil(parentInnerWidth / (CHILD_MAX_WIDTH + CHILD_MARGIN));
                        return (parentInnerWidth / childrenPerRow) - CHILD_MARGIN;
                    };

                    var calculateChildHeight = function(newWidth) {
                        return newWidth / CHILD_ASPECT_RATIO;
                    };
                }
            };
        }]);

angular.module('homePage', ['services', 'controllers', 'directives']);