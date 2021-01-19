var dataTable;

$(document).ready(function () {
    loadDataTable();
})


function loadDataTable(){
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            {
                "data": "name",
                "width": "10%"
            },
            {
                "data": "streetAdress",
                "width": "10%"
            },
            {
                "data": "city",
                "width": "10%"
            },
            {
                "data": "state",
                "width": "10%"
            },
            {
                "data": "postalCode",
                "width": "10%"
            },
            {
                "data": "phoneNumber",
                "width": "10%"
            },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if(data)
                        return `<input type="checkbox" value=${data} onclick="return false" checked />`
                    else
                        return `<input type="checkbox" value=${data} onclick="return false"/>`
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor: pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onClick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor: pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>`;
                },
                "width": "10%"
            }
        ]



    });

}



function Delete(url) {
    swal({
        title: "Are you sure you want Delete?",
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
                        $('#tblData').dataTable().fnDestroy();
                        loadDataTable();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}