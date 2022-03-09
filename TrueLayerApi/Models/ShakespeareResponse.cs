namespace TrueLayerApi.Models
{
    public class ShakespeareResponse
    {
        public Success Success { get; set; }
        public Content Contents { get; set; }
        public Error Error { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class Success
    {
        public int Total { get; set; }
    }
    public class Content
    {
        public string Translated { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
    }
}
