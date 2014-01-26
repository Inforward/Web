(function (bespoke, window, doc) {

    bespoke.externalScripts = {        
        facebookSDK: {
            url: '//connect.facebook.net/en_US/all.js',
            id: 'facebook-jssdk'
        }
    };

    bespoke.scriptLoader = function (require) {
        
        function loadScript(script) {
            var id = script.id,
                url = script.url;

            loadScriptByUrl(url, id);
        }
        
        function loadScriptByUrl(url, id) {
            if (doc.getElementById(id)) return;

            var js = doc.createElement('script'),
                ref = doc.getElementsByTagName('script')[0];

            js.id = id;
            js.async = true;
            js.src = url;

            ref.parentNode.insertBefore(js, ref);
        }

        return {            
            loadScript: loadScript,
            loadScriptByUrl: loadScriptByUrl
        };
    };

}(window.bespoke = window.bespoke || {}, window, document));