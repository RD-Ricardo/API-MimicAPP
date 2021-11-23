using System.Collections.Generic;
using MimicApp_Api.Dto;

namespace MimicApp_Api.Helpers
{
    public class PaginationList<T>
    {
        public List<T> Result { get; set; } =  new List<T>();
        public Paginacao Paginacao { get; set; }
        public List<LInkDTO> LInks { get; set; } = new List<LInkDTO>();
    }
}