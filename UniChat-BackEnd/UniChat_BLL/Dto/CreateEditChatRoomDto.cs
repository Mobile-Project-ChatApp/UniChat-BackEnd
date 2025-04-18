namespace UniChat_BLL.Dto
{
    public class CreateEditChatRoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CreateEditChatRoomSemesterDto> ChatRoomSemesters { get; set; }
        public List<CreateEditChatRoomStudyDto> ChatRoomStudies { get; set; }
    }
}
