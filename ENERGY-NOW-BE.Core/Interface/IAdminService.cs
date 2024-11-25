using ENERGY_NOW_BE.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Interface
{
    public interface IAdminService
    {
        Task<string> ClientConfirmation(ConfirmClient confirmClient);

        Task<List<AdminClientsListResponse>> GetAdminClientList();
    }
}
