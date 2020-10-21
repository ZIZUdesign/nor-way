
$(function () {
    const id = window.location.search.substring(1);
    const url = "GetAtrip?" + id;
    $.get(url, function (trip) {
        $("#id").val(trip.id); // må ha med id inn skjemaet, hidden i html
        $("#tripDate").val(trip.tripDate);
        $("#tripTime").val(trip.tripTime);
        $("#departure").val(trip.departure);
        $("#arrival").val(trip.arrival);
        $("#adultPrice").val(trip.adultPrice);
        $("#studentPrice").val(trip.studentPrice);
        $("#childPrice").val(trip.childPrice); //CHILDPRICE
        $("#seniorPrice").val(trip.seniorPrice);
    });
});

function editTrip() {
    const trip = {
        id: $("#id").val(), // må ha med denne som ikke har blitt endret for å vite hvilken kunde som skal endres
        tripDate: $("#tripDate").val(),
        tripTime: $("#tripTime").val(),
        departure: $("#departure").val(),
        arrival: $("#arrival").val(),
        adultPrice: $("#adultPrice").val(),
        studentPrice: $("#studentPrice").val(),
        childPrice: $("#childPrice").val(), //CHILDPRICE
        seniorPrice: $("#seniorPrice").val(),
    };
    $.post("Edit", trip, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}