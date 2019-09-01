namespace Domain.Models
{
    public class BackgroundSettingModel : BaseDbModel
    {
        public string Type { get; set; }

        public int Hour { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }
    }
}
