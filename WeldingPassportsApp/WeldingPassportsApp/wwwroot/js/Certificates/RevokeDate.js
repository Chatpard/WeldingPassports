export function ToggleDisable(revokeDate, revokeDateError, revokedByCompanyContactID) {
    if (revokedByCompanyContactID.val() == null) {

        revokeDate.rules("add",{ required: false });
        revokeDate.removeAttr("data-val");

        revokeDateError.remove();
        revokeDate.prop("readonly", true);
    }
    else {
        revokeDate.rules("add", { required: true });
        revokeDate.attr("data-val", true);

        revokeDate.removeAttr("readonly");
    }
}