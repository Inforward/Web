
(function (bespoke, window) {

    bespoke.slideManager = function (require) {

        var $ = require('$'),
            pubsub = require('pubsub'),
            events = require('events'),
            $window = $(window);

        $window.on("scroll", function() {
            pubsub.publish(events.window.scroll, $window.scrollTop());
        });

        $window.on("scrollstop", function () {
            pubsub.publish(events.window.scrollStop, $window.scrollTop());
        });
    };

}(window.bespoke = window.bespoke || {}, window));