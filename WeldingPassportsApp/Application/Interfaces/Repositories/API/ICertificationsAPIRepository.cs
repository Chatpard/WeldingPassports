using Application.SQLModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.API
{
    public interface ICertificationsAPIRepository
    {
        Task<GetGetRegistrationTypesFromPEPassportReponse> GetGetRegistrationTypesFromPEPassportSelectList(int pePassportID);
    }
}
