namespace CustomerTest.Presentation.Client
{
    public class ErrorModel
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public Dictionary<string, string> errors { get; set; }

    }

}
