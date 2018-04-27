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
            success: function () {
                alert('New item created.');
                window.location.href = "/CatalogManagement/Index"
            },
            error: function () {
                alert('Unable to create new item. Please try again.');
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
            success: function () {
                alert('Item successfully updated.');
                window.location.href = "/CatalogManagement/Index"
            },
            error: function () {
                alert('Unable to update item. Please try again.');
            }
        });
    });

   


});