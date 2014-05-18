var map;
/*
* Onload features
*/
jQuery(document).ready(function () {

    $("#submitsearch").click(function () { submitresearch(); });
    $("#gly1").click(function () { $("#checkin").focus(); });
    $("#gly2").click(function () { $("#checkout").focus(); });
    $("#pac-input").val(getAddress());
    initializeGoogleMaps();
    $('#slider-price').slider()
        .on('slideStop', function (ev) {
            getAjaxDataLocationInArea();
        });
    $(".selectpicker").change(function () {
        getAjaxDataLocationInArea();
    });
});

function addMarker(Lat, Lng) {
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(Lat.replace(/\,/g, '.'), Lng.replace(/\,/g, '.')),
        map: map
    });
}

/*
*  Ajax method that retrieves data locations found in the area.
*/
function getAjaxDataLocationInArea() {

    var bnds = map.getBounds();

    $(".contentLoad").show();

    var datecheckin = $("#checkin").val().length = 9 ? "0" + $("#checkin").val() : $("#checkin").val();
    var datecheckout = $("#checkout").val().length = 9 ? "0" + $("#checkout").val() : $("#checkout").val();

    var request = {
        bounds: { northEastLatLng: { Lat: bnds.Aa.j, Lng: bnds.ra.k }, southWestLatLng: { Lat: bnds.Aa.k, Lng: bnds.ra.j } },
        checkin: datecheckin.substr(3, 3) + datecheckin.substr(0, 3) + datecheckin.substr(6, 4),
        checkout: datecheckout.substr(3, 3) + datecheckout.substr(0, 3) + datecheckout.substr(6, 4),
        guests: $("#guests").val(),
        rangePrice: $('#slider-price').data('slider').getValue()
    };

    $.ajax({
        type: "POST",
        url: '/s/LocationInAreaAjax',
        contentType: "application/json; charset=utf-8",
        processData: false,
        data: JSON.stringify(request),
        dataType: "html",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        $("#status").html('Ajax Success !').css("color", "green");;
        $("#ajaxframe").html(data);
        $(".contentLoad").hide();
    }

    function errorFunc() {
        $("#status").html('Ajax error !').css("color", "red");
    }
};

/*
*  Regex to get address parameter
*  Format of the address
*/
function getAddress() {
    //Regex to get address parameter
    var patt = /\/s\/(.*)\?/g;
    var result = patt.exec(decodeURI(location.href));
    var address = result[1];
    //convert address "Chaussée-de-Wavre-17--Brussels--Belgium" => "Chaussée de Wavre 17, Brussels, Belgium"
    address = address.replace(/---/g, "/");
    address = address.replace(/--/g, ", ");
    address = address.replace(/_/g, " ");
    return address;
}

/*
*  Selection of the correct parameter Datepicker & selectpicker (nb Guests)
*/
function filterpara(checkin, checkout, guests) {
    //init datepicker
    initdatepick();

    //Selection of the correct parameter in selectpicker (nb Guests)
    $('.selectpicker').val(guests);
    $('.selectpicker').selectpicker('render');

    //Select of the correct date parameter on localhost
    if (checkin != "" && document.location.hostname == "localhost") {
        var date = new Date(checkin.substring(3, 5) + "/" + checkin.substring(0, 2) + "/" + checkin.substring(6, 10));
        $('#checkin').datepicker('setValue', date);

        if (checkout != "") {
            var date = new Date(checkout.substring(3, 5) + "/" + checkout.substring(0, 2) + "/" + checkout.substring(6, 10));
            $('#checkout').datepicker('setValue', date);

        }

    }
        //Select of the correct date parameter on Hosting server
    else if (checkin != "") {
        $("#checkin").val(checkin);

        if (checkout != "") {
            $('#checkout').val(checkout);
        }
    }
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
        getAjaxDataLocationInArea();
    }).data('datepicker');

    var checkout = $('#checkout').datepicker({
        onRender: function (date) {
            return date.valueOf() <= checkin.date.valueOf() ? 'disabled' : '';
        }
    }).on('changeDate', function (ev) {
        checkout.hide();
        getAjaxDataLocationInArea();
    }).data('datepicker');
}

/*
* Initialize Google MAP API
*/
function initializeGoogleMaps() {
    var mapOptions = {
        center: new google.maps.LatLng(0, 0),
        zoom: 2
    };
    map = new google.maps.Map(document.getElementById('map-canvas'),
      mapOptions);
    var geocoder = new google.maps.Geocoder();

    $(document).ready(function (e) {
        var request = {
            address: $("#pac-input").val()
        }
        geocoder.geocode(request, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
                map.setZoom(13);
                var bounds = results[0].geometry.bounds;

                map.fitBounds(bounds);
                var latlng = results[0].geometry.location;
            }
        });
    });

    input = /** @type {HTMLInputElement} */(
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
            map.setZoom(17);
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

    google.maps.event.addListener(map, 'idle', function () {
        getAjaxDataLocationInArea();
    });
}