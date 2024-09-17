namespace Server.Common.Enums
{
    public enum Permission
    {
        CreatePropertyDossier,
        ReadPropertyDossier,
        UpdatePropertyDossier,
        DeletePropertyDossier,

        CreateForm,
        ReadForm,
        UpdateForm,
        DeleteForm,

        CanViewAllForms,
        CanViewTenantForms,
        CanViewOwnForms,

        ReadAllNewResidentRequest,
        UpdateNewResidentRequest,

        CreateAccount,
        ReadAccount,
        UpdateAccount,
        DeleteAccount,

        AssignRole,

        CreateGroup,
        ReadGroup,
        UpdateGroup,
        DeleteGroup,
    }
}