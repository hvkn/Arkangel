Dropzone.options.uploadWidget = {
    paramName: 'file',
    maxFilesize: 1,
    maxFiles: 1,
    acceptedFiles: '.jpg',
};

$(document).ready(function () {
    var owl = $('.owl-carousel');
    owl.owlCarousel({
        loop: true,
        mouseDrag: true,
        lazyLoad: true,
        margin: 10,
        responsiveClass: true,
        loop: true,
        nav: true,
        navText: [
            "<i class='fa fa-chevron-left'></i>",
            "<i class='fa fa-chevron-right'></i>"
        ],
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 3,
            },
            1000: {
                items: 7,
            }
        }
    });

    owl.on('mousewheel', '.owl-stage', function (e) {
        if (e.deltaY > 0) {
            owl.trigger('next.owl');
        } else {
            owl.trigger('prev.owl');
        }
        e.preventDefault();
    });
});