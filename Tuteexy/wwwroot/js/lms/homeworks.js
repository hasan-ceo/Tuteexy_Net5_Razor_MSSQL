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
            "url": "/Lms/Homeworks/GetAll"
        },
        "columns": [
            { "data": "classRoomName" },
            { "data": "subject" },
            { "data": "title" },
            { "data": "schdate" },
            { "data": "datedue" },
            {
                "data": "homeworkID",
                "render": function (data) {
                    return `
                            <div class="buttons has-addons is-right">
  <a href="/Lms/Homeworks/HWPreview/${data}" class="button is-link is-small has-tooltip-top" data-tooltip="Edit School">
                                    <i class="fas fa-print"></i> 
                                </a>
  <a href="/Lms/Homeworks/Upsert/${data}" class="button is-primary is-small has-tooltip-top" data-tooltip="Edit School">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Lms/Homeworks/Delete/${data}") class="button is-danger is-small has-tooltip-top" data-tooltip="Delete School">
                                    <i class="fas fa-trash-alt"></i> 
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