namespace E_Ticaret.Application.Shared;

public static class Permissions
{
    public static class Category
    {
        public const string Create = "Category.Create";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";
        public const string View = "Category.View";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            View
        };
    }
    public static class Role
    {
        public const string GetAllPermissions = "GetAllPermissions";



        public static List<string> All => new List<string>
        {
            GetAllPermissions
        };
    }
    public static class Account
    {
        public const string AddRole = "Account.AddRole";



        public static List<string> All => new List<string>
        {
            AddRole
        };
    }
    public static class Product
    {
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
        public const string UploadImage = "Product.UploadImage";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            UploadImage
        };
    }
    public static class Order
    {
        public const string Create = "Order.Create";
        public const string Update = "Order.Update";
        public const string Delete = "Order.Delete";
        public const string View = "Order.View";
        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            View
        };
    }
}
