using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Interfaces
{
    public interface IImageProfileManagement
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
        void DeleteImageAsync(string src);
    }
}
