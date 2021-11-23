namespace MimicApp_Api.Dto
{
    public class LInkDTO
    {
        public LInkDTO(string rel , string href , string method)
        {
            Rel = rel;
            Href = href;
            Method =  method;
        }
        public string Rel { get; set; }
        public string  Href { get; set; }
        public string  Method { get; set; }
    }
}