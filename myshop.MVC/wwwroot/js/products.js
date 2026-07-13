$(document).ready(function () {

    $("#mytable").DataTable({
        ajax: {
            url: "/Product/GetData",
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { 
                data: "name",
                render: function (data, type, row) {
                    return `<a href="/Product/GetDetails/${row.id}" class=text-decoration-none> ${data}</a>
                    `
                }
            },
            {
                data: "description",
                render: function (data) {
                    if (!data) return "";

                    const words = data.split(/\s+/);

                    return words.length > 7
                        ? words.slice(0, 7).join(" ") + "..."
                        : data;
                }
            },
            { data: "price" },
            { data: "categoryName" },
            {
                data: "id",
                render: function (id) {
                    return `
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button onclick="Delete('/Product/Delete/${id}')" class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ],
        autoWidth: false,
        scrollX: true
    });

});

function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "This action will permanently delete the product.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {

                    if (data.success) {

                        $('#mytable').DataTable().ajax.reload();

                        Swal.fire("Deleted!", data.message, "success");
                    }
                    else {

                        Swal.fire("Error!", data.message, "error");
                    }
                }
            });

        }

    });

}