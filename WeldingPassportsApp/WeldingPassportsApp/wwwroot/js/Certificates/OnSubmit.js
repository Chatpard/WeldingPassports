window.OnSubmit = () => {
    var pePassportID = $("#PEPassportID");
    var processID = $("#ProcessID");
    var modalDialog = $("#modalDialog");
    var form = $("#form");
    if (pePassportID.val() != null && processID.val() != null) {
        $.ajax({
            type: "GET",
            url: "../../api/CertificatesApi/GetHasNotSet",
            data: {
                pePassportID: pePassportID.val(),
                processID: processID.val()
            },
            success: function (response) {
                if (response) {
                    modalDialog.modal('show');
                }
                else {
                    form.submit();
                }
            },
            error: function (jqXHR, testStatus, errorThrown) {
                console.log(errorThrown);
            }
        })
    }
    else {
        form.submit();
    }
};