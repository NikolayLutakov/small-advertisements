using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;

namespace SmallAdvertisements.Models.ViewModels.Advertisement
{
    public class MyAdvertisementsViewModel
    {
        public ICollection<AdvertisementOutputModel> MyAdvertisements { get; set; }
    }
}
