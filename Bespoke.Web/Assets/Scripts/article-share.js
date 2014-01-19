
(function ($, bespoke) {

    $.widget("bespoke.articleShare", {
        options: {
            url: null,
            title: null
        },

        _create: function () {

            var that = this;
            var template = '<div class="box">' +
                             '<div class="sharebuttons">' +
                                 '<a href="#" class="facebook"></a>' +
                                 '<a href="#" class="twitter"></a>' +
                                 '<a href="#" class="googleplus"></a>' +
                                 '<a href="#" class="linkedin"></a>' +
                                 '<a href="#" class="mail"></a>' +
                             '</div>' +
                             '<div class="sharecount">{total} SHARES</div>' +
                            '</div>';
            
            this.element.sharrre({
                share: {
                    twitter: true,
                    facebook: true,
                    googlePlus: true,
                    linkedin: true
                },
                url: this.options.url,
                template: template,
                enableHover: false,
                enableTracking: true,
                //urlCurl: 'http://www.inc.com/sharrre.php',
                render: function (api, options) {
                    $(api.element).on('click', '.twitter', function () {
                        api.openPopup('twitter');
                    });
                    $(api.element).on('click', '.facebook', function () {
                        api.openPopup('facebook');
                    });
                    $(api.element).on('click', '.googleplus', function () {
                        api.openPopup('googlePlus');
                    });
                    $(api.element).on('click', '.linkedin', function () {
                        api.openPopup('linkedin');
                    });
                    $(api.element).on('click', '.mail', function () {
                        document.location.href = 'mailto:?subject=' + encodeURIComponent(that.options.title) + '&body=' + encodeURIComponent(that.options.url);
                    });
                }
            });
        }
    });

})(jQuery, this.bespoke = this.bespoke || {});

