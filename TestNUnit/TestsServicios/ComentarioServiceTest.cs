using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventoService;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using FsCheck;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    [TestFixture]
    public class ComentarioServiceTest
    {
        private static int NON_EXISTING_USER = -1;
        private static long NON_EXISTING_EVENT = -1;
        private static long NON_EXISTING_COMMENT = -1;

        private static IUnityContainer container;
        private static IEventoService EventoService;
        private static IEventoDao EventoDao;
        private static IComentarioDao ComentarioDao;
        private static IUserProfileDao UserProfileDao;

        UserProfile user;
        UserProfile usuarioIncorrecto;
        Evento evento1;
        Evento evento2;
        Evento evento3;
        Evento evento4;

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
            UserProfileDao = container.Resolve<IUserProfileDao>();
            EventoDao = container.Resolve<IEventoDao>();
            ComentarioDao = container.Resolve<IComentarioDao>();
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
        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }



        #region AñadirComentarios Test



        /// <summary>
        ///A test for Add Comment
        ///</summary>
        [Test]
        public void PR_IN_01()
        {
            String comentario = "Esto es un comentario del usuario 1 sobre el evento 1";

            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, comentario);

            Comentario c = ComentarioDao.Find(idComentario);

            Assert.AreEqual(comentario, c.texto);
            Assert.AreEqual(user, c.UserProfile);
            Assert.AreEqual(evento1, c.Evento);
        }

        /// <summary>
        ///A test for Add Comment with one non existing user
        ///</summary>
        [Test]
        public void PR_IN_02()
        {
            try
            {
                EventoService.AnadirComentario(evento1.idEvento, NON_EXISTING_USER, "Esto es un comentario del usuario 1 sobre el evento 1");
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Add Comment with one non existing event
        ///</summary>
        [Test]
        public void PR_IN_03()
        {
            try
            {
                EventoService.AnadirComentario(NON_EXISTING_EVENT, user.usrId, "Esto es un comentario del usuario 1 sobre el evento 1");
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }


        /// <summary>
        ///A test for Add Comment Random Comment
        ///</summary>
        [Test]
        public void PR_AL_05()
        {
            for (int i = 0; i < 100; i++)
            {
                String comentario = GeneradoresAleatorios.RandomString();

                long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, comentario);

                Comentario c = ComentarioDao.Find(idComentario);

                Assert.AreEqual(comentario, c.texto);
                Assert.AreEqual(user, c.UserProfile);
                Assert.AreEqual(evento1, c.Evento);
            }
        }


        #endregion

        #region ModificarComentarios Test
        /// <summary>
        ///A test for Update Comment
        ///</summary>
        [Test]
        public void PR_IN_04()
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
        [Test]
        public void PR_IN_05()
        {
            try
            {
                String comentario = "Esto es un comentario modificado.";
                long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

                Comentario c = ComentarioDao.Find(idComentario);

                EventoService.ModificarComentario(c.idComentario, NON_EXISTING_USER, comentario);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Update Comment with one non existing event
        ///</summary>
        [Test]
        public void PR_IN_06()
        {
            try
            {
                String comentario = "Esto es un comentario modificado.";
                long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

                Comentario c = ComentarioDao.Find(idComentario);

                EventoService.ModificarComentario(NON_EXISTING_COMMENT, evento1.idEvento, comentario);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Update Comment with one non owner event
        ///</summary>
        [Test]
        public void PR_IN_07()
        {
            try
            {
                String comentario = "Esto es un comentario modificado.";
                long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

                Comentario c = ComentarioDao.Find(idComentario);

                EventoService.ModificarComentario(c.idComentario, usuarioIncorrecto.usrId, comentario);
                Assert.IsTrue(false);
            }
            catch (UsuarioNoPropietarioException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Update Comment with a random text
        ///</summary>
        [Test]
        public void PR_AL_06()
        {
            for (int i = 0; i < 100; i++)
            {
                String comentario = GeneradoresAleatorios.RandomString();
                long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

                Comentario c = ComentarioDao.Find(idComentario);

                EventoService.ModificarComentario(c.idComentario, c.UserProfile.usrId, comentario);

                Assert.AreEqual(comentario, c.texto);
                Assert.AreEqual(user, c.UserProfile);
                Assert.AreEqual(evento1, c.Evento);
            }
        }
        #endregion

        #region EliminarComentario Test
        /// <summary>
        ///A test for Delete Event
        ///</summary>
        [Test]
        public void PR_IN_08()
        {

            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");

            EventoService.EliminarComentario(idComentario, user.usrId);

            try
            {
                ComentarioDao.Find(idComentario);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Delete Comment with non owner user
        ///</summary>
        [Test]
        public void PR_IN_09()
        {
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");
            try
            {
                EventoService.EliminarComentario(idComentario, usuarioIncorrecto.usrId);
                Assert.IsTrue(false);
            }
            catch (UsuarioNoPropietarioException)
            {
                Assert.IsTrue(true);
            }

        }

        /// <summary>
        ///A test for Delete Comment with non existing user
        ///</summary>
        [Test]
        public void PR_IN_10()
        {
            long idComentario = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Primer Comentario!");
            try
            {
                EventoService.EliminarComentario(idComentario, NON_EXISTING_USER);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Delete Comment with non existing Comment
        ///</summary>
        [Test]
        public void PR_IN_11()
        {
            try
            {
                EventoService.EliminarComentario(NON_EXISTING_COMMENT, user.usrId);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }
        #endregion

        #region VerComentarios
        /// <summary>
        ///A test for Search Comments
        ///</summary>
        [Test]
        public void PR_IN_12()
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

            BloqueComentarios comentariosObtenidos = EventoService.VerComentarios(evento1.idEvento, 0, 0);

            Assert.IsFalse(comentariosObtenidos.ExistenMasComentarios);
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
        ///A test for Search Comments
        ///</summary>
        [Test]
        public void PR_IN_13()
        {
            List<ComentarioDTO> comentariosEsperados = new List<ComentarioDTO>();
            long idComentario1 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 1 evento 1");
            long idComentario2 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 2 evento 1");
            long idComentario3 = EventoService.AnadirComentario(evento2.idEvento, user.usrId, "Comentario 1 evento 2");

            BloqueComentarios comentariosObtenidos = EventoService.VerComentarios(evento1.idEvento, 0, 1);

            Assert.IsTrue(comentariosObtenidos.ExistenMasComentarios);
            Assert.AreEqual(1, comentariosObtenidos.Comentarios.Count);

        }
        /// <summary>
        ///A test for Search Comments without comments
        ///</summary>
        [Test]
        public void PR_IN_14()
        {
            BloqueComentarios comentariosObtenidos = EventoService.VerComentarios(evento1.idEvento, 0, 0);

            Assert.AreEqual(0, comentariosObtenidos.Comentarios.Count);
        }

        /// <summary>
        ///A test for Search Comments Non existing event
        ///</summary>
        [Test]
        public void PR_IN_15()
        {
            try
            {
                EventoService.VerComentarios(NON_EXISTING_EVENT, 0, 0);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }
        #endregion

        #region BuscarComentarios
        /// <summary>
        ///A test for Search Comments
        ///</summary>
        [Test]
        public void PR_IN_16()
        {
            long idComentario1 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 1 evento 1");

            ComentarioDTO dto1 = new ComentarioDTO();

            Comentario c1 = ComentarioDao.Find(idComentario1);
            dto1.idComentario = c1.idComentario;
            dto1.login = user.loginName;
            dto1.fecha = c1.fecha;
            dto1.texto = c1.texto;

            ComentarioDTO dtoExp = EventoService.BuscarComentario(idComentario1);

            Assert.AreEqual(dto1, dtoExp);
        }

        /// <summary>
        ///A test for Search Comments
        ///</summary>
        [Test]
        public void PR_IN_17()
        {
            try
            {
                EventoService.BuscarComentario(-1);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }

        }
        #endregion


        #region BuscarComentariosPorUsuario
        /// <summary>
        ///A test for Search comments by user 
        ///</summary>
        [Test]
        public void PR_IN_18()
        {
            List<ComentarioDTO> comentariosEsperados = new List<ComentarioDTO>();
            long idComentario1 = EventoService.AnadirComentario(evento1.idEvento, user.usrId, "Comentario 1 evento 1");
            long idComentario2 = EventoService.AnadirComentario(evento1.idEvento, usuarioIncorrecto.usrId, "Comentario 2 evento 1");
            long idComentario3 = EventoService.AnadirComentario(evento2.idEvento, user.usrId, "Comentario 1 evento 2");

            ComentarioDTO dto1 = new ComentarioDTO();
            ComentarioDTO dto3 = new ComentarioDTO();

            Comentario c1 = ComentarioDao.Find(idComentario1);
            dto1.idComentario = c1.idComentario;
            dto1.login = user.loginName;
            dto1.fecha = c1.fecha;
            dto1.texto = c1.texto;

            comentariosEsperados.Add(dto1);

            Collection<ComentarioDTO> comentariosObtenidos = EventoService.BuscarComentarioPorUsuario(user.usrId, evento1.idEvento);

            Assert.AreEqual(1, comentariosObtenidos.Count);
            Assert.IsTrue(comentariosObtenidos.Contains(dto1));
        }

        /// <summary>
        ///A test for Search comments by user with a random start index
        ///</summary>
        [Test]
        public void PR_AL_07()
        {

            for (int i = 0; i < 100; i++)
            {
                EventoService.VerComentarios(evento1.idEvento, GeneradoresAleatorios.RandomInteger(), 10);
            }

        }

        /// <summary>
        ///A test for Search comments by user with a random count
        ///</summary>
        [Test]
        public void PR_AL_08()
        {

            for (int i = 0; i < 100; i++)
            {
                EventoService.VerComentarios(evento1.idEvento, 0, GeneradoresAleatorios.RandomInteger());
            }
        }
        /// <summary>
        ///A test for Search comments by user 
        ///</summary>
        [Test]
        public void PR_IN_19()
        {

            try
            {
                EventoService.BuscarComentarioPorUsuario(-1, evento1.idEvento);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Search comments by user 
        ///</summary>
        [Test]
        public void PR_IN_20()
        {

            try
            {
                EventoService.BuscarComentarioPorUsuario(user.usrId, -1);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for Search comments by user 
        ///</summary>
        [Test]
        public void PR_IN_21()
        {
            List<ComentarioDTO> comentariosEsperados = new List<ComentarioDTO>();

            Collection<ComentarioDTO> comentariosObtenidos = EventoService.BuscarComentarioPorUsuario(user.usrId, evento1.idEvento);

            Assert.AreEqual(0, comentariosObtenidos.Count);
        }

        #endregion
    }
}
