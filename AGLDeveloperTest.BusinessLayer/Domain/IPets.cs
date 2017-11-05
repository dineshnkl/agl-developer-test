using AGLDeveloperTest.BusinessLayer.Dto;
using AGLDeveloperTest.BusinessLayer.Enums;

namespace AGLDeveloperTest.BusinessLayer.Domain
{
    public interface IPets
    {
        PetsGroupedByOwnerGenderResponseDto GetCatsGroupedByOwnerGender(OrderBy orderBy);
    }
}
