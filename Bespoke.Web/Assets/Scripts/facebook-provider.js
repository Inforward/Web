
(function (bespoke, window) {

    bespoke.facebookProvider = function (require) {

        var externalScripts = require('externalScripts'),
            scriptLoader = require('scriptLoader'),
            loginStatusResponse,
            fbAppId;

        // FB data elements we're requesting
        var permissions = {
            scope: 'email,user_friends'
        };

        function init(appId) {

            if (appId == null || !appId.length)
                throw new Error("Invalid Facebook App ID");

            fbAppId = appId;

            // Wire-up init (called from SDK after load)
            window.fbAsyncInit = initializeFacebook;

            // Load SDK script
            scriptLoader.loadScript(externalScripts.facebookSDK);
        }
        
        function initializeFacebook() {
            
            FB.init({
                appId: fbAppId,
                status: false,
                cookie: true,
                xfbml: false
            });

            FB.getLoginStatus(function (response) {
                loginStatusResponse = response;
            });
        }

        function login(callback) {

            if (loginStatusResponse == null)
                return;
            
            if (loginStatusResponse.authResponse) {
                authorize(loginStatusResponse.authResponse.accessToken, callback);
            } else {
                FB.login(function (loginResponse) {
                    if (loginResponse.authResponse) {
                        authorize(loginResponse.authResponse.accessToken, callback);
                    }
                }, permissions);
            }
        }
        
        function authorize(accessToken, callback) {
            $.post('/Account/FacebookLogin', { 'accessToken': accessToken }, function (data) {
                if (callback)
                    callback(data);
            });
        }

        return {
            init: init,
            login: login
        };

    };

}(window.bespoke = window.bespoke || {}, window));