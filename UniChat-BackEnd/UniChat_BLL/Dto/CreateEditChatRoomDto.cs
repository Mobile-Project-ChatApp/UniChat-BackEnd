namespace UniChat_BLL.Dto
{
    public class CreateEditChatRoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ChatRoomSemesterDto> ChatRoomSemesters { get; set; }
        public List<ChatRoomStudyDto> ChatRoomStudies { get; set; }
    }
}
