using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Enums;
using System.Linq;
using AGLDeveloperTest.BusinessLayer.Entity;

namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class PetsGroupedByOwnerGenderResponseDto : ResponseDto
    {
        public PetType PetType { get; set; }
        public IEnumerable<IGrouping<string, PersonPetMap>> GroupedByOwner { get; set; }
    }
}
