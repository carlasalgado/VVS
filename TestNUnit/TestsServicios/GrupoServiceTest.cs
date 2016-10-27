using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.TestNUnit;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Collections.ObjectModel;

namespace es.udc.dotnet.practicamad.testnunit.testsservicios
{
    [TestFixture]
    public class gruposervicetest
    {

        private static IUnityContainer container;
        private static IGrupoService grupoService;
        private static IGrupoDao grupodao;
        private static IUserProfileDao userprofiledao;

        private static long NON_EXISTING_USER = -1;
        private static long NON_EXISTING_GROUP = -1;

        UserProfile user;
        UserProfile user2;

        Grupo g1;
        Grupo g2;
        Grupo g3;

        TransactionScope transaction;

        private TestContext testcontextinstance;

        /// <summary>
        ///gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext testcontext
        {
            get
            {
                return testcontextinstance;
            }
            set
            {
                testcontextinstance = value;
            }
        }

        //use classinitialize to run code before running the first test in the class
        [OneTimeSetUp]
        public static void myclassinitialize()
        {
            container = TestManager.ConfigureUnityContainer("unity");
            grupodao = container.Resolve<IGrupoDao>();
            userprofiledao = container.Resolve<IUserProfileDao>();
            grupoService = container.Resolve<IGrupoService>();
        }
        //use classcleanup to run code after all tests in a class have run
        [OneTimeTearDown]
        public static void myclasscleanup()
        {
            TestManager.ClearUnityContainer(container);
        }
        //use testinitialize to run code before running each test
        [SetUp]
        public void mytestinitialize()
        {
            transaction = new TransactionScope();

            //usuario
            user = new UserProfile();
            user.loginName = "jsmith";
            user.enPassword = "password";
            user.firstName = "john";
            user.lastName = "smith";
            user.email = "jsmith@acme.com";
            user.language = "en";
            user.country = "us";

            userprofiledao.Create(user);

            //usuario 2
            user2 = new UserProfile();
            user2.loginName = "jj";
            user2.enPassword = "password";
            user2.firstName = "juan jose";
            user2.lastName = "martinez";
            user2.email = "jj@acme.com";
            user2.language = "es";
            user2.country = "es";

            userprofiledao.Create(user2);
            g1 = new Grupo();
            g1.nombre = "grupo 1";
            g1.descripcion = "descripcion grupo 1";

            g2 = new Grupo();
            g2.nombre = "grupo 2";
            g2.descripcion = "descripcion grupo 2";

            g3 = new Grupo();
            g3.nombre = "grupo 3";
            g3.descripcion = "descripcion grupo 3";

        }
        //use testcleanup to run code after each test has run
        [TearDown]
        public void mytestcleanup()
        {
            transaction.Dispose();
        }

        /// <summary>
        ///a test for create
        ///</summary>
        [Test]
        public void PR_IN_42()
        {
            Grupo grupoesperado = g1;

            grupoesperado.idGrupo = grupoService.CrearGrupo(grupoesperado, user.usrId);
            Grupo grupoobtenido = grupodao.Find(grupoesperado.idGrupo);

            Assert.AreEqual(grupoesperado, grupoobtenido);

            UserProfile usuario = userprofiledao.Find(user.usrId);
            //comprobamos que el usuario tiene el grupo creado en su lista
            Assert.AreEqual(usuario.Grupo.Count, 1);
            Assert.IsTrue(usuario.Grupo.Contains(grupoobtenido));

            //comprobamos que el grupo contiene el usuario
            Assert.AreEqual(grupoobtenido.UserProfile.Count, 1);
            Assert.IsTrue(grupoobtenido.UserProfile.Contains(usuario));
        }

