using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GrupoDao
{
    public class GrupoDaoEntityFramework :
        GenericDaoEntityFramework<Grupo, Int64>, IGrupoDao
    {
        public GrupoDaoEntityFramework() { }

        public List<Grupo> BuscarPorUsuario(long usrId)
        {

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var resultado =
                (from u in userProfiles
                 where u.usrId == usrId
                 select u);

            UserProfile userProfile = resultado.FirstOrDefault();

            return userProfile.Grupo.ToList();
        }

        public Grupo BuscarPorNombre(string nombre)
        {
            DbSet<Grupo> grupos = Context.Set<Grupo>();

            var resultado =
                (from g in grupos
                 where g.nombre == nombre
                 select g);

            Grupo grupo = resultado.FirstOrDefault();

            if (grupo == null)
                throw new InstanceNotFoundException(nombre,
                    typeof(Grupo).FullName);

            return grupo;
        }

        public List<Grupo> MostrarGrupos(int startIndex, int count)
        {

            DbSet<Grupo> grupos = Context.Set<Grupo>();

            var resultado =
                (from g in grupos
                 orderby g.nombre
                 select g).Skip(startIndex).Take(count);

            return resultado.ToList();
        }

    }
}