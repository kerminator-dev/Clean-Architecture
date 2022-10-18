using Application.Common.Interface;
using Domain.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Master
{
    internal class MasterService : IMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> DeleteAsync(int id)
        {
             return _unitOfWork.AppSettingRepository.Delete(id);
        }

        public Task<AppSetting> GetAppSettingByIdAsync(int id)
        {

             return _unitOfWork.AppSettingRepository.Get(id);
        }

        public async Task<List<AppSetting>> GetAppSettingsAsync()
        {
             return await _unitOfWork.AppSettingRepository
                                    .TableNoTracking
                                    .OrderBy(t => t.ReferenceKey)
                                    .ToListAsync();
        }

        public async Task<AppSetting> UpsertAsync(AppSetting appSetting)
        {
            try
            {
                if (appSetting.Id > 0)
                    _unitOfWork.AppSettingRepository.Update(appSetting);
                else
                    _unitOfWork.AppSettingRepository.Add(appSetting);

                await _unitOfWork.SaveAsync();

                return appSetting;

            }
            catch (Exception)
            {
                return appSetting;
            }
        }
    }
}
