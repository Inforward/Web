
(function (bespoke) {

    bespoke.jumpMenu = function (require) {

        var $ = require('$'),
            $elements = $("[data-role='jumpmenu']");

        if (!$elements.length)
            return;
        
        $elements.on("change", function () {
            var val = $(this).find("option:selected").val();

            if (val && val.length)
                document.location = val;
        });
    };

}(window.bespoke = window.bespoke || {}));

