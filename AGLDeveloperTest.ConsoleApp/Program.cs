using System;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Domain;

namespace AGLDeveloperTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var petsDomain = new Pets();
            var peopleDataUrl = "http://agl-developer-test.azurewebsites.net/people.json";
            var groupedCatsResponseDto = petsDomain.GetCatsGroupedByOwnerGender(peopleDataUrl, OrderBy.Ascending);
            if (groupedCatsResponseDto.Success)
            {
                foreach (var group in groupedCatsResponseDto.GroupedPets)
                {
                    Console.WriteLine(group.Gender);
                    Console.WriteLine(new string('-', group.Gender.Length));
                    foreach (var cat in group.Pets)
                    {
                        Console.WriteLine("{0}", cat.name);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Unable to get cats. Error message: \"{0}\"", groupedCatsResponseDto.Message);
            }
            Console.ReadLine();
        }
    }
}
