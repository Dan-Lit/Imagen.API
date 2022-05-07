using Imagen.DAL.Models;
namespace Imagen.API.Models
{
    public class AssignTagsRequest
    {
        /// <summary>
        /// Recibe el id de la imagen en el primer string, y el id del tag en el segundo. 
        /// </summary>
        public List<KeyValuePair<string, string>> Tags { get; set; }

        //public Dictionary<string, string> tags { get; set; }

    }
}
