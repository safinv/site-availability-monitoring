namespace Domain.Models
{
    public class UserModel : BaseDbModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
