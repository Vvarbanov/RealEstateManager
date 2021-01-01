$(document).ready(function () {
    $(":input[date-picker]").datepicker({
        dateFormat: "dd-M-y",
    });
    $(":input[datetime-picker]").datetimepicker({
        dateFormat: "dd-M-y",
        timeFormat: "HH:mm:ss"
    });
    $(":input[time-picker]").timepicker({
        timeFormat: "HH:mm:ss"
    });
})