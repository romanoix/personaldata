using PersonalData.Entities;
using PersonalData.IServices;
using System.Collections.Generic;
using System.Linq;

namespace PersonalData.Services
{
    class DeleteService : IDeleteService
    {
        public void DeletePersonData(int id)
        {
            AppFacade.DataAccess.InTransaction(s =>
            {
                var AddressCreate = s.CreateCriteria(typeof(Address)).List<Address>();
                var PersonCreate = s.CreateCriteria(typeof(Person)).List<Person>();
                var PhoneCreate = s.CreateCriteria(typeof(Phone)).List<Phone>();

                IEnumerable<Address> ListIdAddress = AddressCreate.Where(x => x.Person.Id == id);
                IEnumerable<Phone> ListIdPhone = PhoneCreate.Where(x => x.Person.Id == id);

                foreach (var a in ListIdAddress)
                {
                    s.Delete(a);
                }
                foreach (var p in ListIdPhone)
                {
                    s.Delete(p);
                }
                foreach (var e in PersonCreate)
                {
                    if (e.Id == id)
                        s.Delete(e);
                }
            });
        }
    }
}
