using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Enums;

namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class PetsGroupedByOwnerGenderResponseDto : ResponseDto
    {
        public PetType PetType { get; set; }
        public IEnumerable<PersonJsonDto> People { get; set; }
        public IEnumerable<GenderPetsDto> GroupedPets { get; set; }
    }
}
