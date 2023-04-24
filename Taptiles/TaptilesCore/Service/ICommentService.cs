using Taptiles.Entity;

namespace Taptiles.Service
{
    public interface ICommentService
    {
        void AddComment(Coment coment);

        IList<Coment> GetLastComment();

        IList<Coment> GetAllComment();

        void DeleteComment();
    }
}
