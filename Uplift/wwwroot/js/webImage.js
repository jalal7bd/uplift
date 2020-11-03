var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/admin/webimage/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
<a href="/Admin/webimage/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px;'>
<i calss='fas fa-edit'></i>Edit
</a>
&nbsp;
<a onclick=Delete("/Admin/webimage/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer;width:100px;'>
<i calss='fas fa-trash-alt'></i>Delete
</a>
</div>
`;
                }, "width": "50%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "Once deleted, you will not be able to recover your data!",
        icon: "warning",
        //buttons: true,
        buttons: ["Cancel", "Yes, delete it!"],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'DELETE',
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        toastr.success(data.message);
                        $('#tblData').DataTable().ajax.reload();
                    },
                    failure: function (data) {
                        toastr.error(data.message);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            }
            else {
                swal("Your data is safe!");
            }
        });
}
