
(function (bespoke) {

    bespoke.login = function (require) {

        var $ = require('$'),
            config = require('config'),
            loginUrl = config.loginUrl,
            facebookAppId = config.fbAppId,
            facebookProvider = require('facebookProvider'),
            events = require('events'),
            pubsub = require('pubsub'),
            $modal = $("<div id='login-modal' class='reveal-modal' data-reveal />"),
            $slider;
        
        // Add modal to the DOM
        $modal.appendTo("body");

        function init(callback) {

            if ($modal.is(":empty")) {
                
                // Initialize providers
                facebookProvider.init(facebookAppId);
                
                // Load login html
                $.ajax({
                    url: loginUrl,
                    success: function (html) {
                        initModal(html);
                        callback();
                    }
                });
            } else {
                callback();
            }
        }
        
        function initModal(html) {
            $modal.html(html);

            $slider = $modal.find(".iosslider");
            
            // Init slider when opened
            $(document).on('opened', '#login-modal', function () {
                if ($slider.data("iosslider"))
                    $slider.iosSlider('destroy');
                
                initSlider();
            });
            
            // Initialize watermarks
            $modal.find("[data-placeholder]").each(function () {
                $(this).watermark($(this).data("placeholder"));
            });
            
            // Wire-up events
            $modal.find("#fb-login").on("click", facebookLogin);
            
            // Ajax-ify forms
            $modal.find("form").ajaxForm({
                dataType: 'json',
                beforeSubmit: function (arr, $form) {
                    $form.find(".validation-summary").text("");
                    return $form.valid();
                },
                success: function (response, statusText, xhr, $form) {
                    if (!response.Success) {
                        $form.find(".validation-summary").text(response.Message).show();
                        return;
                    }

                    pubsub.publish(events.login, response.User);
                    close();
                }
            });
           
            // Initialize form validators
            $.validator.unobtrusive.parse('#login-modal form');

            $modal.find("#forgot").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 1);
            });

            $modal.find("#go-to-login").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 2);
            });

            $modal.find("#email-signup").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 3);
            });

        }
        
        function initSlider() {
            $slider.iosSlider({
                startAtSlide: 2,
                snapToChildren: true,
                desktopClickDrag: false,
                infiniteSlider: false,
                snapSlideCenter: true,
                autoSlide: false,
                onSlideChange: function (args) {
                    var $form = $(args.targetSlideObject).find("form");
                    resetForm($form);
                }
            });
        }
        
        function facebookLogin() {
            facebookProvider.login(function (response) {
                if (response.Success) {
                    pubsub.publish(events.login, response.User);
                    close();
                }
            });
        }
        
        function reset() {
            $modal.find("form").each(function() {
                resetForm($(this));
            });
        }
        
        function resetForm($form) {
            $form.validate().resetForm();
            $form.find(".field-validation-error,.field-validation-valid,.validation-summary").text("");
            $form.find("input:text,input:password").val("");
        }
        
        function show() {
            init(function () {
                reset();
                $modal.foundation('reveal', 'open');
            });
        }
        
        function close() {
            $modal.foundation('reveal', 'close');
        }

        return {
            show: show
        };
    };

}(window.bespoke = window.bespoke || {}));