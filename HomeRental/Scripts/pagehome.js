/*
* Onload features
*/
jQuery(document).ready(function () {
    $("#submitsearch").click(function () { submitresearch(); });
    $(".iconspec1").click(function () { $("#checkin").focus() });
    $(".iconspec2").click(function () { $("#checkout").focus() });
    initselpick();
    initdatepick();
    initialize();
});


/*
*  Init "Select picker" to choose the number of guest
*  http://silviomoreto.github.io/bootstrap-select/
*
*/
function initselpick() {
    $('.selectpicker').selectpicker();
}


/*
*  Datepicker to select 2 dates for interval
*  http://www.eyecon.ro/bootstrap-datepicker/
*
*/
function initdatepick() {
    var nowTemp = new Date();
    var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

    var checkin = $('#checkin').datepicker({
        onRender: function (date) {
            return date.valueOf() < now.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        if (ev.date.valueOf() > checkout.date.valueOf()) {
            var newDate = new Date(ev.date)
            newDate.setDate(newDate.getDate() + 1);
            checkout.setValue(newDate);
        }
        checkin.hide();
        $('#checkout')[0].focus();
    }).data('datepicker');
    var checkout = $('#checkout').datepicker({
        onRender: function (date) {
            return date.valueOf() <= checkin.date.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        checkout.hide();
    }).data('datepicker');
}
    
/*
* using the autocomplete feature of the Google Places API to help users fill in the information.
*/
function initialize() {
    // Create the autocomplete object
    var autocomplete = new google.maps.places.Autocomplete(
        /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
        { types: ['geocode'] });
}

/*
* Submit research
*/
function submitresearch() {
    var addressvalid = $("#autocomplete").val();
    if (addressvalid == "") return;
    //convert address "Chaussée de Wavre 17, Brussels, Belgium" => "Chaussée-de-Wavre-17--Brussels--Belgium"
    var result = addressvalid.replace(/\//g, "---");
    result = result.replace(/, /g, "--");
    result = result.replace(/  /g, " ");
    result = result.replace(/ /g, "_");
    //Creation URL with Query String.
    var checkin = $("#checkin").val()==""?"":"checkin=" + $("#checkin").val() + "&";
    var checkout = $("#checkout").val() == "" ? "" : "checkout=" + $("#checkout").val() + "&";
    var guests = $(".selectpicker").val();
    guests = "guests=" + guests.substr(0, 1);

    var geturl = "";
    geturl = geturl.concat('/s/', result, '?', checkin, checkout, guests);

    window.location = geturl;
}