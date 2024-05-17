namespace FinanceTracker.DAL.Resources
{
    public static class ValidationResources
    {
        public const string InvalidUserStatus = "Invalid UserStatusId.";
        public const string RequiredFieldValidatinMessage = "This field is required.";
        public const string EmailId = "EmailId";
        public const string EmailExistMessage = "Email Id already exists.";
        public const string ConnectionString = "ConnectionStrings";
        public const string Authority = "1Authority";
        public const string DbConnection = "DbConnection";
        public const string MailSettings = "MailSettings";
        public const string CorsPolicy = "CorsPolicy";
        public const string TokenURL = "1Authority:AuthorityTokenUrl";
        public const string ClientID = "1Authority:AuthorityClientId";
        public const string AuthoritySecret = "1Authority:AuthoritySecret";
        public const string BaseUri = "1Authority:baseUri";
        public const string BasePut = "1Authority:basePut";
        public const string DeprecatedMessage = "This Constructor is Deprecated";
        public const string Appsettings = "appsettings.json";
        public const string UserClientName = "FinanceTracker";
        public const string FirstNameRegexMessage ="Please enter a valid First Name.";
        public const string LastNameRegexMessage ="Please enter a valid Last Name.";
        public const string FirstNameLengthMessage ="First name must be between 2-30 characters.";
        public const string LastNameLengthMessage ="Last name must be between 2-30 characters.";
        public const string InvalidEmail ="Invalid email address.";
        public const string InvalidPhone ="Phone number is invalid";
        public const string EmailLength ="Email ID must be between 1-50 characters.";
        public const string InvalidPassword ="Please enter a valid Password.";
        public const string PasswordLength ="password must be between 8-50 characters.";
        public const string ServerError = "Internal server error";
        public const string  InvalidStatus = "Invalid user status";
        public const string InvalidUser = "Invalid user"; 
        public const string TokenGenerateError = "Error while Generate token"; 
        public const string  AlreadyUpdateStatus = "User already Approved/Rejected";
        public const string RegistrationSuccess = "User registered successfully.";
        public const string EmailAlreadyExist = "Conflict - Email already exists.";
        public const string AuthoritySuccess = "Successfully Added To Authority.";
        public const string PendingSuccess = "List of pending user registrations retrieved successfully.";
        public const string ApprovedSuccess = "List of approved user registrations retrieved successfully.";
        public const string RejectedSuccess = "List of rejected user registrations retrieved successfully.";
        public const string RolesSuccess = "List of roles retrieved successfully.";
        public const string StatusSuccess = "List of user statuses retrieved successfully.";
        public const string NoContent = "No Content - Request processed successfully but no content returned.";
        public const string InternalServer = "Internal Server Error - Something went wrong on the server side.";
        public const string BadRequest = "Bad Request - Invalid input data.";
        public const string NotFound = "Not Found - User not found.";
        public const string serverError = "Internal server error";
        public const string UserIs="User is ";
        public const string UserAre="User are ";
        public const string Approved="Approved.";
        public const string Rejected="Rejected.";
        public const string SelectedUser="Selected Users are ";

        // public const string RegistrationSuccess = "User registered successfully.";
        // public const string EmailAlreadyExist = "Conflict - Email already exists.";
        // public const string AuthoritySuccess = "Successfully Added To Authority.";
        // public const string InternalServer = "Internal Server Error - Something went wrong on the server side.";
    }
}