using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Diagnostics;
using System.Reflection;

namespace PersonalData
{
    public class AppFacade
    {
        public static class DataAccess
        {
            private static ISessionFactory _sessionFactory;
            private static readonly object SynsRoot = new object();

            private static readonly Func<ISession> DefaultOpenSession = () =>
            {
                if (_sessionFactory == null)
                {
                    lock (SynsRoot)
                    {
                        if (_sessionFactory == null)
                            Configure();
                    }
                }
                return _sessionFactory.OpenSession();
            };

            private static void Configure()
            {
                try
                {
                    _sessionFactory = Fluently.Configure()
                               .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Data Source=DESKTOP-8TM1SVS\\SQLEXPRESS;Initial Catalog=AddressBook;Integrated Security=True"))
                               .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                               .BuildSessionFactory();

                }
                catch (FluentConfigurationException ex)
                {
                    Debug.WriteLine(ex);
                    Process.GetCurrentProcess().Kill();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    Process.GetCurrentProcess().Kill();
                }
            }

            private static Func<ISession> _openSession;

            public static Func<ISession> OpenSession
            {
                set { _openSession = value; }
                get { return _openSession ?? DefaultOpenSession; }
            }

            public static void InTransaction(Action<ISession> operation)
            {
                using (var session = OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    operation(session);
                    tx.Commit();
                }
            }
        }
    }
}
