using FluentNHibernate.Mapping;
using PersonalData.Entities;

namespace PersonalData.Mappings
{
    public class PhoneMap : ClassMap<Phone>
    {
        public PhoneMap()
        {
            Table("Phone");

            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.Number, "Number");

            References(x => x.Person, "IdPerson");
        }
    }
}

