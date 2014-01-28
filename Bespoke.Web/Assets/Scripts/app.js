
(function (app, global, $) {

    function require(service) {

        if (service in global)
            return global[service];

        if (service in app) {
            if (typeof app[service] === 'function') {
                app[service] = app[service](require);
            }
            return app[service];
        }

        throw new Error('unable to locate ' + service);
    }

    $(function () {
        var registration,
            module;

        // Bootstrap all modules
        for (registration in app) {

            module = app[registration];

            if (typeof module === 'function') {
                app[registration] = module(require);
            }
        }

        // Perform any post-boostrap (global) initialization
        $(document).foundation();

        $("[data-login]").on("click", function(e) {
            e.preventDefault();
            bespoke.login.show();
        });

    });

})(window.bespoke = window.bespoke || {}, window, jQuery);