namespace SmallAdvertisements.Services
{
    using Microsoft.EntityFrameworkCore;
    using SmallAdvertisements.Data.Context;
    using SmallAdvertisements.Data.Entities;
    using SmallAdvertisements.Models.ServiceModels.Comment.Input;
    using SmallAdvertisements.Models.ServiceModels.Comment.Output;
    using SmallAdvertisements.Services.Contracts;

    public class CommentService : ICommentService
    {
        private readonly AdvertisementsDbContext _data;

        public CommentService(AdvertisementsDbContext data)
        {
            _data = data;
        }
        public bool Add(AddCommentInputModel model)
        {
            if (!_data.Advertisements.Any(x => x.Id == model.AdvertisementId))
            {
                return false;
            }

            var comment = new Comment
            {
                Body = model.Body,
                Author = model.Author,
                Date = DateTime.Now,
                AdvertisementId = model.AdvertisementId,
            };

            try
            {
                _data.Comments.Add(comment);
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
            var currentComment = _data.Comments.Where(c => c.Id == Id && c.Author.Id == userId).Include(x => x.Author).FirstOrDefault();

            if (currentComment == null)
            {
                return false;
            }

            try
            {
                _data.Comments.Remove(currentComment);
                _data.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public bool Edit(EditCommentInputModel model)
        {
            var currentComment = _data.Comments.Where(x => x.Id == model.Id).Include(x => x.Author).FirstOrDefault();

            if (currentComment == null || currentComment.Author.Id != model.Editor.Id)
            {
                return false;
            }

            currentComment.Body = model.Body;

            try
            {
                _data.Comments.Update(currentComment);
                _data.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            return true;


        }

        public CommentOutputModel GetById(int commentId)
        {
            var comment = _data.Comments.Where(x => x.Id == commentId)
                .Select(x => new CommentOutputModel()
                {
                    AdvertisementId = x.AdvertisementId,
                    Author = x.Author,
                    Body = x.Body,
                    Id = x.Id,
                    AdvertisementTitle = x.Advertisement.Title
                })
                .FirstOrDefault();

            return comment;
        }

        public ICollection<CommentOutputModel> GetCommentsByAdverisement(int advertisementId)
        {
            var comments = _data.Comments.Where(x => x.AdvertisementId == advertisementId).Select(x => new CommentOutputModel
            {
                AdvertisementId = x.AdvertisementId,
                Id = x.Id,
                Author= x.Author,
                Body = x.Body,
                Date = x.Date
            }).ToList();

            return comments;
        }

        public ICollection<CommentOutputModel> GetCommentsByUser(string userId)
        {
            var userComments = _data.Comments
              .Where(c => c.Author.Id == userId)
                .Select(c => new CommentOutputModel
                {
                    Id = c.Id,
                    Body = c.Body,
                    AdvertisementId = c.AdvertisementId,
                    Date = c.Date,
                    Author = c.Author,
                    AdvertisementTitle = c.Advertisement.Title
                })
                .OrderByDescending(c=>c.Date)
                .ToList();


            return userComments;

        }
    }
}
