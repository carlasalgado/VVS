﻿using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaMaD.Model.EtiquetaDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendacionDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test.TestsServicios
{
    [TestClass()]
    public class IEtiquetaServiceTest
    {
        private static IUnityContainer container;
        private static IEventoService EventoService;
        private static IEventoDao EventoDao;
        private static IEtiquetaDao EtiquetaDao;
        private static IComentarioDao ComentarioDao;
        private static IUserProfileDao UserProfileDao;

        private static long NON_EXISTING_COMMENT = -1;
        private static long NON_EXISTING_TAG = -1;
        private static string NON_EXISTING_TAG_NAME = "NON_EXISTING_TAG_NAME";

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            container = TestManager.ConfigureUnityContainer("unity");
            EventoService = container.Resolve<IEventoService>();
            EventoDao = container.Resolve<IEventoDao>();
            EtiquetaDao = container.Resolve<IEtiquetaDao>();
            ComentarioDao = container.Resolve<IComentarioDao>();
            UserProfileDao = container.Resolve<IUserProfileDao>();
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
        }
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #region Tests Etiquetas

        /// <summary>
        ///A test for create tags
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_CrearEtiquetaTest()
        {
            Etiqueta eEsperada = EventoService.CrearEtiqueta("Etiqueta1");
            Etiqueta eObtenida = EventoService.EtiquetaPorId(eEsperada.idEtiqueta);
            Assert.AreEqual(eEsperada, eObtenida);
        }

        /// <summary>
        ///A test for create duplicate tag
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void ServicioEvento_CrearEtiquetaDuplicadaTest()
        {
            EventoService.CrearEtiqueta("Etiqueta 1");
            EventoService.CrearEtiqueta("Etiqueta 1");
        }

        /// <summary>
        ///A test for NON_EXISTING_TAG
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_BuscarEtiquetaIdInexistenteTest()
        {
            EventoService.EtiquetaPorId(NON_EXISTING_TAG);
        }

        /// <summary>
        ///A test for add and remove tags from one comment
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_AnadirEliminarEtiqueta()
        {
            Evento evento = new Evento();
            evento = new Evento();
            evento.nombre = "Evento";
            evento.reseña = "Reseña evento";
            evento.fecha = DateTime.Now;
            EventoDao.Create(evento);

            UserProfile user = new UserProfile();
            user = new UserProfile();
            user.loginName = "jsmith";
            user.enPassword = "password";
            user.firstName = "John";
            user.lastName = "Smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";
            UserProfileDao.Create(user);

            Comentario c = new Comentario();
            c = new Comentario();
            c.texto = "Esto es un comentario";
            c.fecha = DateTime.Now;
            c.Evento = evento;
            c.UserProfile = user;
            ComentarioDao.Create(c);

            Etiqueta e1 = EventoService.CrearEtiqueta("etiqueta1");
            Etiqueta e2 = EventoService.CrearEtiqueta("etiqueta2");

            List<Etiqueta> etiquetas = new List<Etiqueta>();

            etiquetas.Add(e1);
            etiquetas.Add(e2);

            EventoService.AnadirEtiqueta(c.idComentario, etiquetas);

            Comentario cObtenido = ComentarioDao.Find(c.idComentario);
            Assert.IsTrue(cObtenido.Etiqueta.Contains(e1));
            Assert.IsTrue(cObtenido.Etiqueta.Contains(e2));
            Assert.IsTrue(cObtenido.Etiqueta.Count == 2);

            etiquetas.Remove(e1);

            EventoService.AnadirEtiqueta(c.idComentario, etiquetas);
            cObtenido = ComentarioDao.Find(c.idComentario);
            Assert.IsTrue(cObtenido.Etiqueta.Contains(e2));
            Assert.IsTrue(cObtenido.Etiqueta.Count == 1);
        }

        /// <summary>
        ///A test for add and remove tags from one comment, the comment doesnt exist.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_AnadirEliminarComentarioInexistenteEtiqueta()
        {
            EventoService.AnadirEtiqueta(NON_EXISTING_COMMENT, new List<Etiqueta>());
        }

        /// <summary>
        ///A test for show tags of one comment
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_MostrarComentariosEtiquetaTest()
        {
            Evento evento = new Evento();
            evento = new Evento();
            evento.nombre = "Evento";
            evento.reseña = "Reseña evento";
            evento.fecha = DateTime.Now;
            EventoDao.Create(evento);

            UserProfile user = new UserProfile();
            user = new UserProfile();
            user.loginName = "jsmith";
            user.enPassword = "password";
            user.firstName = "John";
            user.lastName = "Smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";
            UserProfileDao.Create(user);

            Comentario c = new Comentario();
            c = new Comentario();
            c.texto = "Esto es un comentario";
            c.fecha = DateTime.Now;
            c.Evento = evento;
            c.UserProfile = user;
            ComentarioDao.Create(c);
            
            ComentarioDTO dtoC = new ComentarioDTO();
            dtoC.fecha = c.fecha;
            dtoC.idComentario = c.idComentario;
            dtoC.login = c.UserProfile.loginName;
            dtoC.texto = c.texto;

            Comentario c1 = new Comentario();
            c1 = new Comentario();
            c1.texto = "Esto es un comentario 1";
            c1.fecha = DateTime.Now;
            c1.Evento = evento;
            c1.UserProfile = user;
            ComentarioDao.Create(c1);

            ComentarioDTO dtoC1 = new ComentarioDTO();
            dtoC1.fecha = c.fecha;
            dtoC1.idComentario = c.idComentario;
            dtoC1.login = c.UserProfile.loginName;
            dtoC1.texto = c.texto;

            Comentario c2 = new Comentario();
            c2 = new Comentario();
            c2.texto = "Esto es un comentario 2";
            c2.fecha = DateTime.Now;
            c2.Evento = evento;
            c2.UserProfile = user;

            ComentarioDao.Create(c1);
            List<Etiqueta> etiquetas = new List<Etiqueta>();

            etiquetas.Add(EventoService.CrearEtiqueta("etiqueta1"));

            EventoService.AnadirEtiqueta(c.idComentario, etiquetas);
            EventoService.AnadirEtiqueta(c1.idComentario, etiquetas);

            List<ComentarioDTO> comentarios = EventoService.MostrarComentariosEtiqueta(etiquetas[0].nombre);

            Assert.AreEqual(2, comentarios.Count());
            Assert.IsTrue(comentarios.Contains(dtoC));
            Assert.IsTrue(comentarios.Contains(dtoC1));
        }

        /// <summary>
        ///A test for show tags of one comment, the comment doesnt exist.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_MostrarComentariosEtiquetaNoExistenteTest()
        {
            EventoService.MostrarComentariosEtiqueta(NON_EXISTING_TAG_NAME);
        }

        [TestMethod()]
        public void ServicioEvento_NubeEtiquetaTest()
        {
            Evento evento = new Evento();
            evento = new Evento();
            evento.nombre = "Evento";
            evento.reseña = "Reseña evento";
            evento.fecha = DateTime.Now;
            EventoDao.Create(evento);

            UserProfile user = new UserProfile();
            user = new UserProfile();
            user.loginName = "jsmith";
            user.enPassword = "password";
            user.firstName = "John";
            user.lastName = "Smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "US";
            UserProfileDao.Create(user);

            Comentario c = new Comentario();
            c = new Comentario();
            c.texto = "Esto es un comentario";
            c.fecha = DateTime.Now;
            c.Evento = evento;
            c.UserProfile = user;
            ComentarioDao.Create(c);

            Comentario c1 = new Comentario();
            c1 = new Comentario();
            c1.texto = "Esto es un comentario 1";
            c1.fecha = DateTime.Now;
            c1.Evento = evento;
            c1.UserProfile = user;
            ComentarioDao.Create(c1);

            Comentario c2 = new Comentario();
            c2 = new Comentario();
            c2.texto = "Esto es un comentario 2";
            c2.fecha = DateTime.Now;
            c2.Evento = evento;
            c2.UserProfile = user;
            ComentarioDao.Create(c2);

            Comentario c3 = new Comentario();
            c3 = new Comentario();
            c3.texto = "Esto es un comentario 3";
            c3.fecha = DateTime.Now;
            c3.Evento = evento;
            c3.UserProfile = user;
            ComentarioDao.Create(c3);

            Etiqueta e1 = EventoService.CrearEtiqueta("etiqueta1");
            Etiqueta e2 = EventoService.CrearEtiqueta("etiqueta2");
            Etiqueta e3 = EventoService.CrearEtiqueta("etiqueta3");
            Etiqueta e4 = EventoService.CrearEtiqueta("etiqueta4");
            Etiqueta e5 = EventoService.CrearEtiqueta("etiqueta5");

            List<Etiqueta> etiquetas = new List<Etiqueta>();

            etiquetas.Add(e1);
            etiquetas.Add(e2);
            etiquetas.Add(e3);
            etiquetas.Add(e4);

            EventoService.AnadirEtiqueta(c.idComentario, etiquetas);

            etiquetas.Remove(e3);
            EventoService.AnadirEtiqueta(c1.idComentario, etiquetas);

            etiquetas.Remove(e1);
            EventoService.AnadirEtiqueta(c2.idComentario, etiquetas);

            etiquetas.Remove(e4);
            EventoService.AnadirEtiqueta(c3.idComentario, etiquetas);

            List<Etiqueta> nube = EventoService.NubeEtiquetas();

            Assert.AreEqual(5, nube.Count());
            Assert.AreEqual(e2, nube[0]);
            Assert.AreEqual(e4, nube[1]);
            Assert.AreEqual(e1, nube[2]);
            Assert.AreEqual(e3, nube[3]);
            Assert.AreEqual(e5, nube[4]);

        }


        #endregion
   }


}