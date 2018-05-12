using AGL.Dto;
using AGL.Dto.ViewDto;
using AGL.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGL.BusinessLogic
{
    public interface IPeopleBusinessLogic
    {
        Task<Response<List<GenderViewDto>>> GetPeople();
    }
}
