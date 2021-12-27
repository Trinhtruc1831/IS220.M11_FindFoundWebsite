jQuery(document).ready(function($) {
    // open search
    $('header.top .topmenu li.search').on('click', 'a', function (e) {
        $('.search-bar').fadeIn();
        $('.topmenu').fadeOut();
        e.preventDefault();
    });
    // close search
    $(document).mouseup(function (e) {
    var container = $('.search-bar form');
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        $('.search-bar').fadeOut();
        $('.topmenu').fadeIn();
    }
    });
    });