$('ul.dropdown li').hover(function () {
    $(this).children('ul.dropdown-menu').slideDown();
}, function () {
    $(this).children('ul.dropdown-menu').slideUp();
});