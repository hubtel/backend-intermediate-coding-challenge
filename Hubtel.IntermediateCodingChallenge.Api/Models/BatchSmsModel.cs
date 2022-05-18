namespace Hubtel.IntermediateCodingChallenge.Api.Models
{
    public class BatchSmsModel
    {
        public string From { get; set; }
        public string Content { get; set; }
        public string[] Contacts { get; set; }
    }
}