
(function (bespoke) {

    bespoke.login = function (require) {

        var $ = require('$'),
            facebookProvider = require('facebookProvider');

        function init(config) {
            
            var $slider = $(".iosslider"),
                $forgotForm = $("#forgot-form"),
                $loginForm = $("#login-form"),
                $signupForm = $("#signup-form");

            // Set proper focus (setTimeout to avoid automatic window scroll)
            window.setTimeout(function() {
                $loginForm.find("input:text").first().focus();
            }, 100);
            
            
            // Initialize our content slider
            $slider.iosSlider({
                startAtSlide: 2,
                snapToChildren: true,
                desktopClickDrag: false,
                infiniteSlider: false,
                snapSlideCenter: true,
                autoSlide: false,
                onSlideChange: function (args) {
                    var $form = $(args.targetSlideObject).find("form");

                    $form.validate().resetForm();
                    $form.find("input:text,input:password").val("");
                    $form.find(".field-validation-error,.field-validation-valid").text("");

                },
                onSlideComplete: function(args) {
                    $(args.currentSlideObject).find("form input:text").first().focus();
                }
            });
            
            // Initialize providers
            facebookProvider.init(config.fbAppId);
            
            // Wire-up events
            $("#fb-login").on("click", facebookLogin);
            
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
            
            $("#forgot").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 1);
            });
            
            $("#go-to-login").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 2);
            });
            
            $("#email-signup").on("click", function (e) {
                e.preventDefault();
                $slider.iosSlider('goToSlide', 3);
            });
        }
        
        function facebookLogin() {
            facebookProvider.login(function (response) {
                alert(response.Success);
            });
        }

        return {
            init: init
        };
    };

}(window.bespoke = window.bespoke || {}));