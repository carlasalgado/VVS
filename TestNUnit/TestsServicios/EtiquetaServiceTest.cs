using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using FsCheck;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    [TestFixture]
    public class EtiquetaServiceTest
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
        [OneTimeSetUp]
        public static void MyClassInitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            EventoService = container.Resolve<IEventoService>();
            EventoDao = container.Resolve<IEventoDao>();
            EtiquetaDao = container.Resolve<IEtiquetaDao>();
            ComentarioDao = container.Resolve<IComentarioDao>();
            UserProfileDao = container.Resolve<IUserProfileDao>();
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
        }
        //Use TestCleanup to run code after each test has run

        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #region Tests Etiquetas

        /// <summary>
        ///A test for create tags
        ///</summary>
        [Test]
        public void PR_IN_22()
        {
            Etiqueta eEsperada = EventoService.CrearEtiqueta("Etiqueta1");
            Etiqueta eObtenida = EventoService.EtiquetaPorId(eEsperada.idEtiqueta);
            Assert.AreEqual(eEsperada, eObtenida);
        }

        /// <summary>
        ///A test for create duplicate tag
        ///</summary>
        [Test]
        public void PR_IN_23()
        {
            EventoService.CrearEtiqueta("Etiqueta 1");
            try
            {
                EventoService.CrearEtiqueta("Etiqueta 1");
                Assert.IsTrue(false);
            }
            catch (DuplicateInstanceException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for NON_EXISTING_TAG
        ///</summary>
        [Test]
        public void PR_IN_24()
        {
            try
            {
                EventoService.EtiquetaPorId(NON_EXISTING_TAG);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        ///A test for add and remove tags from one comment
        ///</summary>
        [Test]
        public void PR_IN_25()
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

            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

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
        [Test]
        public void PR_IN_26()
        {
            try
            {
                EventoService.AnadirEtiqueta(NON_EXISTING_COMMENT, new Collection<Etiqueta>());
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for show tags of one comment
        ///</summary>
        [Test]
        public void PR_IN_27()
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
            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

            etiquetas.Add(EventoService.CrearEtiqueta("etiqueta1"));

            EventoService.AnadirEtiqueta(c.idComentario, etiquetas);
            EventoService.AnadirEtiqueta(c1.idComentario, etiquetas);

            Collection<ComentarioDTO> comentarios = EventoService.MostrarComentariosEtiqueta(etiquetas[0].nombre);

            Assert.AreEqual(2, comentarios.Count());
            Assert.IsTrue(comentarios.Contains(dtoC));
            Assert.IsTrue(comentarios.Contains(dtoC1));
        }

        /// <summary>
        ///A test for show tags of one comment, the comment doesnt exist.
        ///</summary>
        [Test]
        public void PR_IN_28()
        {
            try
            {
                EventoService.MostrarComentariosEtiqueta(NON_EXISTING_TAG_NAME);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void PR_IN_29()
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

            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

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

            Collection<Etiqueta> nube = EventoService.NubeEtiquetas();

            Assert.AreEqual(5, nube.Count());
            Assert.AreEqual(e2, nube[0]);
            Assert.AreEqual(e4, nube[1]);
            Assert.AreEqual(e1, nube[2]);
            Assert.AreEqual(e3, nube[3]);
            Assert.AreEqual(e5, nube[4]);

        }



        /// <summary>
        ///A test for create random tags
        ///</summary>
        [Test]
        public void PR_AL_10()
        {
            List<string> cadenasRepetidas = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string s = GeneradoresAleatorios.randomDistinctString(cadenasRepetidas, 15);
                EventoService.CrearEtiqueta(s);
            }
        }
        #endregion
    }
}
