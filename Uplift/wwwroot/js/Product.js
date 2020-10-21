var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/admin/product/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "articleNo", "width": "5%" },
            { "data": "manufactureCode", "width": "5%" },
            { "data": "barcode", "width": "5%" },
            { "data": "quantity", "width": "5%" },
            { "data": "b2CPrice", "width": "5%" },
            { "data": "b2BPrice", "width": "5%" },
            { "data": "premiumPrice", "width": "5%" },
            { "data": "distributorPrice", "width": "5%" },
            { "data": "purchasePrice", "width": "5%" },
            { "data": "category.name", "width": "5%" },
            { "data": "frequency.frequencyCount", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
<a href="/Admin/product/Upsert/${data}" class='btn-success  btn-sm text-white' style='cursor:pointer;width:100px;'>
<i class="far fa-edit"></i>
</a>
&nbsp;
<a onclick=Delete("/Admin/product/Delete/${data}") class='btn-danger btn-sm text-white' style='cursor:pointer;width:100px;'>
<i class="far fa-trash-alt"></i>
</a>
</div>
`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}
//

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
