using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendacionDao;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsDAOs
{
    [TestFixture]
    class RecomendacionDAOTest
    {
        private static IUnityContainer container;

        TransactionScope transaction;
        private static IEventoDao eventoDao;
        private static IRecomendacionDao recomendacionDao;
        // private static IUserProfileDao userProfileDao;
        private static IGrupoDao grupoDao;

        private static Evento evento1;
        private static Evento evento2;
        private static Grupo grupo1;
        private static Grupo grupo2;
        private static Recomendacion recomendacion;
        private static Recomendacion recomendacion2;


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

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [OneTimeSetUp]
        public static void MyClassInitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            eventoDao = container.Resolve<IEventoDao>();
            recomendacionDao = container.Resolve<IRecomendacionDao>();
            grupoDao = container.Resolve<IGrupoDao>();

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

            evento1 = new Evento();
            evento1.nombre = "Evento 1";
            evento1.fecha = DateTime.Now;
            evento1.reseña = "Reseña evento1";
            eventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.nombre = "Evento 2";
            evento2.fecha = DateTime.Now;
            evento2.reseña = "Reseña evento2";
            eventoDao.Create(evento2);

            grupo1 = new Grupo();
            grupo1.nombre = "Deportes";
            grupo1.descripcion = "Grupo de deportes";
            grupoDao.Create(grupo1);

            grupo2 = new Grupo();
            grupo2.nombre = "Música";
            grupo2.descripcion = "Grupo de música";
            grupoDao.Create(grupo2);

            recomendacion = new Recomendacion();
            recomendacion.fecha = DateTime.Now;
            recomendacion.texto = "Recomiendo este evento, es buenísimo.";
            recomendacion.Evento = evento1;
            recomendacion.Grupo.Add(grupo1);
            recomendacion.Grupo.Add(grupo2);
            recomendacionDao.Create(recomendacion);


            recomendacion2 = new Recomendacion();
            recomendacion2.fecha = DateTime.Now;
            recomendacion2.texto = "Recomiendo 2 este evento, es buenísimo.";
            recomendacion2.Evento = evento2;
            recomendacion2.Grupo.Add(grupo2);
            recomendacionDao.Create(recomendacion2);
        }

        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }
        #endregion

        [Test]
        public void PR_UN_22()
        {
            Assert.IsTrue(recomendacionDao.BuscarRecomendacion(
                grupo2.idGrupo, recomendacion2.Evento.idEvento));
        }


        [Test]
        public void PR_UN_23()
        {
            Assert.IsTrue(!recomendacionDao.BuscarRecomendacion(
                grupo1.idGrupo, recomendacion2.Evento.idEvento));
        }
    }
}
