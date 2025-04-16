namespace EVideoGameStoreApp.Models
{
    public class VideoGamePlatform
    {
        // Join Table: VideoGamePlatform.cs
        public int VideoGameId { get; set; }
        public VideoGame VideoGame { get; set; }

        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
