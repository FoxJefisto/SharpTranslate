namespace SharpTranslate.Middlewares.Models
{
    public class RequestTracker
    {
        public int HandledRequests { get; set; }

        public List<int>? HandledCodes { get; set; } = new List<int>();
    }
}
