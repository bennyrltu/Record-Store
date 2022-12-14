using Record_Store.Auth;
using System.ComponentModel.DataAnnotations;

namespace Record_Store.Entity
{
    public class Rating
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public int GivenRating { get; set; }
        public DateTime RatingDate { get; set; } = DateTime.UtcNow;
        public DateTime RatingExpirationDate { get; set; } = DateTime.UtcNow.AddMonths(2);
        public uint RecordingID { get; set; }
        public Recording Recording { get; set; }


    }
}
