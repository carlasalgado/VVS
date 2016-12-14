using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using FsCheck;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsAleatorios
{
    [TestFixture]
    public class UnitTest1
    {
        private static String NON_EXISTING_EVENT_NAME = "NON_EXISTING_EVENT";
        private static long NON_EXISTING_EVENT = -1;

        private static IUnityContainer container;
        private static IEventoService EventoService;
        private static IEventoDao EventoDao;

        Evento evento1 = new Evento();
        Evento evento2 = new Evento();
        Evento evento3 = new Evento();
        Evento evento4 = new Evento();

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

            EventoDao = container.Resolve<IEventoDao>();
            EventoService = container.Resolve<IEventoService>();
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
            evento1.reseña = "Reseña evento 1";
            evento1.fecha = DateTime.Now;
            EventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.nombre = "Evento 2";
            evento2.reseña = "Reseña evento 2";
            evento2.fecha = DateTime.Now;
            EventoDao.Create(evento2);

            evento3 = new Evento();
            evento3.nombre = "Otro Evento";
            evento3.reseña = "Reseña evento 3";
            evento3.fecha = DateTime.Now;
            EventoDao.Create(evento3);

            evento4 = new Evento();
            evento4.nombre = "Evento 4";
            evento4.reseña = "Reseña evento 4";
            evento4.fecha = DateTime.Now;
            EventoDao.Create(evento4);
        }

        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }


        public static string randomString()
        {
            var maxLength = 100;
            return Arb.Generate<string>().Sample(maxLength, 1).ToString();
        }



        /// <summary>
        ///A test for Find Events without events
        ///</summary>
        [Test]
        public void PR_AL_01()
        {
            for (int i=0; i < 100; i++)
            {
                string s = randomString();
                EventoService.BusquedaEventos(s, 0, 10);
            }
        }
    }
}
