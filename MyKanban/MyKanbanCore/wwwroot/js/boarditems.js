$("document").ready(function () {

    // Enable jQuery sortable
    $("ul.connectedSortable").each(function (i) {
        $(this).sortable({
            connectWith: ".connectedSortable",
            dropOnEmpty: true,
            stop: function (event, ui) {
                var moved = ui.item,
                    replaced = ui.item.prev();

                // if replaced.length === 0 then the item has been pushed to the top of the list
                // in this case we need the .next() sibling
                if (replaced.length == 0) {
                    replaced = ui.item.next();
                }

                alert("moved ID:" + moved.attr("data-id") + "replaced ID:" + replaced.attr("data-id"));
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

    //// Update column position
    //$("li").each(function () {
    //    $(this).mouseup(function () {
    //        ajaxCall1($(this));
    //    });
    //});
});

function ajaxCall1(li) {
    $.ajax({
        url: "../../api/kanbanitems/" + li.attr("data-id"),
        type: "GET",
        success: function (data) {
            ajaxCall2(li, data);
        }
    });
}
function ajaxCall2(li, data) {
    data.columnId = Number(li.parent().attr("data-id"));
    $.ajax({
        url: "../../api/kanbanitems/" + data.itemId,
        type: "PUT",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (result) {
            alert("updated");
        },
        error: function (passParams) {
            console.log("Error is " + passParams);
        }
    });
}