namespace SharpTranslate.Models
{
    public class StatsModel
    {
        public string? Version { get; set; }

        public int RequestsCount { get; set; }

        public List<int>? RequestsCodes { get; set; }

        public DateTime StartTime { get; set; }

    }

}
