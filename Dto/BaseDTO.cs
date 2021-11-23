using System.Collections.Generic;

namespace MimicApp_Api.Dto
{
    public abstract class BaseDTO
    {
        public List<LInkDTO> Link {get; set;} = new List<LInkDTO>();
    }
}