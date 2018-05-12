using AGL.Dto;
using AGL.Dto.ContractDto;
using AGL.Dto.ViewDto;
using AGL.ExternalServices;
using AGL.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.BusinessLogic
{
    public class PeopleBusinessLogic: IPeopleBusinessLogic
    {
        private IPeopleExternalService _peopleExternalService { get; }

        public PeopleBusinessLogic(IPeopleExternalService peopleExternalService)
        {
            _peopleExternalService = peopleExternalService;
        }

        public async Task<Response<List<GenderViewDto>>> GetPeople()
        {
            var response = new Response<List<GenderViewDto>>();

            var peopleResponse = await _peopleExternalService.GetPeople();
            if(peopleResponse.ResponseStatus == ResponseStatusEnum.Failure)
            {
                response.Errors.AddRange(peopleResponse.Errors);
                return response;
            }

            var cats = peopleResponse.Data
                .Where(person => person.Pets != null) // owner must have a pet
                .Where(person => person.Pets.Any(pet => pet.Type == PetTypeEnum.Cat)) // owner must have a cat
                .GroupBy(person => person.Gender, // group by gender (male/female)
                    person => person.Pets, 
                    (gender, pets) => new GenderViewDto
                    {
                        Gender = gender,
                        Cats = pets
                            .SelectMany(pet => pet) //get all pets from a specific gender
                            .Where(pet => pet.Type == PetTypeEnum.Cat) //get all cats
                            .OrderBy(pet => pet.Name) // alphabetical ordered cat names
                            .Select(pet => new CatViewDto
                            {
                                Name = pet.Name //only return cat name
                            }).ToList()
                    });

            response.Data = cats.ToList();
            return response;
        }
    }
}
