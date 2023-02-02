namespace SmallAdvertisements.Services
{
    using Microsoft.EntityFrameworkCore;
    using SmallAdvertisements.Data.Context;
    using SmallAdvertisements.Data.Entities;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Input;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;
    using SmallAdvertisements.Models.ServiceModels.Comment.Output;
    using SmallAdvertisements.Models.ServiceModels.Like.Output;
    using SmallAdvertisements.Services.Contracts;

    public class AdvertisementService : IAdvertisementService
    {
        private readonly AdvertisementsDbContext _data;

        public AdvertisementService(AdvertisementsDbContext data)
        {
            _data = data;
        }

        public bool Add(AddAdvertisementInputModel model)
        {

            var advertiesement = new Advertisement()
            {
                Body = model.Body,
                Date = DateTime.Now,
                Author = model.Author,
                Title = model.Title
            };

            try
            {
                _data.Advertisements.Add(advertiesement);
                _data.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int Id, string userId)
        {
            var itemToDelete = _data.Advertisements.FirstOrDefault(x => x.Id == Id);

            if (itemToDelete == null || itemToDelete.Author.Id != userId)
            {
                return false;
            }

            try
            {
                _data.Advertisements.Remove(itemToDelete);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Edit(EditAdvertisementInputModel model)
        {
            var advertisement = _data.Advertisements.FirstOrDefault(x => x.Id == model.Id);

            if (advertisement == null || advertisement.Author.Id != model.Editor.Id)
            {
                return false;
            }


            advertisement.Body = model.Body;
            advertisement.Title = model.Title;

            try
            {
                _data.Advertisements.Update(advertisement);
                _data.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ICollection<AdvertisementOutputModel> GetAll()
        {

            var allAdvertisements = _data.Advertisements.Select(a => new AdvertisementOutputModel
            {
                Id = a.Id,
                Author = a.Author,
                Body = a.Body,
                Date = a.Date,
                Title = a.Title,
                Comments = a.Comments.Select(c => new CommentOutputModel
                {
                    Id = c.Id,
                    AdvertisementId = c.Id,
                    Body = c.Body,
                    Author = c.Author,
                    Date = c.Date,
                    //AdvertisementTitle = a.Title

                }).ToList(),
                Likes = a.Likes.Select(l => new LikeOutputModel
                {
                    Id = l.Id,
                    AdvertisementId = l.AdvertisementId,
                    Author = l.Author,

                }).ToList()

            }).OrderByDescending(c => c.Date)
                .ToList();

            return allAdvertisements;

        }

        public AdvertisementOutputModel GetById(int Id)
        {
            var advertisement = _data.Advertisements.Where(x => x.Id == Id).Include(x => x.Author).Include(x => x.Likes).ThenInclude(x => x.Author).Include(x => x.Comments).ThenInclude(x => x.Author).FirstOrDefault();

            if (advertisement == null)
            {
                return null;
            }

            var advertisementOutputModel = new AdvertisementOutputModel
            {
                Id = advertisement.Id,
                Author = advertisement.Author,
                Body = advertisement.Body,
                Date = advertisement.Date,
                Title = advertisement.Title,
                Comments = advertisement.Comments.Select(c => new CommentOutputModel
                {
                    Id = c.Id,
                    AdvertisementId = c.Id,
                    Body = c.Body,
                    Author = c.Author,
                    Date = c.Date,
                    //AdvertisementTitle = advertisement.Title

                }).ToList(),
                Likes = advertisement.Likes.Select(l => new LikeOutputModel
                {
                    Id = l.Id,
                    AdvertisementId = l.AdvertisementId,
                    Author = l.Author,

                }).ToList()
            };

            return advertisementOutputModel;

        }

        public ICollection<AdvertisementOutputModel> GetByUser(string UserId)
        {
            var advertisements = _data.Advertisements.Where(x => x.Author.Id == UserId).Select(ad => new AdvertisementOutputModel()
            {
                Id = ad.Id,
                Author = ad.Author,
                Body = ad.Body,
                Date = ad.Date,
                Title = ad.Title,
                Comments = ad.Comments.Select(c => new CommentOutputModel
                {
                    Id = c.Id,
                    AdvertisementId = c.Id,
                    Body = c.Body,
                    Author = c.Author,
                    Date = c.Date,
                    //AdvertisementTitle = ad.Title

                }).ToList(),
                Likes = ad.Likes.Select(l => new LikeOutputModel
                {
                    Id = l.Id,
                    AdvertisementId = l.AdvertisementId,
                    Author = l.Author,

                }).ToList()
            }).OrderByDescending(c => c.Date)
                  .ToList();

            return advertisements;
        }
    }
}
