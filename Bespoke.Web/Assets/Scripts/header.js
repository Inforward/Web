
(function ($, bespoke) {

    $.widget("bespoke.header", {
        options: {
        },

        _create: function () {

            // Clone our header
            this.$clone = this.element.clone(true);

            // Handle scrolling events
            this._on($(window), {
                scroll: "windowScroll",
                scrollstop: "windowScrollStop"
            });
            
            this.$clone.addClass("clone").hide().appendTo("#body");
        },

        windowScroll: function () {
            var headerHeight = this.element.height(),
                scrollPosition = $(window).scrollTop();

            if (scrollPosition < headerHeight)
                this.$clone.fadeOut("fast");
        },

        windowScrollStop: function () {
            var scrollTop = $(window).scrollTop(),
                headerHeight = this.element.height();

            if (scrollTop < headerHeight) {
                return;
            }

            if (!this.$clone.is(":visible")) {
                this.$clone
                        .css({ top: this.$clone.height() * -1 })
                        .show()
                        .animate({
                            top: 0
                        }, 400);
            }
        }
    });

})(jQuery, this.bespoke = this.bespoke || {});

