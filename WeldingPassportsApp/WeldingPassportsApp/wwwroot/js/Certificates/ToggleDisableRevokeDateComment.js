function ToggleDisableRevokeDateComment() {
    var disable = $("#CurrentCertificateRevokedByCompanyContactID").val() == null;
    $("#CurrentCertificateRevokeDate").prop("readonly", disable);
    $("#CurrentCertificateRevokeComment").prop("disabled", disable);
    if (disable) {
        $("#CurrentCertificateRevokeDate").val("");
        $("#CurrentCertificateRevokeDate").removeAttr("required");
        $("#CurrentCertificateRevokeDate-error").remove();
        $("#CurrentCertificateRevokeComment").val("");
        $("#CurrentCertificateRevokeComment").attr("required", false);
        $("#CurrentCertificateRevokeComment-error").remove();
    }
    else {
        $("#CurrentCertificateRevokeDate").attr("required", true);
        $("#CurrentCertificateRevokeComment").attr("required", true);
    }
}