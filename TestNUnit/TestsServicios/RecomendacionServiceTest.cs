using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.ObjectModel;
using FsCheck;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    [TestFixture]
    public class RecomendacionServiceTest
    {

        private static IUnityContainer container;
        private static IEventoService EventoService;
        private static IEventoDao EventoDao;
        private static IRecomendacionDao RecomendacionDao;
        private static IUserProfileDao UserProfileDao;
        private static IGrupoDao GrupoDao;
        private static long NON_EXISTING_EVENT = -1;
        private static long NON_EXISTING_USER = -1;
        private static long NON_EXISTING_GROUP = -1;


        UserProfile user;
        UserProfile usuarioIncorrecto;

        Evento evento1;
        Evento evento2;
        Evento evento3;
        Evento evento4;

        Grupo grupo;
        Grupo grupo2;

        TransactionScope transaction;


        private TestContext testContextInstance;

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
            EventoService = container.Resolve<IEventoService>();
            RecomendacionDao = container.Resolve<IRecomendacionDao>();
            EventoDao = container.Resolve<IEventoDao>();
            UserProfileDao = container.Resolve<IUserProfileDao>();
            GrupoDao = container.Resolve<IGrupoDao>();
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
            user = new UserProfile();
            user.loginName = "jsmith";
            user.enPassword = "password";
            user.firstName = "John";
            user.lastName = "Smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";

            UserProfileDao.Create(user);

            //Usuario 2
            usuarioIncorrecto = new UserProfile();
            usuarioIncorrecto.loginName = "jj";
            usuarioIncorrecto.enPassword = "password";
            usuarioIncorrecto.firstName = "Juan Jose";
            usuarioIncorrecto.lastName = "Martinez";
            usuarioIncorrecto.email = "jj@acme.com";
            usuarioIncorrecto.language = "es";
            usuarioIncorrecto.country = "ES";

            UserProfileDao.Create(usuarioIncorrecto);

            evento1 = new Evento();
            evento1.nombre = "Evento 1";
            evento1.reseña = "Reseña evento 1";
            evento1.fecha = DateTime.Now;
            EventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.nombre = "Evento 2";
            evento2.reseña = "Reseña evento 2";
            evento2.fecha = DateTime.Now;
            EventoDao.Create(evento2);

            evento3 = new Evento();
            evento3.nombre = "Evento 3";
            evento3.reseña = "Reseña evento 3";
            evento3.fecha = DateTime.Now;
            EventoDao.Create(evento3);

            evento4 = new Evento();
            evento4.nombre = "Evento 4";
            evento4.reseña = "Reseña evento 4";
            evento4.fecha = DateTime.Now;
            EventoDao.Create(evento4);

            grupo = new Grupo();
            grupo.nombre = "Nombre grupo";
            grupo.descripcion = "Descripcion grupo";
            grupo.UserProfile.Add(user);
            GrupoDao.Create(grupo);

            user.Grupo.Add(grupo);
            UserProfileDao.Update(user);

            grupo2 = new Grupo();
            grupo2.nombre = "Nombre grupo2";
            grupo2.descripcion = "Descripcion grupo2";
            GrupoDao.Create(grupo2);

        }
        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        /// <summary>
        ///A test for Recommend one event to some groups
        ///</summary>
        [Test]
        public void PR_IN_34()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            grupos.Add(grupo2);
            String comentario = "Recomiendo este evento";
            Recomendacion recomendacion = EventoService.RecomendarEvento(evento1.idEvento, grupos, comentario);

            Assert.IsTrue(GrupoDao.Find(grupos[0].idGrupo).Recomendacion.Contains(recomendacion));
            Assert.IsTrue(GrupoDao.Find(grupos[1].idGrupo).Recomendacion.Contains(recomendacion));
        }

        /// <summary>
        ///A test for Recommend one event to some groups random comment
        ///</summary>
        [Test]
        public void PR_AL_09()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            grupos.Add(grupo2);

            for (int i = 0; i < 100; i++)
            {
                String comentario = GeneradoresAleatorios.RandomString();
                Recomendacion recomendacion = EventoService.RecomendarEvento(evento1.idEvento, grupos, comentario);
            }
        }

        /// <summary>
        ///A test for Recommend one inexisteng event
        ///</summary>
        [Test]
        public void PR_IN_35()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            try
            {
                EventoService.RecomendarEvento(NON_EXISTING_EVENT, grupos, "Esto es un comentario");
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Recommend one event to 0 groups
        ///</summary>
        [Test]
        public void PR_IN_36()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            //grupos.Add(grupo);
            try
            {
                EventoService.RecomendarEvento(evento1.idEvento, grupos, "Esto es un comentario");
                Assert.IsTrue(false);
            }
            catch (SinGruposException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test to show the events recomended for one user
        ///</summary>
        [Test]
        public void PR_IN_37()
        {
            Collection<Grupo> grupos1 = new Collection<Grupo>();
            grupos1.Add(grupo);

            Collection<Grupo> grupos2 = new Collection<Grupo>();
            grupos2.Add(grupo2);

            Recomendacion recomendacion1 = EventoService.RecomendarEvento(evento1.idEvento, grupos1, "Texto recomendacion");
            Recomendacion recomendacion2 = EventoService.RecomendarEvento(evento2.idEvento, grupos1, "Texto recomendacion");
            Recomendacion recomendacion3 = EventoService.RecomendarEvento(evento3.idEvento, grupos1, "Texto recomendacion");
            Recomendacion recomendacion4 = EventoService.RecomendarEvento(evento4.idEvento, grupos2, "Texto recomendacion");

            List<RecomendacionDTO> recomendacionesEsperadas = new List<RecomendacionDTO>();
            RecomendacionDTO e1 = new RecomendacionDTO();
            RecomendacionDTO e2 = new RecomendacionDTO();
            RecomendacionDTO e3 = new RecomendacionDTO();

            e1.idEvento = recomendacion1.Evento.idEvento;
            e1.nombre = recomendacion1.Evento.nombre;
            e1.fecha = recomendacion1.Evento.fecha;
            e1.reseña = recomendacion1.Evento.reseña;
            e1.recomendacion = recomendacion1.texto;
            e1.fechaRecomendacion = recomendacion1.fecha;

            e2.idEvento = recomendacion2.Evento.idEvento;
            e2.nombre = recomendacion2.Evento.nombre;
            e2.fecha = recomendacion2.Evento.fecha;
            e2.reseña = recomendacion2.Evento.reseña;
            e2.recomendacion = recomendacion2.texto;
            e2.fechaRecomendacion = recomendacion2.fecha;

            e3.idEvento = recomendacion3.Evento.idEvento;
            e3.nombre = recomendacion3.Evento.nombre;
            e3.fecha = recomendacion3.Evento.fecha;
            e3.reseña = recomendacion3.Evento.reseña;
            e3.recomendacion = recomendacion3.texto;
            e3.fechaRecomendacion = recomendacion3.fecha;

            recomendacionesEsperadas.Add(e1);
            recomendacionesEsperadas.Add(e2);
            recomendacionesEsperadas.Add(e3);

            Collection<RecomendacionDTO> eventosObtenidos = EventoService.MostrarRecomendaciones(user.usrId);

            Assert.AreEqual(recomendacionesEsperadas.Count, eventosObtenidos.Count);
            foreach (RecomendacionDTO e in recomendacionesEsperadas)
                Assert.IsTrue(eventosObtenidos.Contains(e));
        }

        /// <summary>
        ///A test for show user recomendations, the user doesnt exist.
        ///</summary>
        [Test]
        public void PR_IN_38()
        {
            try
            {
                EventoService.MostrarRecomendaciones(NON_EXISTING_USER);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }



        /// <summary>
        ///A test to validate that one event was recommended to one group
        ///</summary>
        [Test]
        public void PR_IN_39()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            String comentario = "Recomiendo este evento";

            Recomendacion recomendacion = EventoService.RecomendarEvento(evento1.idEvento, grupos, comentario);


            Assert.IsTrue(EventoService.GrupoRecomendado(evento1.idEvento, grupo.idGrupo));
            Assert.IsFalse(EventoService.GrupoRecomendado(evento1.idEvento, grupo2.idGrupo));


        }

        /// <summary>
        ///A test to validate that one event was recommended to one group, the event doesnt exist
        ///</summary>
        [Test]
        public void PR_IN_40()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            String comentario = "Recomiendo este evento";

            Recomendacion recomendacion = EventoService.RecomendarEvento(evento1.idEvento, grupos, comentario);

            try
            {
                EventoService.GrupoRecomendado(NON_EXISTING_EVENT, grupo.idGrupo);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        ///A test to validate that one event was recommended to one group, the group doesnt exist
        ///</summary>
        [Test]
        public void PR_IN_41()
        {
            Collection<Grupo> grupos = new Collection<Grupo>();
            grupos.Add(grupo);
            String comentario = "Recomiendo este evento";

            Recomendacion recomendacion = EventoService.RecomendarEvento(evento1.idEvento, grupos, comentario);

            try
            {
                EventoService.GrupoRecomendado(evento1.idEvento, NON_EXISTING_GROUP);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
