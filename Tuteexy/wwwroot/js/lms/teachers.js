var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "pageLength": 100,
        "ordering": false,
        "ajax": {
            "url": "/Lms/SchoolTeachers/GetAll"
        },
        "columns": [
            { "data": "schoolName" },
            { "data": "name" },
            { "data": "approvedBy" },
            { "data": "isApproved" },
            {
                "data": "schoolTeacherID",
                "render": function (data) {
                    return `
                            <div class="buttons has-addons is-right">


                                <a onclick=Delete("/Lms/SchoolTeachers/Delete/${data}") class="button is-danger is-small has-tooltip-top" data-tooltip="Delete School">
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