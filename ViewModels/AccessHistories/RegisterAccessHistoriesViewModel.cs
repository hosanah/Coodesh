using Coodesh.Models;

namespace Coodesh.ViewModels.Accounts
{
    public class RegisterAccessHistoriesViewModel
    {
        
        public User User { get; private set; }

        public RegisterAccessHistoriesViewModel(User user)
        {
            User = user;
        }

    }
}