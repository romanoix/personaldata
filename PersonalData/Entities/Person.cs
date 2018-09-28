using System.Collections.Generic;

namespace PersonalData.Entities
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<Phone> Phones { get; set; }
        public virtual IList<Address> Addresses { get; set; }
    }
}
