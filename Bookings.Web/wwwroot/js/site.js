function ajaxFormPost(url, formData, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        beforeSend: function (xhr) {  
            xhr.setRequestHeader("XSRF-TOKEN",  
                $('input:hidden[name="__RequestVerificationToken"]').val());  
        },
        data: formData,
        processData: false,
        contentType: false,
        success: function(response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function(xhr, status, error) {
            if (errorCallback) {
                errorCallback(xhr, status, error);
            }
        }
    });
}

function sendAjaxRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: successCallback,
        error: errorCallback
    });
}
  
function ajaxGet(url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: successCallback,
        error: errorCallback
    });
}

 /**
    * Function to submit a form using jQuery AJAX when a checkbox is checked.
    *
    * @param {string} formId - The ID of the form to be submitted.
    * @param {string} selector - The selector of the checkbox that triggers the form submission.
    * @param {string} url - The URL to which the form data should be submitted.
    * @param {function} successCallback - The callback function to be executed on successful form submission.
    * @param {function} errorCallback - The callback function to be executed if there is an error during form submission.
*/
 function submitFormOnCheckbox(formId, selector, url, successCallback, errorCallback) {
    // Attach a change event listener to the checkbox
    $(`${selector}`).change(function() {
        // Check if the checkbox is checked
        if ($(this).is(":checked")) {
            // Serialize the form data
            const formData = $(`#${formId}`).serialize();

            // Send an AJAX request to submit the form data
            $.ajax({
                type: "POST",
                url: url,
                data: formData,
                success: function(response) {
                    // Execute the success callback function
                    successCallback(response);
                },
                error: function(error) {
                    // Execute the error callback function
                    errorCallback(error);
                }
            });
        }
    });
}