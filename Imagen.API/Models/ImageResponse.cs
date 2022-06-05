namespace Imagen.API.Models
{
    public class ImageResponse
    {
        public List<SingleImage> CodedImages { get; set; }
    }

    public class SingleImage
    {
        public string ImageId { get; set; }
        public byte[] Base64 { get; set; }
    }
    
}
