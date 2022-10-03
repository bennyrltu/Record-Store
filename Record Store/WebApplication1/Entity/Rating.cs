namespace Record_Store.Entity
{
    public class Rating
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public int GivenRating { get; set; }
        public DateTime RatingDate { get; set; }
        public DateTime RatingExpirationDate { get; set; }
        public Recording Recording { get; set; }


    }
}
