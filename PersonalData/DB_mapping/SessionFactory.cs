using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Reflection;

namespace PersonalData.DB_mapping
{
    public static class SessionFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                               .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Data Source=DESKTOP-8TM1SVS\\SQLEXPRESS;Initial Catalog=AddressBook;Integrated Security=True"))
                               .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                               .BuildSessionFactory();
        }
    }
}

