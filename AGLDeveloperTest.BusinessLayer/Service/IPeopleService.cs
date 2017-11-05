using System.Linq;
using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Entity;
using AGLDeveloperTest.BusinessLayer.Enums;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public interface IPeopleService
    {
        IEnumerable<PersonPetMap> GetCatsWithOwner();
        IEnumerable<IGrouping<string, PersonPetMap>> GetPetGroupsGroupedByOwnerGender(IEnumerable<PersonPetMap> personPetMapList, OrderBy orderBy);
    }
}
