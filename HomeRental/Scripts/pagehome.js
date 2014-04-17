﻿/*
* using the autocomplete feature of the Google Places API to help users fill in the information.
*/
var placeSearch, autocomplete;
var componentForm = {
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

    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            componentForm[addressType] = val;
        }
    }
}

/*
* Submit research
*/
function submitresearch() {
}