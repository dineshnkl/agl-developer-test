namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class PersonJsonDto
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public PetJsonDto[] pets { get; set; }
    }
}
