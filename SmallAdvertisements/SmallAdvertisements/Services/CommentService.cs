namespace SmallAdvertisements.Services
{
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
            var currentComment = _data.Comments.FirstOrDefault(c => c.Id == Id && c.Author.Id == userId);

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
            var currentComment = _data.Comments.FirstOrDefault(x => x.Id == model.Id);

            if (currentComment == null)
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

        public ICollection<CommentOutputModel> GetCommentsByUser(string userId)
        {
            var userComments = _data.Comments
              .Where(c => c.Author.Id == userId)
                .Select(c => new CommentOutputModel
                {
                    Id = c.Id,
                    Body = c.Body,
                    AdvertisementId = c.AdvertisementId,
                    Date = c.Date

                })
                .OrderByDescending(c=>c.Date)
                .ToList();


            return userComments;

        }
    }
}
