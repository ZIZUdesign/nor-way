
    function validateTrip() {
            const tripDateOK = validateTripDate($("#tripDate").val());
            const tripTimeOK = validateTripTime($("#tripTime").val());
            const departureOK = validateDeparture($("#departure").val());
            const arrivalOK = validateArrival($("#arrival").val());
            const adultPriceOK = validateAdultPrice($("#adultPrice").val());
            const studentPriceOK = validateStudentPrice($("#studentPrice").val());
            const childPriceOK = validateChildPrice($("#childPrice").val());
            const seniorPriceOK = validateSeniorPrice($("#seniorPrice").val());
            if (tripDateOK && tripTimeOK && departureOK && arrivalOK && adultPriceOK && studentPriceOK && childPriceOK && seniorPriceOK) {
        SaveTrip();
            }
        }

        function SaveTrip() {
            const trip = {
                tripDate: $("#tripDate").val(),
                tripTime: $("#tripTime").val(),
                departure: $("#departure").val(),
                arrival: $("#arrival").val(),
                adultPrice: $("#adultPrice").val(),
                studentPrice: $("#studentPrice").val(),
                youthPrice: $("#childPrice").val(), 
                seniorPrice: $("#seniorPrice").val()
            }
            const url = "Save";
            $.post(url, trip, function (OK) {
                if (OK) {
            window.location.href = '../trip/tripIndex.html';
                }
                else {
            $("#feil").html("Feil i db - prøv igjen senere");
                }
            });
        };