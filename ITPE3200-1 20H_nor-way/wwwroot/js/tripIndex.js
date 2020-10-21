$(function () {
    getAllTrips();
});

function getAllTrips() {
    $.get("GetAll", function (trips) {
        formaterTrips(trips);
    });
}

function removeTrip(id) {
    const url = "RemoveBiId?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'tripIndex.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere.");
        }

    });
}

function formaterTrips(trips) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Id</th>" +
        "<th>TripDate</th>" +
        "<th>TripTime</th>" +
        "<th>Departure</th>" +
        "<th>Arrival</th>" +
        "<th>Price for Adult</th>" +
        "<th>Student</th>" +
        "<th>Youth</th>" +
        "<th>Senior</th>" +
        "<th></th>" +
        "<th></th>" +
        "<th></th><th></th>" +
        "</tr>";
    for (let trip of trips) {
        ut += "<tr>" +
            "<td>" + trip.id + "</td>" +
            "<td>" + trip.tripDate + "</td>" +
            "<td>" + trip.tripTime + "</td>" +
            "<td>" + trip.departure + "</td>" +
            "<td>" + trip.arrival + "</td>" +
            "<td>" + trip.adultPrice + "</td>" +
            "<td>" + trip.studentPrice + "</td>" +
            "<td>" + trip.childPrice + "</td>" + 
            "<td>" + trip.seniorPrice + "</td>" +
            "<td> <a class='btn btn-primary' href='edit.html?id=" + trip.id + "'>Edit</a></td>" +
            "<td> <button class='btn btn-danger' onclick='removeTrip(" + trip.id + ")'>Remove</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#tps").html(ut);
}