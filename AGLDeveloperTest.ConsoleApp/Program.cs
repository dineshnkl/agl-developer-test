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
            var groupedCatsResponseDto = petsDomain.GetCatsGroupedByOwnerGender(OrderBy.Ascending);
            if (groupedCatsResponseDto.Success)
            {
                foreach (var group in groupedCatsResponseDto.GroupedByOwner)
                {
                    Console.WriteLine(group.Key);
                    Console.WriteLine(new string('-', group.Key.Length));
                    foreach (var map in group)
                    {
                        Console.WriteLine("{0}", map.Pet.name);
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
