export function ToggleDisable(revoke, hasPassed) {
    if (hasPassed.val() == "True") {
        revoke.removeAttr("style");
    }
    else
    {
        revoke.prop("style", "display: none");
    }
}