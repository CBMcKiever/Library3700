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
                    var delay = 1000;
                    setTimeout(function () {
                        window.location.href = "/CatalogManagement/Index"
                    }, delay);
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
                    var delay = 1000;
                    setTimeout(function () {
                        window.location.href = "/CatalogManagement/Index"
                    }, delay);
                }
                else {
                    toastr.error(n.msg);
                }
            }
        });
    });


    var deleteItem = $('#deleteItemfromcatalog');
    deleteItem.on('click', function () {
        var itemID = $('#deleteID').val();
        $.ajax({
            type: "POST",
            url: "/CatalogManagement/DeleteItem",
            data: { id: itemID },
            success: function (n) {
                if (n.success) {
                    toastr.success(n.msg);
                    var delay = 1000;
                    setTimeout(function () {
                        window.location.href = "/CatalogManagement/Index"
                    }, delay);
                }
                else {
                    toastr.error(n.msg);
                }
            }
        });
    });

    $('#closedeleteitemmodal').on('click', function () {
        $('#delete-item-btn-@item.ItemId').val('');
    });

    $('.close').on('click', function () {
        $('#delete-item-btn-@item.ItemId').val('');
    });

    function updateItemStatus() {
        var statusUpdate = {
            ItemID: $("#ItemID").val(),
            AccountID: $("#AccountID").val(),
            itemStatusTypeID: $("#statusTypeID").val()
        }
        return statusUpdate;
    };

    var checkOutItem = $('#submitCheckoutButton');
    checkOutItem.on('click', function () {
        toastr.options = {
            "positionClass": "toast-bottom-full-width"
        };
        var itemstatusviewmodel = updateItemStatus();
        $.ajax({
            type: "POST",
            url: "/CatalogManagement/UpdateItemStatus",
            datatype: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ itemstatusviewmodel: itemstatusviewmodel }),
            success: function (n) {
                if (n.success) {
                    toastr.success(n.msg);
                    var delay = 5000;
                    setTimeout(function () {
                        window.location.href = "/CatalogManagement/Index"
                    }, delay);
                }
                else {
                    toastr.error(n.msg);
                }
            }
        });
    });
   


});