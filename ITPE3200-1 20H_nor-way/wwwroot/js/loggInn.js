﻿function loggInn() {

    const brukernavnOK = validerBrukernavn($("#brukernavn").val());
    const passordOK = validerPassord($("#passord").val());

    if (brukernavnOK && passordOK) {
        const bruker = {
            brukernavn: $("#brukernavn").val(),
            passord: $("#passord").val()
        }
        
        $.post("Trip/LoggInn", bruker, function (OK) {
            if (OK) {
                window.location.href = '../trip/tripIndex.html';
            }
            else {
                $("#feil").html("Feil brukernavn eller passord");
            }
        })
            .fail(function (feil) {
                $("#feil").html("Feil på server - prøv igjen senere: " + feil.responseText + " : " + feil.status + " : " + feil.statusText);
            });
    }
}
