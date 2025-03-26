namespace UniChat_BLL.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public UserDto Sender { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }

}
