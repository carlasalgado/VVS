using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    [TestFixture]
    public class EventoServiceTest
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


        /// <summary>
        ///A test for Find Events
        ///</summary>
        [Test]
        public void PR_UN_36()
        {
            List<EventoDTO> eventosEsperados = new List<EventoDTO>();

            EventoDTO dto1 = new EventoDTO();
            EventoDTO dto2 = new EventoDTO();
            EventoDTO dto3 = new EventoDTO();
            EventoDTO dto4 = new EventoDTO();

            dto1.idEvento = evento1.idEvento;
            dto1.nombre = evento1.nombre;
            dto1.fecha = evento1.fecha;
            dto1.reseña = evento1.reseña;

            dto2.idEvento = evento2.idEvento;
            dto2.nombre = evento2.nombre;
            dto2.fecha = evento2.fecha;
            dto2.reseña = evento2.reseña;

            dto3.idEvento = evento3.idEvento;
            dto3.nombre = evento3.nombre;
            dto3.fecha = evento3.fecha;
            dto3.reseña = evento3.reseña;

            dto4.idEvento = evento4.idEvento;
            dto4.nombre = evento4.nombre;
            dto4.fecha = evento4.fecha;
            dto4.reseña = evento4.reseña;

            eventosEsperados.Add(dto1);
            eventosEsperados.Add(dto2);
            eventosEsperados.Add(dto3);
            eventosEsperados.Add(dto4);

            BloqueEventos eventosObtenidos = EventoService.BusquedaEventos("evento");

            Assert.AreEqual(4, eventosObtenidos.Eventos.Count);

            foreach (EventoDTO e in eventosEsperados)
                Assert.IsTrue(eventosObtenidos.Eventos.Contains(e));

        }

        /// <summary>
        ///A test for Find Events without events
        ///</summary>
        [Test]
        public void PR_UN_37()
        {
            BloqueEventos eventosObtenidos = EventoService.BusquedaEventos(NON_EXISTING_EVENT_NAME);

            Assert.AreEqual(0, eventosObtenidos.Eventos.Count);
        }

        /// <summary>
        ///A test for Find Event with one id
        ///</summary>
        [Test]
        public void PR_UN_38()
        {
            EventoDTO dtoObtenido = new EventoDTO();
            EventoDTO dtoEsperado = new EventoDTO();

            dtoEsperado.idEvento = evento1.idEvento;
            dtoEsperado.nombre = evento1.nombre;
            dtoEsperado.reseña = evento1.reseña;
            dtoEsperado.fecha = evento1.fecha;

            dtoObtenido = EventoService.BuscarEvento(evento1.idEvento);

            Assert.AreEqual(dtoEsperado, dtoObtenido);
        }

        /// <summary>
        ///A test for Find Event non existing event
        ///</summary>
        [Test]
        public void PR_UN_39()
        {
            try
            {
                EventoService.BuscarEvento(NON_EXISTING_EVENT);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
