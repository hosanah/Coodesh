 namespace Coodesh.Models
 {
 public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<AccessHistory> AccessHistories { get; set; }
        public List<FavoriteWord> FavoriteWords { get; set; }
    }
 }