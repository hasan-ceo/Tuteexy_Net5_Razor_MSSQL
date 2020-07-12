var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Ironman/Pages/GetAll"
        },
        "columns": [
            { "data": "pageName" },
            {
                "data": "pageID",
                "render": function (data) {
                    return `
                            <div class="buttons has-addons is-centered">
                                <a href="/Ironman/Pages/Upsert/${data}" class="button is-primary">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Ironman/Pages/Delete/${data}") class="button is-danger">
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