
(function (bespoke) {

    bespoke.pubsub = function(require) {

        var topics = {},
            subUid = -1;
        
        function subscribe(topic, func) {
            if (!topics[topic]) {
                topics[topic] = [];
            }
            var token = (++subUid).toString();
            topics[topic].push({
                token: token,
                func: func
            });
            return token;
        }
        
        function publish(topic, args) {
            if (!topics[topic]) {
                return false;
            }
            setTimeout(function () {
                var subscribers = topics[topic],
                    len = subscribers ? subscribers.length : 0;

                while (len--) {
                    subscribers[len].func(args);
                }
            }, 0);
            return true;
        }
        
        function unsubscribe(token) {
            for (var m in topics) {
                if (topics[m]) {
                    for (var i = 0, j = topics[m].length; i < j; i++) {
                        if (topics[m][i].token === token) {
                            topics[m].splice(i, 1);
                            return token;
                        }
                    }
                }
            }
            return false;
        }

        return {            
            subscribe: subscribe,
            publish: publish,
            unsubscribe: unsubscribe
        };
    };
    
}(window.bespoke = window.bespoke || {}));