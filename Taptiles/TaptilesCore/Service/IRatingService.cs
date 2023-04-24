using Taptiles.Entity;

namespace Taptiles.Service
{
    public interface IRatingService
    {
        void AddRating(Rating rating);

        IList<Rating> GetLastRating();

        IList<Rating> GetAllRating();

        void DeleteRating();
    }
}
