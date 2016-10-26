using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class GrupoService : IGrupoService
    {
        [Dependency]
        public IGrupoDao grupoDao { private get; set; }
        [Dependency]
        public IUserProfileDao userProfileDao { private get; set; }

        public long CrearGrupo(Grupo grupo, long idUsuario)
        {
            try
            {
                //Comprobamos que el grupo no exista
                grupoDao.BuscarPorNombre(grupo.nombre);
                throw new DuplicateInstanceException(grupo.nombre,
                    typeof(Grupo).FullName);
            }
            catch (InstanceNotFoundException)
            {
                //Comprobamos que el usuario exista
                UserProfile usuario = userProfileDao.Find(idUsuario);
                //Creamos el grupo
                grupo.UserProfile.Add(usuario);
                grupoDao.Create(grupo);

                return grupo.idGrupo;
            }
        }

        public BloqueGrupos VerGrupos(int startIndex, int count)
        {
            GrupoDTO dto = new GrupoDTO();
            List<GrupoDTO> gruposDTO = new List<GrupoDTO>();
            List<Grupo> grupos = grupoDao.MostrarGrupos(startIndex, count + 1);
            List<Recomendacion> recomendaciones = new List<Recomendacion>();
            bool existenMasGrupos = (grupos.Count == count + 1);

            if (existenMasGrupos)
                grupos.RemoveAt(count);

            foreach (Grupo g in grupos)
            {
                dto.idGrupo = g.idGrupo;
                dto.nombre = g.nombre;
                dto.descripcion = g.descripcion;
                dto.nMiembros = g.UserProfile.Count();
                foreach (Recomendacion r in g.Recomendacion)
                {
                    if (!recomendaciones.Contains(r))
                        recomendaciones.Add(r);
                }
                dto.nRecomendaciones = recomendaciones.Count();

                gruposDTO.Add(new GrupoDTO(dto));
            }

            return new BloqueGrupos(gruposDTO, existenMasGrupos);
        }

        public void AltaGrupo(long idUsuario, long idGrupo)
        {
            Grupo grupo = grupoDao.Find(idGrupo);

            /* Buscar Grupo */
            if (grupo == null)
                throw new InstanceNotFoundException(idGrupo,
                    typeof(Grupo).FullName);

            /* Buscar Usuario */
            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            grupo.UserProfile.Add(usuario);
            grupoDao.Update(grupo);
        }

        public void BajaGrupo(long idUsuario, long idGrupo)
        {
            Grupo grupo = grupoDao.Find(idGrupo);

            /* Buscar Grupo */
            if (grupo == null)
                throw new InstanceNotFoundException(idGrupo,
                    typeof(Grupo).FullName);

            /* Buscar Usuario */
            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            //Eliminamos al usuario del grupo
            grupo.UserProfile.Remove(usuario);  

            /* Si el grupo se queda vacío, lo eliminamos */
            if (grupo.UserProfile.Count > 0)
                grupoDao.Update(grupo);
            else
                grupoDao.Remove(grupo.idGrupo);

        }
        
        public List<GrupoDTO> BuscarPorUsuario(long idUsuario)
        {
            GrupoDTO dtoGrupo = new GrupoDTO();
            List<GrupoDTO> gruposDTO = new List<GrupoDTO>();
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            /* Buscar Usuario */
            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            List<Grupo> grupos = grupoDao.BuscarPorUsuario(idUsuario);

            foreach (Grupo grupo in grupos)
            {
                dtoGrupo.idGrupo = grupo.idGrupo;
                dtoGrupo.nombre = grupo.nombre;
                dtoGrupo.descripcion = grupo.descripcion;
                dtoGrupo.nMiembros = grupo.UserProfile.Count();
                foreach (Recomendacion recomendacion in grupo.Recomendacion)
                {
                    if (!recomendaciones.Contains(recomendacion))
                        recomendaciones.Add(recomendacion);
                }
                dtoGrupo.nRecomendaciones = recomendaciones.Count();

                gruposDTO.Add(new GrupoDTO(dtoGrupo));
            }

            return gruposDTO;

        }

        public GrupoDTO BuscarGrupo(long idGrupo)
        {

            GrupoDTO dtoGrupo = new GrupoDTO();
            List<Recomendacion> recomendaciones = new List<Recomendacion>();

            Grupo grupo = grupoDao.Find(idGrupo);

            if (grupo == null)
                throw new InstanceNotFoundException(idGrupo,
                    typeof(Grupo).FullName);

            dtoGrupo.idGrupo = grupo.idGrupo;
            dtoGrupo.nombre = grupo.nombre;
            dtoGrupo.descripcion = grupo.descripcion;
            dtoGrupo.nMiembros = grupo.UserProfile.Count();

            foreach (Recomendacion recomendacion in grupo.Recomendacion)
            {
                if (!recomendaciones.Contains(recomendacion))
                    recomendaciones.Add(recomendacion);
            }
            dtoGrupo.nRecomendaciones = recomendaciones.Count();

            return dtoGrupo;
        }

        public Grupo BuscarUnGrupo(long idGrupo)
        {
            Grupo grupo = grupoDao.Find(idGrupo);

            if (grupo == null)
                throw new InstanceNotFoundException(idGrupo,
                    typeof(Grupo).FullName);

            return grupo;
        }
    }
}

