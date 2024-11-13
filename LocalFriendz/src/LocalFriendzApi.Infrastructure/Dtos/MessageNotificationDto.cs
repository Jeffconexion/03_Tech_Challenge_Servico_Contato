namespace LocalFriendzApi.Infrastructure.Dtos
{
    public class MessageNotificationDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public string? FeedbackMessage { get; set; }
    }
}
