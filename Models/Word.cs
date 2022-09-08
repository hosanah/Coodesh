 namespace Coodesh.Models
 {
 public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FavoriteWord> FavoriteWords { get; set; }
    }
 }