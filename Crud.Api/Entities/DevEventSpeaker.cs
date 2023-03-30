namespace Crud.Api.Entities
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescriptions { get; set; }
        public string LinkedInProfile { get; set; }
        public Guid DevEventId { get; set; }

    }
}
