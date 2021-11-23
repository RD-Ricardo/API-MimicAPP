using System.Collections.Generic;
using System.Threading.Tasks;
using MimicApp_Api.Helpers;
using MimicApp_Api.Models;

namespace MimicApp_Api.Repository.Interfaces
{
    public interface IPalavraRepository
    {
        Task CreatePalavraAsync(Palavra model);
        Task UpdatePalavraAsync(Palavra model);
        Task RemovePalavraAsync(int id);
        Task<PaginationList<Palavra>> GetallPalavrasAsync(PalavraUrlQuery query);
        Task<Palavra> GetByIdPalavraAsync(int id);
    }
}