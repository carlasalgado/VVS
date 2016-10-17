﻿using System;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsDAOs
{
    [TestFixture]
    public class ComentarioDAOTest
    {
        private static IUnityContainer container;

        TransactionScope transaction;
        private static IComentarioDao comentarioDao;
        private static IEventoDao eventoDao;
        private static IUserProfileDao usuarioDao;
        private Comentario comentario1;
        private Comentario comentario2;
        private UserProfile userProfile;
        private Evento evento1;
        private Evento evento2;
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
            evento1.nombre = "Evento 1";
            evento1.reseña = "Esto es la reseña del evento 1";

            eventoDao.Create(evento1);

            evento2 = new Evento();
            evento2.fecha = DateTime.Now;
            evento2.nombre = "Evento 2";
            evento2.reseña = "Esto es la reseña del evento 2";

            eventoDao.Create(evento2);

            //Comentario
            comentario1 = new Comentario();
            comentario1.UserProfile = userProfile;
            comentario1.Evento = evento1;
            comentario1.fecha = DateTime.Now;
            comentario1.texto = "Esto es un comentario";


            comentarioDao.Create(comentario1);

            //Comentario
            comentario2 = new Comentario();
            comentario2.UserProfile = userProfile;
            comentario2.Evento = evento2;
            comentario2.fecha = DateTime.Now;
            comentario2.texto = "Esto es un comentario2";


            comentarioDao.Create(comentario2);
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
                List<Comentario> comentarios = comentarioDao.VerComentarios(evento1.idEvento, 0, 10);

                Assert.IsTrue(comentarios.Count == 1);
                Assert.AreEqual(comentarios[0], comentario1);

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
                List<Comentario> comentarios = comentarioDao.BuscarPorUsuario(userProfile.usrId, evento1.idEvento);

                Assert.IsTrue(comentarios.Count == 1);
                Assert.AreEqual(comentarios[0], comentario1);
            }
            catch (InstanceNotFoundException)
            {
                Assert.Fail();
            }
        }

    }
}
