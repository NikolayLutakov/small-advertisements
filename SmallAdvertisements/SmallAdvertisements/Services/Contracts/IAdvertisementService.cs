namespace SmallAdvertisements.Services.Contracts
{
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Input;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;

    public interface IAdvertisementService
    {
        bool Add(AddAdvertisementInputModel model);

        bool Delete(int Id, string userId);

        bool Edit(EditAdvertisementInputModel model);

        AdvertisementOutputModel GetById(int Id);

        AdvertisementOutputModel GetByUser(string UserId);

        ICollection<AdvertisementOutputModel> GetAll();

    }
}
