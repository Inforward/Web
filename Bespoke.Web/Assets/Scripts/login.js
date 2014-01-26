
(function (bespoke) {

    bespoke.login = function (require) {

        var $ = require('$'),
            facebookProvider = require('facebookProvider');

        function init(config) {

            // Initialize (load scripts, etc.)
            facebookProvider.init(config.fbAppId);
            
            // Wire-up events
            $("#fb-login").on("click", facebookLogin);
            
            $("#login-form").ajaxForm({
                dataType: 'json',
                success: function (response) {
                    alert(response.Success);
                }
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