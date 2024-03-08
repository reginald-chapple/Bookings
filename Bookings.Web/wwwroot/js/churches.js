function loadDenominations(branch) {
    $('#DenominationId').empty();

    $.ajax({
        url: `/Churches/${branch}/GetDenominations`,
        success: (response) => {
            console.log(response);
            if (response != null && response != undefined && response.length > 0) {
                $('#DenominationId').attr('disabled', false);
                $('#DenominationId').append('<option value="-1">--Select Denomination--</option>');
                $.each(response, (i, data) => {
                    $('#DenominationId').append(`<option value="${data.id}">${data.name}</option>`);
                });
            }
            else {
                $('#DenominationId').attr('disabled', true);
                $('#DenominationId').append('<option value="-1">--No Denominations found--</option>');
            }
        },
        error: (error) => {
            alert(error);
        }
    });
}