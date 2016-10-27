using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using System.Collections.ObjectModel;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsDAOs
{
    [TestFixture]
    public class ComentarioDAOTest
    {
        private static IUnityContainer container;

        TransactionScope transaction;
        private static long NON_EXISTING_EVENT = -1;
        private static long NON_EXISTING_USER = -1;
        private static IComentarioDao comentarioDao;
        private static IEventoDao eventoDao;
        private static IUserProfileDao usuarioDao;
        private Comentario comentario1;
        private Comentario comentario2;
        private Comentario comentario3;
        private Comentario comentario4;
        private UserProfile userProfile;
        private Evento evento1;
        private Evento evento2;
        private Evento evento3;
        private TestContext testContextInstance;

        //Mocks
        //private static Mock<IEventoDao> mockEventoDao;
        //private static Mock<IUserProfileDao> mockUserProfileDao;
        //private static Mock<IComentarioDao> mockComentarioDao;



        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        //Use ClassInitialize to run code before running the first test in the class
        [OneTimeSetUp]
        public static void MyClassInitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            comentarioDao = container.Resolve<IComentarioDao>();
            eventoDao = container.Resolve<IEventoDao>();
            usuarioDao = container.Resolve<IUserProfileDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize to run code before running each test
        [SetUp]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            //Usuario
            userProfile = new UserProfile();
            userProfile.loginName = "jsmith";
            userProfile.enPassword = "password";
            userProfile.firstName = "John";
            userProfile.lastName = "Smith";
            userProfile.email = "jsmith@acme.com";
            userProfile.language = "en";
            userProfile.country = "US";

            usuarioDao.Create(userProfile);

            //Evento
            evento1 = new Evento();
            evento1.fecha = DateTime.Now;
            evento1.fecha.AddDays(-20);
            evento1.nombre = "Evento 1";
            evento1.reseña = "Esto es la reseña del evento 1";

            eventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.fecha = DateTime.Now;
            evento2.nombre = "Evento 2";
            evento2.reseña = "Esto es la reseña del evento 2";

            eventoDao.Create(evento2);

            evento3 = new Evento();
            evento3.fecha = DateTime.Now;
            evento3.nombre = "Evento 3";
            evento3.reseña = "Esto es la reseña del evento 3";

            eventoDao.Create(evento3);

            //Comentario
            comentario1 = new Comentario();
            comentario1.UserProfile = userProfile;
            comentario1.Evento = evento1;
            comentario1.fecha = DateTime.Now;
            comentario1.fecha.AddDays(-15);
            comentario1.texto = "Esto es un comentario";


            comentarioDao.Create(comentario1);

            //Comentario
            comentario2 = new Comentario();
            comentario2.UserProfile = userProfile;
            comentario2.Evento = evento2;
            comentario2.fecha = DateTime.Now;
            comentario2.fecha.AddDays(-14);
            comentario2.texto = "Esto es un comentario2";


            comentarioDao.Create(comentario2);

            //Comentario
            comentario3 = new Comentario();
            comentario3.UserProfile = userProfile;
            comentario3.Evento = evento1;
            comentario3.fecha = DateTime.Now;
            comentario3.fecha.AddDays(-13);
            comentario3.texto = "Esto es un comentario3";


            comentarioDao.Create(comentario3);

            comentario4 = new Comentario();
            comentario4.UserProfile = userProfile;
            comentario4.Evento = evento1;
            comentario4.fecha = DateTime.Now;
            comentario4.fecha.AddDays(-12);
            comentario4.texto = "Esto es un comentario4";


            comentarioDao.Create(comentario4);
        }

        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_01()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(evento1.idEvento, 0, 10);

                Assert.IsTrue(comentarios.Count == 3);
                Assert.IsTrue(comentarios.Contains(comentario4));
                Assert.IsTrue(comentarios.Contains(comentario3));
                Assert.IsTrue(comentarios.Contains(comentario1));

            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_02()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(evento1.idEvento, 0, 0);

                Assert.IsTrue(comentarios.Count == 3);
                Assert.IsTrue(comentarios.Contains(comentario4));
                Assert.IsTrue(comentarios.Contains(comentario3));
                Assert.IsTrue(comentarios.Contains(comentario1));

            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void PR_UN_03()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(evento1.idEvento, 0, -1);

                Assert.IsTrue(comentarios.Count == 3);
                Assert.IsTrue(comentarios.Contains(comentario4));
                Assert.IsTrue(comentarios.Contains(comentario3));
                Assert.IsTrue(comentarios.Contains(comentario1));

            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_04()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(evento3.idEvento, 0, 0);

                Assert.IsTrue(comentarios.Count == 0);

            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_05()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(NON_EXISTING_EVENT, 0, 0);

                Assert.IsTrue(comentarios.Count == 0);

            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_06()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.VerComentarios(evento1.idEvento, 1, 1);

                Assert.IsTrue(comentarios.Count == 1);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_07()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.BuscarPorUsuario(userProfile.usrId, evento2.idEvento);

                Assert.IsTrue(comentarios.Count == 1);
                Assert.AreEqual(comentarios[0], comentario2);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_08()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.BuscarPorUsuario(userProfile.usrId, evento3.idEvento);

                Assert.IsTrue(comentarios.Count == 0);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_09()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.BuscarPorUsuario(NON_EXISTING_USER, evento3.idEvento);

                Assert.IsTrue(comentarios.Count == 0);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [Test]
        public void PR_UN_10()
        {
            try
            {
                Collection<Comentario> comentarios = comentarioDao.BuscarPorUsuario(userProfile.usrId, NON_EXISTING_EVENT);

                Assert.IsTrue(comentarios.Count == 0);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

    }
}
