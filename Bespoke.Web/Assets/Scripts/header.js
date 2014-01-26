
(function (bespoke) {

    bespoke.header = function (require) {

        var $ = require('$'),
            $header = $("#header"),
            $clone = $header.clone(true),
            $window = $(window);

        if (!$header.length)
            return;
        
        $clone.addClass("clone").hide().appendTo("#body");
        
        $window.on("scroll", function () {
            
            if ($window.scrollTop() < $header.height())
                $clone.fadeOut("fast");
        });

        $window.on("scrollstop", function () {

            if ($window.scrollTop() < $header.height())
                return;

            if (!$clone.is(":visible"))
                $clone.css({ top: $clone.height() * -1 }).show().animate({ top: 0 }, 400);
        });
    };

}(window.bespoke = window.bespoke || {}));