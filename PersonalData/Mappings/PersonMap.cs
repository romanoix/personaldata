using FluentNHibernate.Mapping;
using PersonalData.Entities;

namespace PersonalData.Mappings
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("Person");

            Id(x => x.Id, "Id").GeneratedBy.Native();
            Map(x => x.Name, "Name");
            Map(x => x.Surname, "Surname");

            HasMany(x => x.Phones).Inverse().Cascade.All();
            HasMany(x => x.Addresses).Inverse().Cascade.All();
        }
    }
}
