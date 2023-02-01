namespace SmallAdvertisements.Models.ViewModels.Home
{
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;

    public class IndexViewModel
    {
        public ICollection<AdvertisementOutputModel> Advertisements { get; set; }

    }
}
