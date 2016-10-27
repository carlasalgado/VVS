using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventoDao;
using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public class EventoService : IEventoService
    {

        #region DAOS
        [Dependency]
        public IComentarioDao comentarioDao { get; set; }
        [Dependency]
        public IEventoDao eventoDao { get; set; }
        [Dependency]
        public IUserProfileDao userProfileDao { get; set; }
        [Dependency]
        public IRecomendacionDao recomendacionDao { get; set; }
        [Dependency]
        public IEtiquetaDao etiquetaDao { get; set; }
        [Dependency]
        public IGrupoDao grupoDao { get; set; }
        #endregion

        #region Evento
        public BloqueEventos BusquedaEventos(String busqueda, int startIndex = 0, int count = 0)
        {
            /*
                * Find count+1 accounts to determine if there exist more accounts above
                * the specified range.
                */
            List<String> palabrasClave = new List<string>();
            Collection<String> collPalabrasClave = new Collection<string>();

            palabrasClave.AddRange(busqueda.Split(' '));

            foreach (String s in palabrasClave)
            {
                collPalabrasClave.Add(s);
            }

            Collection<Evento> eventos = new Collection<Evento>();
            if (busqueda.Equals(""))
            {
                if (count > 0)
                    eventos = eventoDao.BuscarEventos(startIndex, count + 1);
                else
                    eventos = eventoDao.BuscarEventos();
            }
            else
            {
                if (count > 0)
                    eventos = eventoDao.BuscarEventos(collPalabrasClave, startIndex, count + 1);
                else
                    eventos = eventoDao.BuscarEventos(collPalabrasClave);
            }
            Collection<EventoDTO> eventosDTO = new Collection<EventoDTO>();
            EventoDTO dto = new EventoDTO();

            bool existenMasEventos = false;

            if (count > 0)
            {
                existenMasEventos = (eventos.Count == count + 1);

                if (existenMasEventos)
                    eventos.RemoveAt(count);
            }

            foreach (Evento e in eventos)
            {
                dto.idEvento = e.idEvento;
                dto.nombre = e.nombre;
                dto.reseña = e.reseña;
                dto.fecha = e.fecha;

                eventosDTO.Add(new EventoDTO(dto));
            }

            return new BloqueEventos(eventosDTO, existenMasEventos);
        }

        public EventoDTO BuscarEvento(long idEvento)
        {

            EventoDTO eventoDto = new EventoDTO();

            Evento evento = eventoDao.Find(idEvento);

            if (evento == null)
                throw new InstanceNotFoundException(idEvento,
                    typeof(Evento).FullName);

            eventoDto.idEvento = evento.idEvento;
            eventoDto.nombre = evento.nombre;
            eventoDto.fecha = evento.fecha;
            eventoDto.reseña = evento.reseña;

            return eventoDto;
        }
        #endregion

        #region Comentarios
        public long AnadirComentario(long idEvento, long idUsuario, String texto)
        {

            /* Buscar Evento y usuario*/
            Evento evento = eventoDao.Find(idEvento);
            
            if (evento == null)
                throw new InstanceNotFoundException(idEvento,
                    typeof(Evento).FullName);

            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            Comentario comentario = new Comentario();

            comentario.Evento = evento;
            comentario.UserProfile = usuario;
            comentario.texto = texto;
            comentario.fecha = DateTime.Now;

            comentarioDao.Create(comentario);

            return comentario.idComentario;

        }

        public void ModificarComentario(long idComentario, long idUsuario, String texto)
        {

            /* Buscar comentario y usuario*/
            Comentario comentario = comentarioDao.Find(idComentario);

            if (comentario == null)
                throw new InstanceNotFoundException(idComentario,
                    typeof(Comentario).FullName);

            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            //El usuario tiene que ser el Autor del comentario
            if (comentario.UserProfile.Equals(usuario))
            {
                comentario.texto = texto;
                comentarioDao.Update(comentario);
            }
            else
            {
                //El usuario no es el propietario del comentario
                throw new UsuarioNoPropietarioException(idUsuario, idComentario);
            }
        }

        public void EliminarComentario(long idComentario, long idUsuario)
        {
            /* Buscar comentario y usuario*/
            Comentario comentario = comentarioDao.Find(idComentario);

            if (comentario == null)
                throw new InstanceNotFoundException(idComentario,
                    typeof(Comentario).FullName);

            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            //El usuario tiene que ser el Autor del comentario
            if (comentario.UserProfile.Equals(usuario))
                comentarioDao.Remove(comentario.idComentario);
            else
                //El usuario no es el propietario del comentario
                throw new UsuarioNoPropietarioException(idUsuario, idComentario);
        }

        public BloqueComentarios VerComentarios(long idEvento, int startIndex = 0, int count = 0)
        {
            Collection<ComentarioDTO> comentariosDTO = new Collection<ComentarioDTO>();
            Collection<Comentario> comentarios;
            bool existenMasComentarios;

            ComentarioDTO dto = new ComentarioDTO();

            /* Buscar Evento*/
            Evento evento = eventoDao.Find(idEvento);

            if (evento == null)
                throw new InstanceNotFoundException(idEvento,
                    typeof(Evento).FullName);

            if (count == 0)
            {
                comentarios = comentarioDao.VerComentarios(idEvento, startIndex, 0);
                existenMasComentarios = false;
            }
            else
            {
                comentarios = comentarioDao.VerComentarios(idEvento, startIndex, count + 1);
                existenMasComentarios = (comentarios.Count == count + 1);
            }

            if (existenMasComentarios)
                comentarios.RemoveAt(count);

            foreach (Comentario c in comentarios)
            {
                dto.idComentario = c.idComentario;
                dto.login = c.UserProfile.loginName;
                dto.fecha = c.fecha;
                dto.texto = c.texto;

                comentariosDTO.Add(new ComentarioDTO(dto));
            }
            return new BloqueComentarios(comentariosDTO, existenMasComentarios);
        }

        public ComentarioDTO BuscarComentario(long idComentario)
        {

            Comentario comentario = comentarioDao.Find(idComentario);

            if (comentario == null)
                throw new InstanceNotFoundException(idComentario,
                    typeof(Comentario).FullName);

            ComentarioDTO comentarioDto = new ComentarioDTO();

            comentarioDto.idComentario = comentario.idComentario;
            comentarioDto.login = comentario.UserProfile.loginName;
            comentarioDto.fecha = comentario.fecha;
            comentarioDto.texto = comentario.texto;


            return comentarioDto;
        }

        public Collection<ComentarioDTO> BuscarComentarioPorUsuario(long idUsuario, long idEvento)
        {

            Collection<ComentarioDTO> comentariosDTO = new Collection<ComentarioDTO>();

            ComentarioDTO dto = new ComentarioDTO();

            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            Collection<Comentario> comentarios = comentarioDao.BuscarPorUsuario(idUsuario, idEvento);

            foreach (Comentario c in comentarios)
            {
                dto.idComentario = c.idComentario;
                dto.login = c.UserProfile.loginName;
                dto.fecha = c.fecha;
                dto.texto = c.texto;

                comentariosDTO.Add(new ComentarioDTO(dto));
            }

            return comentariosDTO;
        }
        #endregion

        #region Recomendaciones
        public Recomendacion RecomendarEvento(long idEvento, Collection<Grupo> grupos, String textoRecomendacion)
        {

            //Si un grupo de la lista no existe, devuelve InstanceNotFoundException
            Boolean eventoRecomendado = false;
            Recomendacion recomendacion = new Recomendacion();
            Evento evento = new Evento();

            /* Comprobamos que la lista de grupos no estéa vacía*/
            if (grupos.Count == 0)
                throw new SinGruposException();

            /* Buscar Evento */
            evento = eventoDao.Find(idEvento);

            if (evento == null)
                throw new InstanceNotFoundException(idEvento,
                    typeof(Evento).FullName);

            //Comprobamos todos los grupos si el evento ha sido recomendado
            //Si no es así, lo recomendamos
            foreach (Grupo g in grupos)
            {
                foreach (Recomendacion r in g.Recomendacion)
                {
                    if (r.Evento.idEvento.Equals(idEvento))
                    {
                        eventoRecomendado = true;
                    }
                }
                if (!eventoRecomendado)
                {
                    recomendacion.Grupo.Add(g);
                }
                eventoRecomendado = false;
            }

            recomendacion.Evento = evento;
            recomendacion.texto = textoRecomendacion;
            recomendacion.fecha = DateTime.Now;

            recomendacionDao.Create(recomendacion);

            return recomendacion;
        }

        public Collection<RecomendacionDTO> MostrarRecomendaciones(long idUsuario)
        {

            List<Recomendacion> recomendaciones = new List<Recomendacion>();
            List<Evento> eventos = new List<Evento>();
            List<RecomendacionDTO> recomendacionDTO = new List<RecomendacionDTO>();

            Collection<RecomendacionDTO> collDTO = new Collection<RecomendacionDTO>();

            UserProfile usuario = userProfileDao.Find(idUsuario);

            if (usuario == null)
                throw new InstanceNotFoundException(idUsuario,
                    typeof(UserProfile).FullName);

            Collection<Grupo> grupos = grupoDao.BuscarPorUsuario(idUsuario);

            /* Recoge todas las recomendaciones de cada grupo del usuario */
            foreach (Grupo g in grupos)
            {
                recomendaciones.AddRange(g.Recomendacion);
            }

            /* Recoge los eventos de las recomendaciones */
            foreach (Recomendacion r in recomendaciones)
            {
                if (!eventos.Contains(r.Evento))
                {
                    RecomendacionDTO dto = new RecomendacionDTO();
                    dto.idEvento = r.Evento.idEvento;
                    dto.nombre = r.Evento.nombre;
                    dto.reseña = r.Evento.reseña;
                    dto.fecha = r.Evento.fecha;
                    dto.fechaRecomendacion = r.fecha;
                    dto.recomendacion = r.texto;

                    recomendacionDTO.Add(dto);
                    eventos.Add(r.Evento);
                }
            }

            foreach (RecomendacionDTO r in recomendacionDTO.OrderByDescending(reco => reco.fechaRecomendacion).ToList())
            {
                collDTO.Add(r);
            }    

            return collDTO;
        }
        public bool GrupoRecomendado(long idEvento, long idGrupo)
        {
            Evento evento = eventoDao.Find(idEvento);

            if (evento == null)
                throw new InstanceNotFoundException(idEvento,
                    typeof(Evento).FullName);

            Grupo grupo = grupoDao.Find(idGrupo);

            if (grupo == null)
                throw new InstanceNotFoundException(idGrupo,
                    typeof(Grupo).FullName);

            return recomendacionDao.BuscarRecomendacion(idGrupo, idEvento);
        }
        #endregion

        #region Etiquetas
        public Etiqueta CrearEtiqueta(String nombreEtiqueta)
        {
            /* Convertimos el nombre en minusculas y comprobamos que no estéa duplicada */
            if (etiquetaDao.BuscarPorNombre(nombreEtiqueta.ToLower()) != null)
                throw new DuplicateInstanceException(nombreEtiqueta,
                    typeof(Etiqueta).FullName);
            else
            {
                Etiqueta etiqueta = new Etiqueta();
                etiqueta.nombre = nombreEtiqueta.ToLower();
                etiquetaDao.Create(etiqueta);
                return etiqueta;
            }
        }

        public Etiqueta EtiquetaPorId(long idEtiqueta)
        {
            Etiqueta etiqueta = etiquetaDao.Find(idEtiqueta);

            if (etiqueta == null)
                throw new InstanceNotFoundException(idEtiqueta,
                    typeof(Etiqueta).FullName);

            return etiqueta;
        }

        public void AnadirEtiqueta(long idComentario, Collection<Etiqueta> etiquetas)
        {

            Comentario comentario = comentarioDao.Find(idComentario);

            if (comentario == null)
                throw new InstanceNotFoundException(idComentario,
                    typeof(Comentario).FullName);

            /* Quitamos todas las etiquetas del comentario */
            comentario.Etiqueta.Clear();
            comentarioDao.Update(comentario);

            /* Añadimos todas las etiquetas nuevas */
            foreach (Etiqueta e in etiquetas)
                comentario.Etiqueta.Add(e);

            comentarioDao.Update(comentario);
        }

        public Collection<Etiqueta> EtiquetasDeComentario(long idComentario)
        {
            Comentario comentario = comentarioDao.Find(idComentario);
            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();

            if (comentario == null)
                throw new InstanceNotFoundException(idComentario,
                    typeof(Comentario).FullName);

            foreach (Etiqueta e in comentario.Etiqueta.ToList<Etiqueta>())
            {
                etiquetas.Add(e);
            }
            return etiquetas;
        }

        public Collection<Etiqueta> NubeEtiquetas()
        {
            Collection<Etiqueta> etiquetas = new Collection<Etiqueta>();
            foreach (Etiqueta e in etiquetaDao.NubeEtiquetas())
            {
                etiquetas.Add(e);
            }
            return etiquetas;
            
        }

        public Collection<ComentarioDTO> MostrarComentariosEtiqueta(String nombreEtiqueta)
        {
            Collection<ComentarioDTO> comentariosDTO = new Collection<ComentarioDTO>();
            ComentarioDTO dto = new ComentarioDTO();

            Etiqueta etiqueta = etiquetaDao.BuscarPorNombre(nombreEtiqueta);

            if (etiqueta == null)
                throw new InstanceNotFoundException(nombreEtiqueta,
                    typeof(Etiqueta).FullName);

            foreach (Comentario comentario in etiqueta.Comentario.ToList<Comentario>())
            {
                dto.idComentario = comentario.idComentario;
                dto.login = comentario.UserProfile.loginName;
                dto.fecha = comentario.fecha;
                dto.texto = comentario.texto;

                comentariosDTO.Add(new ComentarioDTO(dto));
            }
            return comentariosDTO;
        }
        #endregion
    }
}
