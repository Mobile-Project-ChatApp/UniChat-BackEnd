namespace UniChat_BLL.Dto
{
    public class ChatRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SemesterDto> Semesters { get; set; }
        public List<StudyDto> Studies { get; set; }
        public List<MessageDto> Messages { get; set; }
        public List<UserDto> Members { get; set; }
    }

}
