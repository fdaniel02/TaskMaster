namespace UI.Tasks.Constants
{
    public static class NotificationMessages
    {
        public const string SaveSuccess = "Successfully saved!";

        public const string UnknownError = "Unknown error.";

        public const string SaveProjectFirst = "Please save the Project first.";

        public const string ActionItemCannotBeAdded = "Action item cannot be added!";

        public const string ActionItemSaveErrorMissingProject = ActionItemCannotBeAdded + " " + SaveProjectFirst;

        public const string ActionItemSaveErrorUnknown = ActionItemCannotBeAdded + " " + UnknownError;
    }
}