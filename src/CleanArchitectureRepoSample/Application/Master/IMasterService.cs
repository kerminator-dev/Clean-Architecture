using Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Master
{
    public interface IMasterService
    {
        Task<List<AppSetting>> GetAppSettingsAsync();
        Task<AppSetting> GetAppSettingByIdAsync(int id);
        Task<AppSetting> UpsertAsync(AppSetting appSetting);
        Task<bool> DeleteAsync(int id);
    }
}
