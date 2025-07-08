namespace E_Ticaret.Application.Shared;

public static class Permissions
{
    public static class Category
    {
        public const string Create = "Category.Create";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";
        public const string Get = "Category.Get";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            Get
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
        public const string Get = "Product.Get";
        public const string AddProductImage = "Product.AddImage";
        public const string DeleteImage = "Product.DeleteImage";

        public static List<string> All => new List<string>
        {
            Create,
            Update,
            Delete,
            Get,
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
        public const string Get = "Order.Get";
        public static List<string> All => new List<string>
        {
            Create,
            GetAll,
            Update,
            Delete,
            Get
        };
    }
    public static class OrderProduct
    {
        public const string Create = "OrderProduct.Create";
        public const string Get = "OrderProduct.Get";
        public const string Update = "OrderProduct.Update";
        public const string Delete = "OrderProduct.Delete";
        public static List<string> All => new List<string>
        {
            Create,
            Get,
            Update,
            Delete
        };
    }
    public static class Favourite
    {
        public const string Create = "Favourite.Create";
        public const string Get = "Favourite.Get";
        public const string Update = "Favourite.Update";
        public const string Delete = "Favourite.Delete";
        public static List<string> All => new List<string>
        {
            Create,
            Get,
            Update,
            Delete
        };
    }
}
