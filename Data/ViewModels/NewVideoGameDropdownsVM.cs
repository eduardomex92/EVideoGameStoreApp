using EVideoGameStoreApp.Models;

namespace EVideoGameStoreApp.Data.ViewModels
{
    public class NewVideoGameDropdownsVM
    {
        public NewVideoGameDropdownsVM()
        {
            Developers = new List<Developer>();
            Publishers = new List<Publisher>();
            Platforms = new List<Platform>();
        }
        public List<Developer> Developers { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Platform> Platforms { get; set; }

    }
}
