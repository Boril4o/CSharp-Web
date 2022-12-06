namespace Library.Data
{
    public class DataConstants
    {
        public class Category
        {
            public const int MaxNameLength = 50;
            public const int MinNameLength = 5;
        }

        public class Book
        {
            public const int MaxTitleLength = 50;
            public const int MinTitleLength = 10;

            public const int MaxAuthorLength = 50;
            public const int MinAuthorLength = 5;

            public const int MaxDescriptionLength = 5000;
            public const int MinDescriptionLength = 5;

            public const string MaxRating = "10.00";
            public const string MinRating = "0.00";
        }
    }
}
