﻿using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.TestsDAOS
{
    [TestClass]
    public class IEventoDaoTest
    {

        #region Variables
        private static IUnityContainer container;
        private static IEventoDao eventoDao;
        private static Evento evento1;
        private static Evento evento2;
        private static Evento evento3;
        private static Evento evento4;
        private static Evento evento5;

        TransactionScope transaction;

        private TestContext testContextInstance;
        #endregion

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            container = TestManager.ConfigureUnityContainer("unity");
            eventoDao = container.Resolve<IEventoDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            evento1 = new Evento();
            evento1.nombre = "Evento 1";
            evento1.fecha = DateTime.Now;
            evento1.fecha.AddDays(14);
            evento1.reseña = "Reseña evento1";
            eventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.nombre = "Evento 2";
            evento2.fecha = DateTime.Now;
            evento1.fecha.AddDays(13);
            evento2.reseña = "Reseña evento2";
            eventoDao.Create(evento2);

            evento3 = new Evento();
            evento3.nombre = "Otro nombre";
            evento3.fecha = DateTime.Now;
            evento1.fecha.AddDays(12);
            evento3.reseña = "Reseña evento3";
            eventoDao.Create(evento3);

            evento4 = new Evento();
            evento4.nombre = "Evento 4";
            evento4.fecha = DateTime.Now;
            evento1.fecha.AddDays(11);
            evento4.reseña = "Reseña evento4";
            eventoDao.Create(evento4);

            evento5 = new Evento();
            evento5.nombre = "Evento 5";
            evento5.fecha = DateTime.Now;
            evento1.fecha.AddDays(10);
            evento5.reseña = "Reseña evento5";
            eventoDao.Create(evento5);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [TestMethod]
        public void DAOEvento_BuscarEventosTest()
        {
            /* Comprobamos que devuelve los eventos que coinciden
             * y comprobamos paginacion haciendo que se salte el primer
             * resultado y que nos muestre solo 2 resultados 
             */
            List<String> keyWords = new List<string>();

            keyWords.Add("Eve");
            keyWords.Add("to");

            List<Evento> obtenido = eventoDao.BuscarEventos(keyWords, 1, 2);

            Assert.IsTrue(obtenido.Count == 2);
            Assert.AreEqual(obtenido[0], evento2);
            Assert.AreEqual(obtenido[1], evento4);
        }

        [TestMethod]
        public void DAOEvento_BuscarEventosSinPalabrasClaveTest()
        {
            List<Evento> obtenido = eventoDao.BuscarEventos(0, 10);

            Assert.IsTrue(obtenido.Count == 5);
            Assert.IsTrue(obtenido.Contains(evento1));
            Assert.IsTrue(obtenido.Contains(evento2));
            Assert.IsTrue(obtenido.Contains(evento3));
            Assert.IsTrue(obtenido.Contains(evento4));
            Assert.IsTrue(obtenido.Contains(evento5));

        }

    }
}