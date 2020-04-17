namespace DOT_NET_CORE_WEBAPI_SQLITE.DTO.products
{
    public class AddProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId {get; set;}
    }
}