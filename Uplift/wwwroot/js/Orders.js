var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrder");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPendingOrder");
        }
        else if (url.includes("rejected")) {
            loadDataTable("GetAllRejectedOrder");
        }
        else {
            loadDataTable("GetAllOrder");
        }
    }
});
    function loadDataTable(url) {
    dataTable = $('#tblData').dataTable({
        ajax: {
            url: "/admin/order/" + url,
            type: "GET",
            datatype: "json"
        },
        columns: [
            { "data": "name", "width": "20%"},
            { "data": "phone", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "productCount", "width": "15%" },
            {
                "data": "status", "width": "15%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
<a href="/Admin/order/Details/${data}" class='btn-sm text-dark'data-toggle='tooltip' data-placement='bottom' title='Deatils' style='cursor:pointer;width:100px;'>
<i class="far fa-list-alt"></i></a>
</div>
`;
                }, "width": "15%"
            },
        ],
       
        language: {
            "emptyTable": "No records found."
        },
        width: "100%",
        //createdRow: function (row, data) {
        //    if (data['status'] === "Submitted") {
        //        $(row).addClass('bg-warning');
        //    } else if (data['status'] === "Approved") {
        //        $(row).addClass('bg-success');
        //    } else if (data['status'] === "Rejected") {
        //        $(row).addClass('bg-danger');
        //    }
        //}   
        createdRow: function (row, data) {
            if (data['status'] === "Submitted") {
                $(row).css('background-color', '#FFFCA2');
            } else if (data['status'] === "Approved") {
                $(row).css('background-color','#B2F09D');
            } else if (data['status'] === "Rejected") {
                $(row).css('background-color', '#FFFC');
            }
        } 
    });
}

