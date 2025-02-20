namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string HasAtLeast20 = "HasAtLeast20";
    public const string CreatedAtLeast2Restaurants = "CreatedAtLeast2Restaurants";
}

public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}