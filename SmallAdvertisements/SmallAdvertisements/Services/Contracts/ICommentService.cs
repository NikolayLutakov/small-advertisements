namespace SmallAdvertisements.Services.Contracts
{
    using SmallAdvertisements.Models.ServiceModels.Comment.Input;
    using SmallAdvertisements.Models.ServiceModels.Comment.Output;

    public interface ICommentService
    {
        bool Add(AddCommentInputModel model);

        bool Delete(int Id,string userId);

        bool Edit(EditCommentInputModel model);

        ICollection<CommentOutputModel> GetCommentsByUser(string userId);
    }
}
