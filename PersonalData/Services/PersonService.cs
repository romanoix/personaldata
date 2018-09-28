using PersonalData.Entities;
using PersonalData.IServices;
using PersonalData.Models;
using System.Collections.Generic;

namespace PersonalData.Services
{
    public class PersonService : IPersonService
    {
        public void DeletePerson(int id)
        {
            AppFacade.DataAccess.InTransaction(s =>
            {
                var PersonCreate = s.CreateCriteria(typeof(Person)).List<Person>();
                foreach (var con in PersonCreate)
                {
                    if (con.Id == id)
                        s.Delete(con);
                }
            });
        }
        public void SavePerson(string name, string surname, int id = 0)
        {
            Person country;
            AppFacade.DataAccess.InTransaction(s =>
            {
                if (id != 0)
                {
                    country = new Person
                    {
                        Id = id,
                        Name = name,
                        Surname = surname

                    };
                }
                else
                {
                    country = new Person
                    {
                        Name = name,
                        Surname = surname
                    };

                    s.SaveOrUpdate(country);
                }
            });
        }
        public List<PersonModel> GetPerson()
        {
            var personModelList = new List<PersonModel>();
            AppFacade.DataAccess.InTransaction(s =>
            {
                var PersonCreate = s.CreateCriteria((typeof(Person))).List<Person>();
                foreach (var per in PersonCreate)
                {
                    var temp = new PersonModel()
                    {
                        Id = per.Id,
                        Name = per.Name,
                        Surname = per.Surname,
                    };

                    personModelList.Add(temp);
                }
            });
            return personModelList;
        }
    }
}
