 namespace Coodesh.Models
 {
 public class AccessHistory
    {
        public int Id { get; set; }
        public User Who { get; set; }
        public DateTime AccessedWhen { get; set; }
    }
 }