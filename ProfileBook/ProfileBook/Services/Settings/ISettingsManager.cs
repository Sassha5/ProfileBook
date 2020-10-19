namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int AuthorizedUserID { get; set; }
        int SortingType { get; set; }
        int Theme { get; set; }
        string Language { get; set; }
    }
}
