namespace Hubtel.IntermediateCodingChallenge.Api.Models
{
    public class SubmitSmsRequest
    {
        public string From { get; set; }
        public string Content { get; set; }
        public string To { get; set; }
        public string MessageId { get; set; }
        public string BatchId { get; set; }
    }
}