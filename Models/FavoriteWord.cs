 namespace Coodesh.Models
 {
 public class FavoriteWord
    {
        public int Id { get; set; }
        public DateTime When { get; set; }
        public Word Word { get; set; }
        public User Who { get; set; }
    }
 }