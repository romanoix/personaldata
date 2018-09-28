namespace PersonalData.Models
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public PersonModel PersonModel { get; set; }
    }
}
