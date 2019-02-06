var currentTimeTable = $("#currentTimeTable").DataTable({
    pageLength: 10
});

$(document).ready(function () {
    GetAll();
});

function PopulateDataTable(data) {
    currentTimeTable.clear();
    $(data).each(function (index, item) {
        $("#currentTimeTable").dataTable().fnAddData([
            item.currentTimeQueryId,
            formatDateTime(item.time),
            item.clientIp,
            formatDateTime(item.utcTime),
            item.timeZone
        ]);
    });
}

function GetAll() {
    ApiRequest("currenttime", "GET", null,
        function () {
            if (IsApiResponseSuccess(this)) {
                PopulateDataTable(JSON.parse(this.response));
            }
        }
    );
}

function UserAction() {
    var timeZone = $("#listTimeZone").val();
    ApiRequest("currenttime/" + timeZone + "/", "GET", null,
        function () {
            if (IsApiResponseSuccess(this)) {
                showModal($.trim($("#listTimeZone option:selected").text()),
                    formatDateTime(JSON.parse(this.response)));
                GetAll();
            }
        }
    );
}

function showModal(selectedTimeZone, value) {
    $("#modalBody").html(value);
    $("#modalHeader").html("Current Time" + (selectedTimeZone !== '' ? " in " + selectedTimeZone : ""));
    $("#currentTimeModal").modal("show");
}