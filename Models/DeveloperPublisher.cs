namespace EVideoGameStoreApp.Models
{
    public class DeveloperPublisher
    {

        // Join Table: DeveloperPublisher.cs
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
