$(document).ready(function () {

    function getItem() {
        var item = {
            ItemID: $("#itemID").val(),
            Title: $("#ItemTitle").val(),
            Author: $("#ItemAuthor").val(),
            ItemTypeName: $("input[type='radio'][name='ItemType']:checked").val(),
            Genre: $("#ItemGenre").val(),
            PublicationYear: $("#ItemPublicationYear").val()
        }

        return item;
    }



    $("#submitButton").click(function () {
        var catalogItem = getItem();
        $.ajax({
            type: "POST",
            url: "/CatalogManagement/CreateItem",
            datatype: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ catalogItem: catalogItem }),
            success: function (n) {
                if (n.success) {
                    toastr.success(n.msg);
                    window.location.href = "/CatalogManagement/Index"
                }
                else {
                    toastr.error(n.msg);
                }
            }
        });
    });

    $("#submitEditButton").click(function () {
        var catalogItem = getItem();
        $.ajax({
            type: "POST",
            url: "/CatalogManagement/EditItem",
            datatype: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ catalogItem: catalogItem }),
            success: function (n) {
                if (n.success) {
                    toastr.success(n.msg);
                    window.location.href = "/CatalogManagement/Index"
                }
                else {
                    toastr.error(n.msg);
                }
            }
        });
    });

   


});