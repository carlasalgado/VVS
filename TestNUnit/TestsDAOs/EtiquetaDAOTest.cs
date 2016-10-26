using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsDAOs
{
    [TestFixture]
    public class EtiquetaDAOTest
    {
        private static IUnityContainer container;

        TransactionScope transaction;
        private static IEtiquetaDao etiquetaDao;
        private static IComentarioDao comentarioDao;
        private static IEventoDao eventoDao;
        private static IUserProfileDao usuarioDao;

        private TestContext testContextInstance;

        //Etiquetas Creadas
        private Etiqueta etiqueta1;
        private Etiqueta etiqueta2;
        private Etiqueta etiqueta3;
        private Etiqueta etiqueta4;
        private Etiqueta etiqueta5;

        private Comentario comentario1;
        private Comentario comentario2;
        private Comentario comentario3;

        private Evento evento1;

        private UserProfile userProfile;

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
            etiquetaDao = container.Resolve<IEtiquetaDao>();
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
            evento1.nombre = "Evento 1";
            evento1.fecha = DateTime.Now;
            evento1.reseña = "Reseña evento1";
            eventoDao.Create(evento1);

            //Etiqueta 1
            etiqueta1 = new Etiqueta();
            etiqueta1.nombre = "Etiqueta 1";
            etiquetaDao.Create(etiqueta1);

            //Etiqueta 2
            etiqueta2 = new Etiqueta();
            etiqueta2.nombre = "Etiqueta 2";
            etiquetaDao.Create(etiqueta2);

            //Etiqueta 3
            etiqueta3 = new Etiqueta();
            etiqueta3.nombre = "Etiqueta 3";
            etiquetaDao.Create(etiqueta3);

            //Etiqueta 4
            etiqueta4 = new Etiqueta();
            etiqueta4.nombre = "Etiqueta 4";
            etiquetaDao.Create(etiqueta4);

            //Etiqueta 5
            etiqueta5 = new Etiqueta();
            etiqueta5.nombre = "Etiqueta 5";
            etiquetaDao.Create(etiqueta5);


            //Comentarios

            //Comentario 1
            comentario1 = new Comentario();
            comentario1.UserProfile = userProfile;
            comentario1.Evento = evento1;
            comentario1.fecha = DateTime.Now;
            comentario1.texto = "Esto es un comentario 1";
            comentario1.Etiqueta.Add(etiqueta1);
            comentario1.Etiqueta.Add(etiqueta2);
            comentarioDao.Create(comentario1);

            //Comentario 2
            comentario2 = new Comentario();
            comentario2.UserProfile = userProfile;
            comentario2.Evento = evento1;
            comentario2.fecha = DateTime.Now;
            comentario2.texto = "Esto es el comentario 2";
            comentario2.Etiqueta.Add(etiqueta1);
            comentario2.Etiqueta.Add(etiqueta2);
            comentario2.Etiqueta.Add(etiqueta5);
            comentario2.Etiqueta.Add(etiqueta3);
            comentarioDao.Create(comentario2);

            //Comentario 3
            comentario3 = new Comentario();
            comentario3.UserProfile = userProfile;
            comentario3.Evento = evento1;
            comentario3.fecha = DateTime.Now;
            comentario3.texto = "Esto es el comentario 3";
            comentario3.Etiqueta.Add(etiqueta2);
            comentario3.Etiqueta.Add(etiqueta5);
            comentarioDao.Create(comentario3);

        }

        [TearDown]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }




        [Test]
        public void PR_UN_11()
        {
            Etiqueta obtenida = etiquetaDao.BuscarPorNombre("Etiqueta 1");
            Assert.AreEqual(etiqueta1, obtenida);
        }

        [Test]
        public void PR_UN_12()
        {
            /* Orden de aparición:
             * Etiqueta 2: 3
             * Etiqueta 1: 2 
             * Etiqueta 5: 2
             * Etiqueta 3: 1
             * Etiqueta 4: 0
             */
            List<Etiqueta> obtenidas = etiquetaDao.NubeEtiquetas();

            Assert.IsTrue(obtenidas.Count == 5);
            Assert.AreEqual(obtenidas[0], etiqueta2);
            Assert.AreEqual(obtenidas[1], etiqueta1);
            Assert.AreEqual(obtenidas[2], etiqueta5);
            Assert.AreEqual(obtenidas[3], etiqueta3);
            Assert.AreEqual(obtenidas[4], etiqueta4);

        }
    }
}
