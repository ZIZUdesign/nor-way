function validateTripDate(tripDate) {
    const regexp = /(0[1-9]|[12][0-9]|3[01])[ \.-](0[1-9]|1[012])[ \.-](19|20|)\d\d/;

    const ok = regexp.test(tripDate);
    if (!ok) {
        $("#tripDateError").html("Please select a valid date");
        return false;
    }
    else {
        $("#tripDateError").html("");
        return true;
    }
}

function validateTripTime(tripTime) {
    const regexp = /([01]\d|2[0-3]):?([0-5]\d)$/;
    const ok = regexp.test(tripTime);
    if (!ok) {
        $("#tripTimeError").html("Please select a valid time");
        return false;
    }
    else {
        $("#tripTimeError").html("");
        return true;
    }
}

function validateDeparture(departure) {  //Sjekker at input i departure-text er riktig
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
    var ok = regexp.test(departure);
    if (!ok) {
        $("#departureError").html("Input must consist of atleast 2 letters.");
        return false;
    }
    else {
        $("#departureError").html("");
        return true;
    }
}

function validateArrival(arrival) { //Sjekker at input i arrival-text er riktig
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
    var ok = regexp.test(arrival);
    if (!ok) {
        $("#arrivalError").html("Input must consist of atleast 2 letters.");
        return false;
    }
    else {
        $("#arrivalError").html("");
        return true;
    }
}

function validateAdultPrice(adultPrice) {
    var regexp = /^\d+(,\d{1,2})?$/;
    var ok = regexp.test(adultPrice);
    if (!ok) {
        $("#adultPriceError").html("The Price Should Be A Valid Number");
        return false;
    }
    else {
        $("#adultPriceError").html("");
        return true;
    }
}


function validateStudentPrice(studentPrice) {
    var regexp = /^\d+(,\d{1,2})?$/;
    var ok = regexp.test(studentPrice);
    if (!ok) {
        $("#studentPriceError").html("The Price Should Be A Valid Number");
        return false;
    }
    else {
        $("#studentPriceError").html("");
        return true;
    }
}


function validateChildPrice(childPrice) { //CHILDPRICE
    var regexp = /^\d+(,\d{1,2})?$/;
    var ok = regexp.test(childPrice);
    if (!ok) {
        $("#childPriceError").html("The Price Should Be A Valid Number");
        return false;
    }
    else {
        $("#childPriceError").html("");
        return true;
    }
}


function validateSeniorPrice(seniorPrice) {
    var regexp = /^\d+(,\d{1,2})?$/;
    var ok = regexp.test(seniorPrice);
    if (!ok) {
        $("#seniorPriceError").html("The Price Should Be A Valid Number");
        return false;
    }
    else {
        $("#seniorPriceError").html("");
        return true;
    }
}


function validatePinKode(pinKode) {
    var regexp = /^(\d{3}|\d{6})$/;
    var ok = regexp.test(pinKode);
    if (!ok) {
        $("#pinKodeError").html("Pin Code Shloud be Valid");
        return false;
    }
    else {
        $("#pinKodeError").html("");
        return true;
    }
}

function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå minimum 6 tegn, minst en bokstav og et tall");
        return false;
    }
    else {
        $("#feilPassord").html("");
        return true;
    }
}




