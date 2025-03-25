namespace UniChat_BLL.Dto
{
    public class CreateEditChatRoomDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Members { get; set; }
    }
}
