
(function (bespoke) {

    bespoke.dataManager = function (require) {

        var $ = require('$');

        var dataDefaults = {
            dataType: 'json',
            type: 'POST'
        };

        function sendRequest(options) {

            var that = mstats.dataManager,
                normalizedUrl = mstats.getRelativeEndpointUrl(options.url),
                callerOptions = $.extend({ cache: true }, dataDefaults, options, { url: normalizedUrl });

            if (callerOptions.cache && cachedData) {
                options.success(cachedData);
                return;
            }

            callerOptions.success = function (data) {
                if (callerOptions.cache) {
                    mstats.dataStore.set(normalizedUrl, data);
                }
                options.success(data);
            };

            $.ajax(callerOptions);
        }
        
        return {
            sendRequest: sendRequest
        };
    };

}(window.bespoke = window.bespoke || {}));
