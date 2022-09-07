using Coodesh.Models;

namespace Coodesh.ViewModels.Accounts
{
    public class RegisterAccessHistoriesViewModel
    {
        
        public User User { get; private set; }
        public Word Word { get; private set; }

        public RegisterAccessHistoriesViewModel(Word word)
        {
            Word = word;
        }

        public RegisterAccessHistoriesViewModel(User user)
        {
            User = user;
        }
        
         public RegisterAccessHistoriesViewModel(User user, Word word)
        {
            User = user;
            Word = word;
        }
    }
}