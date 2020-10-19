using ProfileBook.Enums;

namespace ProfileBook
{
    public static class Constants
    {
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 16;
        public const int MinLoginLength = 4;
        public const int MaxLoginLength = 16;
        public const string DATABASE_NAME = "users.db";
        public const int DefaultSorting = (int)Sorting.Date;
        public const int NoAuthorizedUser = -1;
        public const string DefaultProfileImage = "pic_profile.png";
        public const string DefaultLanguage = "en";
    }
}
