using System.Collections.Generic;

namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class GenderPetsDto
    {
        public string Gender { get; set; }
        public IEnumerable<PetJsonDto> Pets { get; set; }
    }
}
