$(function () {
    formatReciept();
});

function formatReciept() { 
    var reciept = JSON.parse(sessionStorage.object);
    $("#test").html(reciept.date);
    let out = "<p><h6></br>Date: " + reciept.date + "</br>Departure Station: " + reciept.departure + "</br>Arrival Station: " + reciept.arrival + "</br >Adults: " +
        reciept.adults + "</br>Students: " + reciept.students + "</br>Youths: " + reciept.youths + "</br>Seniors: " + reciept.seniors + " </br>Total Price: " + reciept.totalPrice + "</h6></p>";
    $("#reciept").html(out);
    console.log("formatReciept ferdig. " + reciept.departure);
};