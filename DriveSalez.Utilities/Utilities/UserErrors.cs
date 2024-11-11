namespace DriveSalez.Utilities.Utilities;

public static class UserErrors
{
    public static readonly Error NotFound = new Error(
        "Users.NotFound", "User not found.");

    public static readonly Error InvalidUserId = new Error(
        "Users.InvalidUserId", "The provided user ID is invalid.");

    public static readonly Error EmailAlreadyExists = new Error(
        "Users.EmailAlreadyExists", "A user with the given email already exists.");

    public static readonly Error UnauthorizedAction = new Error(
        "Users.UnauthorizedAction", "You are not authorized to perform this action.");
}