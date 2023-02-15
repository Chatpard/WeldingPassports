function DeleteRevokeByEncryptedID(encryptedID) {
    $.ajax({
        type: "GET",
        url: "https://localhost:44315/Api/CertificatesApi/DeleteRevokeByEncryptedID",
        data: { "encryptedID": encryptedID },
        success: function () {
            console.log("succes");
            $("#CurrentCertificateRevokeDate").val('');
            $("#CurrentCertificateRevokedByCompanyContactID").val('');
            $("#CurrentCertificateRevokeComment").val('');
        },
        error: function () { console.log("error"); }
    });
}