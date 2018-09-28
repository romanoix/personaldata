using System.Collections.Generic;

namespace PersonalData.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<PhoneModel> PhonesModel { get; set; }
        public IList<AddressModel> AddressesModel { get; set; }
    }
}
