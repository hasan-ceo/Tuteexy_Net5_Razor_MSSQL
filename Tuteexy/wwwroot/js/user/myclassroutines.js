var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "language": {
            "emptyTable": "No data available in table"
        },
        "pageLength": 100,
        "ordering": false,
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
