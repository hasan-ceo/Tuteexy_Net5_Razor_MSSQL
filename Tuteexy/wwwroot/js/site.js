// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Close menu
$("#close-menu").click(function (e) {
    e.preventDefault();
    $("#sidebar-wrapper").toggleClass("active");
});
// Open menu
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#sidebar-wrapper").toggleClass("active");
});


$(".menu-overlay").click(function (e) {
    e.preventDefault();
    $("#sidebar-wrapper").toggleClass("active");
});