        /// <summary>
        ///A test for Create a duplicate group
        ///</summary>
        [Test]
        public void PR_IN_43()
        {
            Grupo duplicateGroup = new Grupo();
            duplicateGroup.nombre = g1.nombre;
            duplicateGroup.descripcion = g1.descripcion;

            grupoService.CrearGrupo(g1, user.usrId);
            try
            {
                grupoService.CrearGrupo(duplicateGroup, user.usrId);
                Assert.IsTrue(false);
            }
            catch (DuplicateInstanceException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for show groups
        ///</summary>
        [Test]
        public void PR_IN_44()
        {
            List<GrupoDTO> gruposEsperados = new List<GrupoDTO>();
            grupoService.CrearGrupo(g1, user.usrId);
            grupoService.CrearGrupo(g2, user.usrId);
            grupoService.CrearGrupo(g3, user2.usrId);

            GrupoDTO dto1 = new GrupoDTO();
            GrupoDTO dto2 = new GrupoDTO();
            GrupoDTO dto3 = new GrupoDTO();

            dto1.idGrupo = g1.idGrupo;
            dto1.nombre = g1.nombre;
            dto1.descripcion = g1.descripcion;
            dto1.nMiembros = g1.UserProfile.Count;
            dto1.nRecomendaciones = g1.Recomendacion.Count;

            dto2.idGrupo = g2.idGrupo;
            dto2.nombre = g2.nombre;
            dto2.descripcion = g2.descripcion;
            dto2.nMiembros = g2.UserProfile.Count;
            dto2.nRecomendaciones = g2.Recomendacion.Count;

            dto3.idGrupo = g3.idGrupo;
            dto3.nombre = g3.nombre;
            dto3.descripcion = g3.descripcion;
            dto3.nMiembros = g3.UserProfile.Count;
            dto3.nRecomendaciones = g3.Recomendacion.Count;

            gruposEsperados.Add(dto1);
            gruposEsperados.Add(dto2);
            gruposEsperados.Add(dto3);

            BloqueGrupos gruposObtenidos = grupoService.VerGrupos(0, 20);

            Assert.AreEqual(3, gruposObtenidos.Grupos.Count);

            foreach (GrupoDTO g in gruposEsperados)
                Assert.IsTrue(gruposObtenidos.Grupos.Contains(g));
        }


        /// <summary>
        ///A test for add users to a group
        ///</summary>
        [Test]
        public void PR_IN_45()
        {
            grupoService.CrearGrupo(g1, user.usrId);

            grupoService.AltaGrupo(user2.usrId, g1.idGrupo);

            Assert.IsTrue(g1.UserProfile.Contains(user2));
            Assert.IsTrue(user2.Grupo.Contains(g1));
        }


        /// <summary>
        ///a test for add users to a group non existing user
        ///</summary>
        [Test]
        public void PR_IN_46()
        {
            try
            {
                grupoService.AltaGrupo(NON_EXISTING_USER, g1.idGrupo);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for add users to a group Non Existing Group
        ///</summary>
        [Test]
        public void PR_IN_47()
        {
            try
            {
                grupoService.AltaGrupo(user2.usrId, NON_EXISTING_GROUP);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for delete one users to a group
        ///</summary>
        [Test]
        public void PR_IN_48()
        {
            grupoService.CrearGrupo(g1, user.usrId);
            grupoService.AltaGrupo(user2.usrId, g1.idGrupo);
            grupoService.BajaGrupo(user2.usrId, g1.idGrupo);

            Assert.IsFalse(g1.UserProfile.Contains(user2));
            Assert.IsFalse(user2.Grupo.Contains(g2));
        }

        /// <summary>
        ///A test for delete users to a group Non Existing User
        ///</summary>
        [Test]
        public void PR_IN_49()
        {
            try
            {
                grupoService.BajaGrupo(NON_EXISTING_USER, g1.idGrupo);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for delete users to a group Non Existing Group
        ///</summary>
        [Test]
        public void PR_IN_50()
        {
            try
            {
                grupoService.BajaGrupo(user2.usrId, NON_EXISTING_GROUP);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for delete users to a group, last user
        ///</summary>
        [Test]
        public void PR_IN_51()
        {
            grupoService.CrearGrupo(g1, user.usrId);
            grupoService.BajaGrupo(user.usrId, g1.idGrupo);

            Assert.AreEqual(grupoService.VerGrupos(0, 20).Grupos.Count, 0);
        }

        /// <summary>
        ///A test for show user groups
        ///</summary>
        [Test]
        public void PR_IN_52()
        {
            List<GrupoDTO> gruposEsperados = new List<GrupoDTO>();

            GrupoDTO dto1 = new GrupoDTO();
            GrupoDTO dto2 = new GrupoDTO();

            dto1.idGrupo = grupoService.CrearGrupo(g1, user.usrId);
            dto2.idGrupo = grupoService.CrearGrupo(g2, user.usrId);

            dto1.nombre = g1.nombre;
            dto1.descripcion = g1.descripcion;
            dto1.nMiembros = g1.UserProfile.Count;
            dto1.nRecomendaciones = g1.Recomendacion.Count;

            dto2.nombre = g2.nombre;
            dto2.descripcion = g2.descripcion;
            dto2.nMiembros = g2.UserProfile.Count;
            dto2.nRecomendaciones = g2.Recomendacion.Count;

            gruposEsperados.Add(dto1);
            gruposEsperados.Add(dto2);

            grupoService.CrearGrupo(g3, user2.usrId);

            Collection<GrupoDTO> gruposObtenidos = grupoService.BuscarPorUsuario(user.usrId);

            Assert.AreEqual(2, gruposObtenidos.Count);

            foreach (GrupoDTO g in gruposEsperados)
                Assert.IsTrue(gruposObtenidos.Contains(g));
        }

        /// <summary>
        ///A test for show user groups, inexistent user
        ///</summary>
        [Test]
        public void PR_IN_53()
        {
            try
            {
                grupoService.BuscarPorUsuario(NON_EXISTING_USER);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for find group by id
        ///</summary>
        [Test]
        public void PR_IN_54()
        {
            GrupoDTO dtoEsperado = new GrupoDTO();

            dtoEsperado.idGrupo = grupoService.CrearGrupo(g1, user.usrId);

            dtoEsperado.nombre = g1.nombre;
            dtoEsperado.descripcion = g1.descripcion;
            dtoEsperado.nMiembros = g1.UserProfile.Count;
            dtoEsperado.nRecomendaciones = g1.Recomendacion.Count;

            GrupoDTO dtoObtenido = grupoService.BuscarGrupo(g1.idGrupo);

            Assert.AreEqual(dtoEsperado, dtoObtenido);


        }

        /// <summary>
        ///A test for find group by non existing id
        ///</summary>
        [Test]
        public void PR_IN_55()
        {
            try
            {
                grupoService.BuscarGrupo(NON_EXISTING_GROUP);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        ///A test for find group by id
        ///</summary>
        [Test]
        public void PR_IN_56()
        {
            Grupo grupoEsperado = g1;

            grupoService.CrearGrupo(g1, user.usrId);

            Grupo grupoObtenido = grupoService.BuscarUnGrupo(g1.idGrupo);

            Assert.AreEqual(grupoEsperado, grupoObtenido);


        }

        /// <summary>
        ///A test for find group by non existing id
        ///</summary> 
        [Test]
        public void PR_IN_57()
        {
            try
            {
                grupoService.BuscarUnGrupo(NON_EXISTING_GROUP);
                Assert.IsTrue(false);
            }
            catch (InstanceNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }
    }

}
