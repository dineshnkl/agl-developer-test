using System;
using System.Linq;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Dto;
using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Service;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Domain
{
    public class Pets : IPets
    {
        private const string CatTypeString = "Cat";
        public IDownloadService DownloadService { get; }

        public Pets()
        {
            DownloadService = new DownloadService();
        }

        public Pets(IDownloadService downloadService)
        {
            DownloadService = downloadService;
        }

        private IEnumerable<GenderPetsDto> GetOrderedGroupedPets(IEnumerable<PersonJsonDto> people, string filterBy, OrderBy orderBy)
        {
            var filteredOrderedGroupedPets = people.GroupBy(p => p.gender == null ? string.Empty : p.gender.Trim().ToLower())
                                            .Select(g => new GenderPetsDto
                                            {
                                                Gender = g.Key,
                                                Pets = g.SelectMany(gp => gp.pets ?? Enumerable.Empty<PetJsonDto>())
                                                .Where(p => string.Equals(p.type, filterBy, StringComparison.OrdinalIgnoreCase))
                                            }).ToList();
            foreach (var groupedPets in filteredOrderedGroupedPets)
            {
                if (orderBy == OrderBy.Ascending)
                {
                    groupedPets.Pets = groupedPets.Pets.OrderBy(p => p.name).ToList();
                }
                else
                {
                    groupedPets.Pets = groupedPets.Pets.OrderByDescending(p => p.name).ToList();
                }
            }
            return filteredOrderedGroupedPets;
        }

        public PetsGroupedByOwnerGenderResponseDto GetCatsGroupedByOwnerGender(string url, OrderBy orderBy)
        {
            var responseDto = new PetsGroupedByOwnerGenderResponseDto();
            var readStringResponseDto = DownloadService.ReadStringFromUrl(url);
            responseDto.Success = readStringResponseDto.Success;
            responseDto.Message = readStringResponseDto.Message;
            responseDto.PetType = PetType.Cat;
            if (readStringResponseDto.Success)
            {
                responseDto.People = JsonHelper.Deserialize<PersonJsonDto[]>(readStringResponseDto.ResponseText);
                responseDto.GroupedPets = GetOrderedGroupedPets(responseDto.People, CatTypeString, orderBy);
            }
            return responseDto;
        }
    }
}
