
(function ($) {

    $(function () {
        var $sliders = $(".iosSlider");

        $(document).foundation();

        if ($sliders.length) {
            $sliders.iosSlider({
                snapToChildren: true,
                desktopClickDrag: true,
                infiniteSlider: true,
                snapSlideCenter: true,
                navNextSelector: '.next',
                navPrevSelector: '.prev',
                autoSlide: true,
                autoSlideTimer: 5000,
                autoSlideHoverPause: true
            });
        }

        $("#header").header();

        $("select[data-role='jump-menu']").on("change", function() {
            var val = $(this).find("option:selected").val();

            if (val && val.length)
                document.location = val;
        });

    });

})(jQuery);