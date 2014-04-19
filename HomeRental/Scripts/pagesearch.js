/*
* Onload features
*/
jQuery(document).ready(function () {
    $("#submitsearch").click(function () { submitresearch(); });
    $(".iconspec1").click(function () { $("#checkin").focus() });
    $(".iconspec2").click(function () { $("#checkout").focus() });
    initialize();
});

/*
*  Selection of the correct parameter in selectpicker (nb Guests)
*/
function filterpara(checkin, checkout, guests) {
    //init datepicker
    initdatepick();

    //Selection of the correct parameter in selectpicker (nb Guests)
    $('.selectpicker').val(guests);
    $('.selectpicker').selectpicker('render');

    //Select of the correct date parameter
    var date = new Date(checkin.substring(6, 10), checkin.substring(3, 5), checkin.substring(0, 2));
    alert(date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear());
    $('#checkin').val(date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear());

    var date = new Date(checkout.substring(6, 10), checkout.substring(3, 5), checkout.substring(0, 2));
    alert(date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear());
    $('#checkout').val(date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear());
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
* Initialize Google MAP API
*/
function initialize() {
    var mapOptions = {
        center: new google.maps.LatLng(0, 0),
        zoom: 2
    };
    var map = new google.maps.Map(document.getElementById('map-canvas'),
      mapOptions);

    var input = /** @type {HTMLInputElement} */(
        document.getElementById('pac-input'));

    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
        }


        var address = '';
        if (place.address_components) {
            address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }
    });

    // Sets a listener on a radio button to change the filter type on Places
    // Autocomplete.
    function setupClickListener(id, types) {
        var radioButton = document.getElementById(id);
        google.maps.event.addDomListener(radioButton, 'click', function () {
            autocomplete.setTypes(types);
        });
    }

    setupClickListener('changetype-all', []);
    setupClickListener('changetype-establishment', ['establishment']);
    setupClickListener('changetype-geocode', ['geocode']);
}