$(function () {
    $("#priceTable").fadeToggle("slow");
    $("#datepicker").datepicker("setDate", new Date());
    getStations();
});

function getStations() { //Autocomplete av stasjon-text
    $.get("GetStations", function (stations) {

        var Departure = [];
        var Aarrival = []
        for (let station of stations) {
            Departure.push(station.stationName);
            Aarrival.push(station.stationName);
        }
        $("#departure").autocomplete({
            source: Departure
        });
        $("#arrival").autocomplete({
            source: Aarrival
        });
    });
}

function validateOrder() { //Sjekker om input er gyldig
    const departureOK = validateDeparture($("#selectedDeparture").val());
    const arrivalOK = validateArrival($("#selectedAarrival").val());
    if (departureOK && arrivalOK) {
        console.log("Order Validated!")
        findTrip();
    }
}

function findTrip() {
    const orderTicket = {
        selectedDeparture: $("#departure").val(),
        selectedAarrival: $("#arrival").val()
    }
    const url = "FindTrip";
    $.get(url, orderTicket, function (trips) {
        {
            formaterTrips(trips);
        }
    });
};

function formaterTrips(trips) {
    let ut = "<h3>Bus Schedule:</h3>" +
        "<table class='table table-hover table-striped table-bordered'>" +
        "<thead>" +
        "<tr>" +
        "<th>ID</th>" +
        "<th>Date</th>" +
        "<th>Time</th>" +
        "<th>Departure</th>" +
        "<th>Arrival</th>" +
        "<th>Adult price</th>" +
        "<th>Student price</th>" +
        "<th>Youth price</th>" +
        "<th>Senior price</th>" +
        "<th></th>" +
        "</tr>" +
        "</thead>";

    for (let trip of trips) {
        ut += "<tr>" +
            "<td>" + trip.id + "</td>" +
            "<td>" + trip.tripDate + "</td>" +
            "<td>" + trip.tripDate + "</td>" +
            "<td>" + trip.departure + "</td>" +
            "<td>" + trip.arrival + "</td>" +
            "<td>" + trip.adultPrice + "</td>" +
            "<td>" + trip.studentPrice + "</td>" +
            "<td>" + trip.childPrice + "</td>" +
            "<td>" + trip.seniorPrice + "</td>" +
            "<td> <a class='btn btn-primary' onclick='formaterSelectedTrips(" + trip.id + ")'>Select</a></td>" + //ENDRET FRA selectedTrips(id)
            "</tr>";
    }
    ut += "</table>";
    $("#busSchedule").html(ut);
}

function validatePayment() {
    const departureOK = validateDeparture($("#selectedDeparture").val());
    const arrivalOK = validateArrival($("#selectedAarrival").val());
    if (departureOK && arrivalOK) {
        findTrip();
    }
}



/*function selectedTrip(id) {
    formaterSelectedTrips(id);
    const orderTicket = {
        //selectedDeparture: $("#departure").val(),
        //selectedAarrival: $("#Aarrival").val()
        //selectedDate: $("#selectedDate").val(),
        //arrival: $("#arrival").val(),
        //adultPrice: $("#adultPrice").val(),
        //studentPrice: $("#studentPrice").val(),
        //childPrice: $("#childPrice").val(),
        //seniorPrice: $("#seniorPrice").val()
    }

};*/


function formaterSelectedTrips(id) {
    $("#priceTable").fadeToggle("slow", "linear");
    $("#id").val(id); // må ha med id inn skjemaet, hidden i html
    window.location.href = "#priceTable"; //Hopper til kjøpebildet!
}

function getPrice() { //Regner ut pris
    const orderTicket = {
        tripId: $("#id").val(),
        numberOfAdults: $("#adults").val(),
        numberOfStudents: $("#students").val(),
        numberOfKids: $("#youths").val(),
        numberOfSeniors: $("#seniors").val(),
    }
    const url = "GetPrice";
    $.get(url, orderTicket, function (result) {
        {
            console.log("getPrice pass");
            console.log(result.totalPrice)
            $("#totalPrice").html("<strong>" + result.totalPrice + " NOK</strong>");
        }
    });
}

function payment() {
    const order = {
        tripId: $("#id").val(),
        numberOfAdults: $("#numberOfAdults").val(),
        numberOfStudents: $("#numberOfStudents").val(),
        numberOfKids: $("#numberOfKids").val(),
        numberOfSeniors: $("#numberOfSeniors").val(),
        departure: $("#departure").val(),
        arrival: $("#arrival").val(),
        kontoNo: $("#kontoNo").val(),
        pinKode: $("#pinKode").val(),
        kontoNo: $("#kontoNo").val(),
        mobilNo: $("#mobilNo").val()
    }
    const url = "Save";
    $.post(url, order, function (OK) {
        if (OK) {
            console.log("Payment is ok")
            alert("Payment is accepted, and your ticket will be sent via sms as soon!");
            alert("Good Bye. Enjoy Your Trip");
            window.location.href = '../orderTicket/index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
};

function printReciept() {
    const reciept = {
        date: $("#datepicker").val(),
        departure: $("#departure").val(),
        arrival: $("#arrival").val(),
        adults: $("#adults").val(),
        students: $("#students").val(),
        youths: $("#youths").val(),
        seniors: $("#seniors").val(),
        totalPrice: $("#totalPrice").text()
    };

    sessionStorage.object = JSON.stringify(reciept);
    window.location.href = "reciept.html";
}


function validatePayment() {
    const pinKodeOK = validatePinKode($("#pinKode").val());
    if (pinKodeOK) {
        printReciept();
    }
}
