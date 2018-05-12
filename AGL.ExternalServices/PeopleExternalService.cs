using AGL.Dto;
using AGL.Dto.ContractDto;
using AGL.Library;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.ExternalServices
{
    public class PeopleExternalService : BaseExternalService, IPeopleExternalService
    {
        private IConfiguration _configuration { get; }

        public PeopleExternalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response<List<PersonContractDto>>> GetPeople()
        {
            var response = new Response<List<PersonContractDto>>();

            var uri = new Uri(_configuration["PeopleUrl"]);

            var peopleResponse = await Get(uri);
            if (peopleResponse.ResponseStatus == ResponseStatusEnum.Failure)
            {
                response.Errors.AddRange(peopleResponse.Errors);
                return response;
            }
            else
            {
                try
                {
                    response.Data = JsonConvert.DeserializeObject<List<PersonContractDto>>(peopleResponse.Data);
                    if(response.Data == null || !response.Data.Any())
                    {
                        response.Errors.Add(ErrorMessages.CannotDeserializePeople_02);
                    }
                }
                catch (JsonException)
                {
                    response.Errors.Add(ErrorMessages.CannotDeserializePeople_02);
                }
            }
            
            return response;
        }
    }
}
