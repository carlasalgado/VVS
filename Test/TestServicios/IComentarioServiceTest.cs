using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ComentarioDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoService;
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



    /// <summary>
    ///This is a test class for IComentarioServiceTest and is intended
    ///to contain all Comentarios Unit Tests
    ///</summary>
    [TestClass()]
    public class IComentarioServiceTest
    {

        private static int NON_EXISTING_USER = -1;
        private static long NON_EXISTING_EVENT = -1;
        private static long NON_EXISTING_COMMENT = -1;

        private static IUnityContainer container;
        private static IEventoService EventoService;
        private static IEventoDao EventoDao;
        private static IComentarioDao ComentarioDao;
        private static IUserProfileDao UserProfileDao;

        UserProfile user = new UserProfile();
        UserProfile usuarioIncorrecto = new UserProfile();

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            container = TestManager.ConfigureUnityContainer("unity");
            EventoService = container.Resolve<IEventoService>();
            ComentarioDao = container.Resolve<IComentarioDao>();
            EventoDao = container.Resolve<IEventoDao>();
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
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        
        #region AñadirComentarios Test
        /// <summary>
        ///A test for Add Comment
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_AnadirComentarioTest()
        {
            String comentario = "Esto es un comentario del usuario 1 sobre el evento 1";
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, comentario);

            Comentario c = ComentarioDao.Find(idComentario);

            Assert.AreEqual(comentario, c.texto);
            Assert.AreEqual(user, c.UserProfile);
            Assert.AreEqual(evento1, c.Evento);

            Assert.AreEqual(user, c.UserProfile);
            Assert.AreEqual(evento1, c.Evento);
        }

        /// <summary>
        ///A test for Add Comment with one non existing user
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_AnadirComentarioUsuarioInexistenteTest()
        {
            EventoService.AnadirComentario(evento1.idEvento, NON_EXISTING_USER, "Esto es un comentario del usuario 1 sobre el evento 1");
        }

        /// <summary>
        ///A test for Add Comment with one non existing event
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_AnadirComentarioEventoInexistenteTest()
        {
            EventoService.AnadirComentario(NON_EXISTING_EVENT, user.usrId, "Esto es un comentario del usuario 1 sobre el evento 1");
        }
        #endregion

        #region ModificarComentarios Test
        /// <summary>
        ///A test for Update Comment
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_ModificarComentarioTest()
        {
            String comentario = "Esto es un comentario modificado.";
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            Comentario c = ComentarioDao.Find(idComentario);

            EventoService.ModificarComentario(c.idComentario, c.UserProfile.usrId, comentario);

            Assert.AreEqual(comentario, c.texto);
            Assert.AreEqual(user, c.UserProfile);
            Assert.AreEqual(evento1, c.Evento);
        }

        /// <summary>
        ///A test for Update Comment with one non existing user
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_ModificarComentarioUsuarioInexistenteTest()
        {
            String comentario = "Esto es un comentario modificado.";
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            Comentario c = ComentarioDao.Find(idComentario);

            EventoService.ModificarComentario(c.idComentario, NON_EXISTING_USER, comentario);
        }

        /// <summary>
        ///A test for Update Comment with one non existing event
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_ModificarComentarioEventoInexistenteTest()
        {
            String comentario = "Esto es un comentario modificado.";
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            Comentario c = ComentarioDao.Find(idComentario);

            EventoService.ModificarComentario(c.idComentario, NON_EXISTING_EVENT, comentario);
        }

        /// <summary>
        ///A test for Update Comment with one non owner event
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(UsuarioNoPropietarioException))]
        public void ServicioEvento_ModificarComentarioUsuarioIncorrectoTest()
        {
            String comentario = "Esto es un comentario modificado.";
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            Comentario c = ComentarioDao.Find(idComentario);

            EventoService.ModificarComentario(c.idComentario, usuarioIncorrecto.usrId, comentario);
        } 
        
        #endregion

        #region EliminarComentario Test
        /// <summary>
        ///A test for Delete Event
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_EliminarComentarioTest()
        {
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            EventoService.EliminarComentario(idComentario, user.usrId);

            ComentarioDao.Find(idComentario);
        }

        /// <summary>
        ///A test for Delete Comment with non owner user
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(UsuarioNoPropietarioException))]
        public void ServicioEvento_EliminarComentarioUsuarioNoPropietarioTest()
        {
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            EventoService.EliminarComentario(idComentario, usuarioIncorrecto.usrId);

        }

        /// <summary>
        ///A test for Delete Comment with non existing user
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_EliminarComentarioUsuarioInexistenteTest()
        {
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            EventoService.EliminarComentario(idComentario, NON_EXISTING_USER);
        }

        /// <summary>
        ///A test for Delete Comment with non existing Comment
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_EliminarComentarioComentarioInexistenteTest()
        {
            EventoService.EliminarComentario(NON_EXISTING_COMMENT, user.usrId);
        }
        #endregion

        #region VerComentarios
        /// <summary>
        ///A test for Search Comments
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_VerComentariosTest()
        {
            List<ComentarioDTO> comentariosEsperados = new List<ComentarioDTO>();
            long idComentario1 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 1 evento 1");
            long idComentario2 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 2 evento 1");
            long idComentario3 = EventoService.AnadirComentario(evento2.idEvento, user.usrId, "Comentario 1 evento 2");

            ComentarioDTO dto1 = new ComentarioDTO();
            ComentarioDTO dto2 = new ComentarioDTO();

            Comentario c1 = ComentarioDao.Find(idComentario1);
            dto1.idComentario = c1.idComentario;
            dto1.login = user.loginName;
            dto1.fecha = c1.fecha;
            dto1.texto = c1.texto;

            Comentario c2 = ComentarioDao.Find(idComentario1);
            dto2.idComentario = c2.idComentario;
            dto2.login = user.loginName;
            dto2.fecha = c2.fecha;
            dto2.texto = c2.texto;

            comentariosEsperados.Add(dto1);
            comentariosEsperados.Add(dto2);

            BloqueComentarios comentariosObtenidos = EventoService.VerComentarios(evento1.idEvento);

            Assert.AreEqual(2, comentariosObtenidos.Comentarios.Count);

            DateTime d = new DateTime();

            foreach (ComentarioDTO c in comentariosEsperados)
            {
                if (DateTime.Compare(d, new DateTime()) != 0)
                    Assert.IsTrue(DateTime.Compare(d, c.fecha) > 0);
                Assert.IsTrue(comentariosObtenidos.Comentarios.Contains(c));
            }
        }

        /// <summary>
        ///A test for Search Comments without comments
        ///</summary>
        [TestMethod()]
        public void ServicioEvento_VerComentariosSinComentariosTest()
        {
            BloqueComentarios comentariosObtenidos = EventoService.VerComentarios(evento1.idEvento);

            Assert.AreEqual(0, comentariosObtenidos.Comentarios.Count);
        }

        /// <summary>
        ///A test for Search Comments Non existing event
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioEvento_VerComentariosEventoInexistenteTest()
        {
            EventoService.VerComentarios(NON_EXISTING_EVENT);
        }
        #endregion

    }
}
