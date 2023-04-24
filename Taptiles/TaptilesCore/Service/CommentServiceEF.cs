using Taptiles.Entity;
using Microsoft.EntityFrameworkCore;

namespace Taptiles.Service
{
    public class CommentServiceEF : ICommentService
    {
        public void AddComment(Coment coment)
        {
            using (var context = new TaptilesDbContext())
            {
                context.Coments.Add(coment);
                context.SaveChanges();
            }
        }

        public IList<Coment> GetLastComment()
        {
            using (var context = new TaptilesDbContext())
            {
                return (from s in context.Coments orderby s.Comment descending select s).Take(3).ToList();
            }
        }

        public IList<Coment> GetAllComment()
        {
            using (var context = new TaptilesDbContext())
            {
                return (from s in context.Coments orderby s.Comment descending select s).ToList();
            }
        }

        public void DeleteComment()
        {
            using (var context = new TaptilesDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Comments");
            }
        }
    }
}
