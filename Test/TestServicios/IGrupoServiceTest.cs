using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
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
    [TestClass()]
    public class IGrupoServiceTest
    {
        private static IUnityContainer container;
        private static IGrupoService GrupoService;
        private static IGrupoDao GrupoDao;
        private static IUserProfileDao UserProfileDao;

        private static long NON_EXISTING_USER = -1;
        private static long NON_EXISTING_GROUP = -1;

        UserProfile user = new UserProfile();
        UserProfile user2 = new UserProfile();

        Grupo g1 = new Grupo();
        Grupo g2 = new Grupo();
        Grupo g3 = new Grupo();

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
            GrupoDao = container.Resolve<IGrupoDao>();
            UserProfileDao = container.Resolve<IUserProfileDao>();
            GrupoService = container.Resolve<IGrupoService>();
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
            user2 = new UserProfile();
            user2.loginName = "jj";
            user2.enPassword = "password";
            user2.firstName = "Juan Jose";
            user2.lastName = "Martinez";
            user2.email = "jj@acme.com";
            user2.language = "es";
            user2.country = "ES";

            UserProfileDao.Create(user2);

            g1.nombre = "Grupo 1";
            g1.descripcion = "Descripcion grupo 1";
            g2.nombre = "Grupo 2";
            g2.descripcion = "Descripcion grupo 2";
            g3.nombre = "Grupo 3";
            g3.descripcion = "Descripcion grupo 3";

        }
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_CrearGrupoTest()
        {
            Grupo grupoEsperado = g1;

            grupoEsperado.idGrupo = GrupoService.CrearGrupo(grupoEsperado, user.usrId);
            Grupo grupoObtenido = GrupoDao.Find(grupoEsperado.idGrupo);

            Assert.AreEqual(grupoEsperado, grupoObtenido);


            UserProfile usuario = UserProfileDao.Find(user.usrId);
            //Comprobamos que el usuario tiene el grupo creado en su lista
            Assert.AreEqual(usuario.Grupo.Count, 1);
            Assert.AreEqual(usuario.Grupo.FirstOrDefault<Grupo>(), grupoObtenido);

            //Comprobamos que el grupo contiene el usuario
            Assert.AreEqual(grupoObtenido.UserProfile.Count, 1);
            Assert.AreEqual(usuario, grupoObtenido.UserProfile.FirstOrDefault<UserProfile>());
        }

        /// <summary>
        ///A test for Create a duplicate group
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void ServicioGrupos_CrearGrupoDuplicadoTest()
        {
            Grupo duplicateGroup = new Grupo();
            duplicateGroup.nombre = g1.nombre;
            duplicateGroup.descripcion = g1.descripcion;

            GrupoService.CrearGrupo(g1, user.usrId);
            GrupoService.CrearGrupo(duplicateGroup, user.usrId);

        }

        /// <summary>
        ///A test for show groups
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_VerGruposTest()
        {
            List<GrupoDTO> gruposEsperados = new List<GrupoDTO>();
            GrupoService.CrearGrupo(g1, user.usrId);
            GrupoService.CrearGrupo(g2, user.usrId);
            GrupoService.CrearGrupo(g3, user2.usrId);

            GrupoDTO dto1 = new GrupoDTO();
            GrupoDTO dto2 = new GrupoDTO();
            GrupoDTO dto3 = new GrupoDTO();

            dto1.idGrupo = g1.idGrupo;
            dto1.nombre = g1.nombre;
            dto1.descripcion = g1.descripcion;
            dto1.nMiembros = g1.UserProfile.Count();
            dto1.nRecomendaciones = g1.Recomendacion.Count();

            dto2.idGrupo = g2.idGrupo;
            dto2.nombre = g2.nombre;
            dto2.descripcion = g2.descripcion;
            dto2.nMiembros = g2.UserProfile.Count();
            dto2.nRecomendaciones = g2.Recomendacion.Count();

            dto3.idGrupo = g3.idGrupo;
            dto3.nombre = g3.nombre;
            dto3.descripcion = g3.descripcion;
            dto3.nMiembros = g3.UserProfile.Count();
            dto3.nRecomendaciones = g3.Recomendacion.Count();

            gruposEsperados.Add(dto1);
            gruposEsperados.Add(dto2);
            gruposEsperados.Add(dto3);

            BloqueGrupos gruposObtenidos = GrupoService.VerGrupos(0, 20);

            Assert.AreEqual(3, gruposObtenidos.Grupos.Count);

            foreach (GrupoDTO g in gruposEsperados)
                Assert.IsTrue(gruposObtenidos.Grupos.Contains(g));
        }


        /// <summary>
        ///A test for add users to a group
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_AltaGrupoTest()
        {
            GrupoService.CrearGrupo(g1, user.usrId);

            GrupoService.AltaGrupo(user2.usrId, g1.idGrupo);

            Assert.IsTrue(g1.UserProfile.Contains(user2));
            Assert.IsTrue(user2.Grupo.Contains(g1));
        }


        /// <summary>
        ///A test for add users to a group Non Existing User
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_AltaGrupoUsuarioNoExisteTest()
        {

            GrupoService.AltaGrupo(NON_EXISTING_USER, g1.idGrupo);
        }

        /// <summary>
        ///A test for add users to a group Non Existing Group
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_AltaGrupoNoExisteTest()
        {
            GrupoService.AltaGrupo(user2.usrId, NON_EXISTING_GROUP);
        }

        /// <summary>
        ///A test for delete one users to a group
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_BajaGrupoTest()
        {
            GrupoService.CrearGrupo(g1, user.usrId);
            GrupoService.AltaGrupo(user2.usrId, g1.idGrupo);
            GrupoService.BajaGrupo(user2.usrId, g1.idGrupo);

            Assert.IsFalse(g1.UserProfile.Contains(user2));
            Assert.IsFalse(user2.Grupo.Contains(g2));
        }

        /// <summary>
        ///A test for delete users to a group Non Existing User
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_BajaGrupoUsuarioNoExisteTest()
        {
            GrupoService.BajaGrupo(NON_EXISTING_USER, g1.idGrupo);
        }

        /// <summary>
        ///A test for delete users to a group Non Existing Group
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_BajaGrupoNoExisteTest()
        {
            GrupoService.BajaGrupo(user2.usrId, NON_EXISTING_GROUP);
        }

        /// <summary>
        ///A test for delete users to a group, last user
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_BajaGrupoVacioTest()
        {
            GrupoService.CrearGrupo(g1, user.usrId);
            GrupoService.BajaGrupo(user.usrId, g1.idGrupo);

            Assert.AreEqual(GrupoService.VerGrupos(0, 20).Grupos.Count, 0);
        }

        /// <summary>
        ///A test for show user groups
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_BuscarPorUsuarioTest()
        {
            List<GrupoDTO> gruposEsperados = new List<GrupoDTO>();

            GrupoDTO dto1 = new GrupoDTO();
            GrupoDTO dto2 = new GrupoDTO();
            
            dto1.idGrupo = GrupoService.CrearGrupo(g1, user.usrId);
            dto2.idGrupo = GrupoService.CrearGrupo(g2, user.usrId);
            
            dto1.nombre = g1.nombre;
            dto1.descripcion = g1.descripcion;
            dto1.nMiembros = g1.UserProfile.Count();
            dto1.nRecomendaciones = g1.Recomendacion.Count();

            dto2.nombre = g2.nombre;
            dto2.descripcion = g2.descripcion;
            dto2.nMiembros = g2.UserProfile.Count();
            dto2.nRecomendaciones = g2.Recomendacion.Count();

            gruposEsperados.Add(dto1);
            gruposEsperados.Add(dto2);

            GrupoService.CrearGrupo(g3, user2.usrId);

            List<GrupoDTO> gruposObtenidos = GrupoService.BuscarPorUsuario(user.usrId);

            Assert.AreEqual(2, gruposObtenidos.Count);

            foreach (GrupoDTO g in gruposEsperados)
                Assert.IsTrue(gruposObtenidos.Contains(g));
        }

        /// <summary>
        ///A test for show user groups, inexistent user
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_BuscarPorUsuarioNoExistenteTest()
        {
            GrupoService.BuscarPorUsuario(NON_EXISTING_USER);
        }

        /// <summary>
        ///A test for find group by id
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_BuscarUnGrupoDTOPorIdTest()
        {
            GrupoDTO dtoEsperado = new GrupoDTO();

            dtoEsperado.idGrupo = GrupoService.CrearGrupo(g1, user.usrId);

            dtoEsperado.nombre = g1.nombre;
            dtoEsperado.descripcion = g1.descripcion;
            dtoEsperado.nMiembros = g1.UserProfile.Count();
            dtoEsperado.nRecomendaciones = g1.Recomendacion.Count();

            GrupoDTO dtoObtenido = GrupoService.BuscarGrupo(g1.idGrupo);

            Assert.AreEqual(dtoEsperado, dtoObtenido);

            
        }

        /// <summary>
        ///A test for find group by non existing id
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_BuscarUnGrupoDTONoExistenteTest()
        {
            GrupoService.BuscarGrupo(NON_EXISTING_GROUP);
        }

        /// <summary>
        ///A test for find group by id
        ///</summary>
        [TestMethod()]
        public void ServicioGrupos_BuscarUnGrupoPorIdTest()
        {
            Grupo grupoEsperado = g1;

            GrupoService.CrearGrupo(g1, user.usrId);

            Grupo grupoObtenido = GrupoService.BuscarUnGrupo(g1.idGrupo);

            Assert.AreEqual(grupoEsperado, grupoObtenido);


        }

        /// <summary>
        ///A test for find group by non existing id
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ServicioGrupos_BuscarUnGrupoNoExistenteTest()
        {
            GrupoService.BuscarUnGrupo(NON_EXISTING_GROUP);
        }

    }

}
