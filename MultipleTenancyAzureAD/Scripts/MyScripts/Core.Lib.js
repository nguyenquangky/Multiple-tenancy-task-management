

var CoreLib = (function () {
    function CoreLib() { }

    CoreLib.prototype.ConfirmationDelete = function (elementId) {
        $(elementId).on("click", function () {
            let _id = $(this).data("id");
            let _url = $(this).data("url");

            if (window.confirm("Are you want to delete this record?")) {
                $.ajax({
                    type: "POST",
                    url: _url,
                    data: JSON.stringify({ id: _id }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    complete: function (result) {
                        if (result.responseText) {
                            $('body').html(result.responseText);
                        }
                    }
                })
            }
        });
    };
    return CoreLib;
}());
