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
        public const string Create = "Role.Create";
        public const string Update = "Role.Update";
        public const string Delete = "Role.Delete";
        public const string GetAllPermissions = "GetAllPermissions";



        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            GetAllPermissions
        };
    }
    public static class Account
    {
        public const string AddRole = "Account.AddRole";
        public const string Create = "Account.Create";
        public const string Update = "Account.Update";
        public const string Delete = "Account.Delete";




        public static List<string> All => new List<string>
        {
            AddRole,
            Create,
            Update,
            Delete
        };
    }
    public static class Product
    {
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
        public const string AddProductImage = "Product.AddImage";
        public const string DeleteImage = "Product.DeleteImage";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            AddProductImage,
            DeleteImage
        };
    }
    public static class Order
    {
        public const string Create = "Order.Create";
        public const string GetAll = "Order.GetAll";
        public const string Update = "Order.Update";
        public const string Delete = "Order.Delete";
        public const string View = "Order.View";
        public const string GetMy = "Order.GetMy";
        public const string GetDetail = "Order.GetDetail";
        public const string GetMySales = "Order.GetMySales";
        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            View,
            GetMy,
            GetDetail,
            GetMySales
        };
    }
    public static class OrderProduct
    {
        public const string Create = "Permissions.OrderProduct.Create";
        public const string Get = "Permissions.OrderProduct.Get";
        public const string Update = "Permissions.OrderProduct.Update";
        public const string Delete = "Permissions.OrderProduct.Delete";
        public static List<string> All => new List<string>
        {
            Create,
            Get,
            Update,
            Delete
        };
    }
}
