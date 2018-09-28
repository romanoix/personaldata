using FluentNHibernate.Mapping;
using PersonalData.Entities;

namespace PersonalData.Mappings
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("Address");

            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.Street, "Street");
            Map(x => x.City, "City");
            Map(x => x.ZipCode, "ZipCode");
            Map(x => x.Country, "Country");

            References(x => x.Person, "IdPerson");
        }
    }
}

