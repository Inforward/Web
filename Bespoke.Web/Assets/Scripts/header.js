
(function (bespoke) {

    bespoke.header = function (require) {

        var $ = require('$'),
            $header = $("#header"),
            $clone = $header.clone(true),
            pubsub = require('pubsub'),
            events = require('events');
        
        $clone.addClass("clone").hide().appendTo("#body");
        
        pubsub.subscribe(events.window.scroll, function (scrollTop) {

            if (scrollTop < $header.height())
                $clone.fadeOut("fast");
        });

        pubsub.subscribe(events.window.scrollStop, function (scrollTop) {

            if (scrollTop < $header.height())
                return;

            if (!$clone.is(":visible"))
                $clone.css({ top: $clone.height() * -1 }).show().animate({ top: 0 }, 400);
        });
    };

}(window.bespoke = window.bespoke || {}));