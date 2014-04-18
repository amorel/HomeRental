var placeSearch, autocomplete;
var componentForm = {
    street_number: 'short_name',
    route: 'long_name',
    locality: 'long_name',
    administrative_area_level_1: 'short_name',
    country: 'long_name',
    postal_code: 'short_name'
};
var addressvalid = '';
var componentresult = {
    street_number: '',
    route: '',
    locality: '',
    administrative_area_level_1: '',
    country: '',
    postal_code: ''
};

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
*  Select picker to choose the number of guest
*/
function initselpick() {
    $('.selectpicker').selectpicker();
}


/*
* Datepicker to select 2 dates for interval
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
    autocomplete = new google.maps.places.Autocomplete(
        /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
        { types: ['geocode'] });
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        fillInAddress();
    });
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();

    for (var property in componentresult) {
        componentresult[property] = '';
    }

    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            componentresult[addressType] = val;
        }
    }
    addressvalid = $("#autocomplete").val();
}

/*
* Submit research
*/
function submitresearch() {
    //convert address "Chaussée de Wavre 17, Brussels, Belgium" => "Chaussée-de-Wavre-17--Brussels--Belgium"
    var result = addressvalid.replace(/, /g, "--");
    result = result.replace(/  /g, " ");
    result = result.replace(/ /g, "-");

    //Creation URL with Query String.
    var checkin = $("#checkin").val()==""?"":"checkin=" + $("#checkin").val() + "&";
    var checkout = $("#checkout").val() == "" ? "" : "checkout=" + $("#checkout").val() + "&";
    var guests = $(".selectpicker").val();
    guests = "guests=" + guests.substr(0, 1);

    var geturl = "";
    geturl = geturl.concat('/s/', result, '?', checkin, checkout, guests);

    window.location = geturl;
}