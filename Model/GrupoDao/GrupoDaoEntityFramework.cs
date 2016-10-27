using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Collection<Grupo> BuscarPorUsuario(long usrId)
        {

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();
            Collection<Grupo> grupos = new Collection<Grupo>();

            var resultado =
                (from u in userProfiles
                 where u.usrId == usrId
                 select u);

            UserProfile userProfile = resultado.FirstOrDefault();

            foreach (Grupo g in userProfile.Grupo.ToList())
                grupos.Add(g);
            return grupos;
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

        public Collection<Grupo> MostrarGrupos(int startIndex, int count)
        {

            DbSet<Grupo> grupos = Context.Set<Grupo>();
            Collection<Grupo> collGrupos = new Collection<Grupo>();

            var resultado =
                (from g in grupos
                 orderby g.nombre
                 select g).Skip(startIndex).Take(count);


            foreach(Grupo g in resultado.ToList())
            {
                collGrupos.Add(g);
            }
            return collGrupos;
        }

    }
}