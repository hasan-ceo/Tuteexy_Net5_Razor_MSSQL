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
            "url": "/Lms/Schools/GetAll"
        },
        "columns": [
            { "data": "id" },
            { "data": "schoolname" },
            { "data": "phonenumber" },
            { "data": "isauthorize" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="buttons has-addons is-right">
 <a href="/Lms/SchoolNotices/Create/${data}" class="button is-gray is-small has-tooltip-top" data-tooltip="Add School Notice">
                                    <i class="fas fa-desktop"></i> 
                                </a>
                                <a href="/Lms/ClassRooms/Create/${data}" class="button is-warning is-small has-tooltip-top" data-tooltip="Add Class">
                                    <i class="fas fa-warehouse"></i> 
                                </a>

                                <a href="/Lms/Schools/Upsert/${data}" class="button is-primary is-small has-tooltip-top" data-tooltip="Edit School">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Lms/Schools/Delete/${data}") class="button is-danger is-small has-tooltip-top" data-tooltip="Delete School">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
<a href="/Lms/Subjects/Create/${data}" class="button is-success is-small has-tooltip-top" data-tooltip="Add Subject">
                                    <i class="fas fa-book"></i> 
                                </a>
                            </div>
                           `;
                },
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}