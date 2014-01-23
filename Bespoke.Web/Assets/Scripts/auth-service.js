
(function (bespoke) {

    bespoke.authService = function (require) {
        
        var pubsub = require('pubsub'),
            events = require('events');

        function facebookLogin() {
            
        }
        
        function init() {
            // TODO:  Load scripts (create scriptLoader module -> scriptLoader.load(scripts.facebookSDK);
        }
        
        function getCurrentUser() {
            
        }

        return {            
            init: init,
            facebookLogin: facebookLogin,
            currentUser: getCurrentUser
        };

    };

}(window.bespoke = window.bespoke || {}));