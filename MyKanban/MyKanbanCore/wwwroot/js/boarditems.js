$("document").ready(function () {

    // Enable jQuery sortable
    $("ul.connectedSortable").each(function (i) {
        $(this).sortable({
            connectWith: ".connectedSortable",
            dropOnEmpty: true,
            stop: function (event, ui) {
                var itemId = Number(ui.item.attr("data-itemId"));
                var columnId = Number(ui.item.parent().attr("data-columnId"));
                var positionId = Number(ui.item.index());
                updateItem(itemId, columnId, positionId);
            }
        }).disableSelection();
    });

    // Hide/show "add new item" button
    $(".col").each(function () {
        var col = $(this);
        col.mouseenter(function () {
            col.children("a").css("visibility", "visible");
        });
        col.mouseleave(function () {
            col.children("a").css("visibility", "hidden");
        });
    });
});

function updateItem(itemId, newColumn, newPosition) {
    $.ajax({
        url: "../../api/kanbanitems/" + itemId,
        type: "GET",
        success: function (data) {
            data.columnId = newColumn;
            data.position = newPosition;
            put(data);
        }
    });
}

function put(data) {
    $.ajax({
        url: "../../api/kanbanitems/" + data.itemId,
        type: "PUT",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (result) {
            //alert("updated");
        },
        error: function (passParams) {
            console.log("Error is " + passParams);
        }
    });
}