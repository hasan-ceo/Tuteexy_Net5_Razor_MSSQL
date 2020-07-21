var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "pageLength": 100,
        "ordering": false,
        "ajax": {
            "url": "/Ironman/Schools/GetAll"
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
                                <a onclick=Authorized("/Ironman/Schools/Authorize/${data}") class="button is-warning is-small has-tooltip-top" data-tooltip="Delete School">
                                    <i class="far fa-check-square"></i> 
                                </a>
                                <a onclick=Delete("/Ironman/Schools/Delete/${data}") class="button is-danger is-small has-tooltip-top" data-tooltip="Delete School">
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

function Authorized(url) {
    swal({
        title: "Are you sure you want to Authorize?",
        text: "Make Sure you check all documents before authorize",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({ 
                type: "POST",
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