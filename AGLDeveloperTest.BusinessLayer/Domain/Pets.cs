using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Dto;
using AGLDeveloperTest.BusinessLayer.Service;

namespace AGLDeveloperTest.BusinessLayer.Domain
{
    public class Pets : IPets
    {
        public IPeopleService PeopleService { get; }

        public Pets()
        {
            PeopleService = new PeopleService();
        }

        public Pets(IPeopleService peopleService)
        {
            PeopleService = peopleService;
        }

        public PetsGroupedByOwnerGenderResponseDto GetCatsGroupedByOwnerGender(OrderBy orderBy)
        {
            var catsWithOwner = PeopleService.GetCatsWithOwner();
            var groupedCats = PeopleService.GetPetGroupsGroupedByOwnerGender(catsWithOwner, orderBy);
            var responseDto = new PetsGroupedByOwnerGenderResponseDto { Success = true, PetType = PetType.Cat };
            responseDto.GroupedByOwner = groupedCats;
            return responseDto;
        }
    }
}
