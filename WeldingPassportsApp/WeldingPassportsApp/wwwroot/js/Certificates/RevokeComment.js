export function ToggleDisable(revokeComment, revokeCommentError, revokedByCompanyContactID) {
    if (revokedByCompanyContactID.val() == null) {
        revokeComment.rules("add", { required: false });
        revokeComment.removeAttr("data-val");

        revokeComment.rules("add", { messages: { required: false } });
        revokeComment.removeAttr("data-val-required");

        revokeCommentError.remove();
        revokeComment.prop("disabled", true);
    }
    else {
        revokeComment.rules("add", { required: true });
        revokeComment.attr("data-val", true);

        revokeComment.rules("add", { messages: { required: "My message" } });
        revokeComment.attr("data-val-required", "My message");

        revokeComment.removeAttr("disabled");
    }
}