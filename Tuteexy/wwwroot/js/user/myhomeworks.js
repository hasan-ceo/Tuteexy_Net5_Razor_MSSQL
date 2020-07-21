var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "pageLength": 100,
        "ordering": false,
        "ajax": {
            "url": "/User/Dashboard/GetAllHomeWorks"
        },
        "columns": [
            { "data": "classRoomName" },
            { "data": "subject" },
            { "data": "title" },
            { "data": "schdate" },
            { "data": "datedue" }
        ]
    });
}
