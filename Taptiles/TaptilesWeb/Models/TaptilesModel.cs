using Taptiles.Core;
using Taptiles.Entity;

namespace TaptilesWeb.Models
{
    public class TaptilesModel
    {
        public Field Field { get; set; }

        public IList<Score> Scores { get; set; }

        public IList<Coment> Coments { get; set; }

        public IList<Rating> Ratings { get; set; }

    }
}
