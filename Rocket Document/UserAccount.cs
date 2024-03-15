using System;

//Current User Context
public class ActiveAccount
{
    private static string Password; //= "FDQfRbV33E8GxJIl"; //Remove Default
    public static string password
    {
        get { return Password; }
        set
        {
            Password = value;
        }
    }
    private static string Username; //= "TestUser1"; //Remove Default
    public static string username
    {
        get { return Username; }
        set
        {
            Username = value;
        }
    }
    private static string Repository; //= "RocketTest"; //Remove Default
    public static string repository
    {
        get { return Repository; }
        set
        {
            Repository = value;
        }
    }
    private static string FirstName; //= "Denver"; //Remove Default
    public static string firstName
    {
        get { return FirstName; }
        set
        {
            FirstName = value;
        }
    }
    private static string LastName; //= "The Dogo"; //Remove Default
    public static string lastName
    {
        get { return LastName; }
        set
        {
            LastName = value;
        }
    }

}
