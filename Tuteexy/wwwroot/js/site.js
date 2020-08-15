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


//$(".makecircleava").addEventListener('mouseover', () => {
//    $("#imgeditprof").classList.add = 'active';
//})


//CURRENT ACTIVE PAGE

var weburl = window.location.href;
var curpage = weburl.toLowerCase().search('hub');
if (curpage > 0) {
    $('#hubnav').toggleClass("navspry");
};

var curpage = weburl.toLowerCase().search('lms');
if (curpage > 0) {
    $('#schoolnav').toggleClass("navspry");
};