var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "language": {
            "lengthMenu": "Display _MENU_ records per page",
            "zeroRecords": "Nothing found - sorry",
            "info": "Showing page _PAGE_ of _PAGES_",
            "infoEmpty": "No records available",
            "infoFiltered": "(filtered from _MAX_ total records)"
        },
        "autoWidth": false,
        "pageLength": 100,
        "ordering": false,
        "scrollX": true,
        "ajax": {
            "url": "/User/Dashboard/GetAllClassRoutine"
        },
        "columns": [
            { "data": "classname" },
            { "data": "day" },
            { "data": "p1" },
            { "data": "p2" },
            { "data": "p3" },
            { "data": "p4" },
            { "data": "p5" },
            { "data": "p6" },
            { "data": "p7" },
            { "data": "p8" },
            { "data": "p9" },
            { "data": "p10" }
        ]
    });
}
