using PersonalData.Entities;
using PersonalData.IServices;
using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.Services
{
    public class PhoneService : IPhoneService
    {
        public void DeletePhone(int id)
        {
            AppFacade.DataAccess.InTransaction(s =>
            {
                var PhoneCreate = s.CreateCriteria(typeof(Phone))
                  .List<Phone>();

                foreach (var pho in PhoneCreate)
                {
                    if (pho.Id == id)
                        s.Delete(pho);
                }
            });
        }
        public void SavePhone(int number, int personId, int phoneId = 0)
        {
            Phone phone;

            AppFacade.DataAccess.InTransaction(s =>
            {
                if (phoneId != 0)
                {
                    phone = new Phone
                    {
                        Id = phoneId,
                        Number = number,

                        Person = new Person { Id = personId }
                    };
                }
                else
                {
                    phone = new Phone
                    {
                        Number = number,

                        Person = new Person { Id = personId }
                    };
                }
                s.SaveOrUpdate(phone);
            });
        }
        public List<PhoneModel> GetPhone()
        {
            var phoneModelList = new List<PhoneModel>();
            AppFacade.DataAccess.InTransaction(s =>
            {
                var PhoneCreate = s.CreateCriteria(typeof(Phone))
                  .List<Phone>();

                foreach (var pho in PhoneCreate)
                {
                    var temp = new PhoneModel()
                    {
                        Id = pho.Id,
                        Number = pho.Number,

                        PersonModel = new PersonModel
                        {
                            Id = pho.Person.Id,
                        }
                    };
                    phoneModelList.Add(temp);
                }
            });
            return phoneModelList;
        }
    }
}
