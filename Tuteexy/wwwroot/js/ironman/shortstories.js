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
            "url": "/Ironman/ShortStories/GetAll"
        },
        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div>
                                <a class="a-pointer" href="/Ironman/ShortStories/Answer/${data}" data-toggle="tooltip" data-placement="top" title="Answer">
                                    <i class="fas fa-check-square"></i> 
                                </a>
                                <a class="a-pointer" onclick=Authorized("/Ironman/ShortStories/Approved/${data}") data-toggle="tooltip" data-placement="top" title="Authorized">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a class="a-pointer" onclick=ReplyClose("/Ironman/ShortStories/ReplyClose/${data}") data-toggle="tooltip" data-placement="top" title="Reply Close">
                                    <i class="fas fa-calendar-times"></i> 
                                </a>
                                <a class="a-pointer" onclick=Offensive("/Ironman/ShortStories/Offensive/${data}") data-toggle="tooltip" data-placement="top" title="Offensive">
                                    <i class="fas fa-minus-circle"></i> 
                                </a>
                                <a class="a-pointer" onclick=Delete("/Ironman/ShortStories/Delete/${data}") data-toggle="tooltip" data-placement="top" title="Delete">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                },
            },
            { "data": "title" },
            { "data": "isreplyclose" },
            { "data": "isoffensive" },
            { "data": "isapproved" },
            { "data": "submitteddate" }

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
        title: "Are you sure you want to Approve?",
        text: "Make Sure you check all documents before approve",
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



function ReplyClose(url) {
    swal({
        title: "Are you sure you want to mark as Reply Close?",
        text: "Make Sure you check all documents before mark reply close",
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

function Offensive(url) {
    swal({
        title: "Are you sure you want to mark as Offensive?",
        text: "Make Sure you check all documents before mark offensive",
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