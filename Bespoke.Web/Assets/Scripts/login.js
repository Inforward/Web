
(function (bespoke) {

    bespoke.login = function (require) {

        var $ = require('$'),
            config = require('config'),
            loginUrl = config.loginUrl,
            facebookAppId = config.fbAppId,
            facebookProvider = require('facebookProvider'),
            intialized = false,
            initializing = false,
            $modal = $("<div id='login-modal' class='reveal-modal' data-reveal />");
        
        var $slider,
            $forgotForm,
            $loginForm,
            $signupForm;
        
        // Add modal to the DOM
        $modal.appendTo("body");

        function init(callback) {
           
            // Initialize providers
            facebookProvider.init(facebookAppId);
            
            // Initialize modal
            if ($modal.is(":empty")) {
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
            $forgotForm = $modal.find("#forgot-form");
            $loginForm = $modal.find("#login-form");
            $signupForm = $modal.find("#signup-form");
            
            // Init slider when first opened
            $(document).on('opened', '#login-modal', function () {
                if ($slider.data("iosslider"))
                    $slider.iosSlider('destroy');
                
                initSlider();
                $loginForm.find("input:text").first().focus();
            });
            
            // Initialize watermarks
            $modal.find("[data-placeholder]").each(function () {
                $(this).watermark($(this).data("placeholder"));
            });
            
            // Wire-up events
            $modal.find("#fb-login").on("click", facebookLogin);

            // Ajax-ify forms
            $loginForm.ajaxForm({
                dataType: 'json',
                success: function (response) {
                    if (!response.Success) {
                        $loginForm.find(".validation-summary").text(response.Message).show();
                        return;
                    }
                }
            });

            $signupForm.ajaxForm({
                dataType: 'json',
                success: function (response) {
                    if (!response.Success) {
                        $signupForm.find(".validation-summary").text(response.Message).show();
                        return;
                    }
                }
            });

            $forgotForm.ajaxForm({
                dataType: 'json',
                success: function (response) {
                    if (!response.Success) {
                        $forgotForm.find(".validation-summary").text(response.Message).show();
                        return;
                    }
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
                },
                onSlideComplete: function (args) {
                    $(args.currentSlideObject).find("form input:text").first().focus();
                }
            });
        }
        
        function facebookLogin() {
            facebookProvider.login(function (response) {
                alert(response.Success);
            });
        }
        
        function reset() {
            $modal.find("form").each(function() {
                resetForm($(this));
            });
        }
        
        function resetForm($form) {
            $form.validate().resetForm();
            $form.find("input:text,input:password").val("");
            $form.find(".field-validation-error,.field-validation-valid").text("");
        }
        
        function show() {
            init(function () {
                reset();
                $modal.foundation('reveal', 'open');
            });
        }

        return {
            show: show
        };
    };

}(window.bespoke = window.bespoke || {}));