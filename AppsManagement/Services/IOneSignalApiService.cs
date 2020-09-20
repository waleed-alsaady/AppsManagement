using AppsManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppsManagement.Services
{
    public interface IOneSignalApiService
    {
        public Task<IEnumerable<AppModel>> GetAllAsync();
        public Task<AppModel> GetAsync(string id);
        public Task<AppModel> CreateApp(AppModel appModel);
        public Task<AppModel> UpdateApp(AppModel appModel);
    }
}
