using System;
using System.Linq;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Repository;
using AGLDeveloperTest.BusinessLayer.Entity;
using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Dto;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public class PeopleService : IPeopleService
    {
        public IPeopleRepository PeopleRepository { get; }

        public PeopleService()
        {
            PeopleRepository = new PeopleRepository();
        }

        public PeopleService(IPeopleRepository peopleRepository)
        {
            PeopleRepository = peopleRepository;
        }

        public IEnumerable<PersonPetMap> GetCatsWithOwner()
        {
            var catsWithOwner = PeopleRepository.People.SelectMany(p => (p.pets ?? Enumerable.Empty<Pet>()).Where(pet => string.Equals(pet.type, PetType.Cat.ToString(), StringComparison.OrdinalIgnoreCase))
                                           .Select(pet => new PersonPetMap { Person = p, Pet = pet })).ToList();
            return catsWithOwner;
        }
        
        public IEnumerable<IGrouping<string,PersonPetMap>> GetPetGroupsGroupedByOwnerGender(IEnumerable<PersonPetMap> personPetMapList, OrderBy orderBy)
        {
            var orderedPetMapList = Enumerable.Empty<PersonPetMap>();
            if (orderBy == OrderBy.Ascending)
            {
                orderedPetMapList = personPetMapList.OrderBy(p => p.Pet.name);
            }
            else
            {
                orderedPetMapList = personPetMapList.OrderByDescending(p => p.Pet.name);
            }
            var groupedPets = orderedPetMapList.GroupBy(p => p.Person.gender == null ? string.Empty : p.Person.gender).ToList();
            return groupedPets;
        }
    }
}
