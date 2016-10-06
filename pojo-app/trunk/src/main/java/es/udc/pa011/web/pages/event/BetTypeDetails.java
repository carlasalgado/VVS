/**
 * 
 */
package es.udc.pa011.web.pages.event;

import java.text.DateFormat;
import java.text.Format;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

/* Las opciones pueden estar en los siguientes estados:
 * 	No resuelta: el evento no comenzo. No es ganadora ni perdedora
 *  Ganada: opcion ganadora
 *  Perdida: opcion perdedora
 */

public class BetTypeDetails {
	private String login;
	private Long userId;
	private Long bettypeId;
	
	private Set<Option> optionsSet = new HashSet<Option>();
	private List<Option> options;
	private Option option;
	private BetType bettype;
	
	/* Los eventos pueden haber comenzado o no */
	private boolean comenzado;
	
	/* Hay tres tipos de usuarios:
	 *   Registrados: user -- no administrador
	 *   No registrados: guest --no administrador
	 *   Administrador: admin
	 */
	private boolean admin;
	
	private Long est;

	@Inject
	private UserService userService;
	
	@Inject
	private EventService eventService;
	
	@Inject
	private Locale locale;
	
	@InjectPage
	private CreateOption createOption;
	
	@InjectPage
	private CheckListOptions clo;
	
	@SessionState(create=false)	
	UserSession userSession;
	
	public List<Option> getOptions() {
		return options;
	}
	
	public Option getOption() {
		return option;
	}
	
	public void setOption(Option option) {
		this.option = option;
	}	
	public Long getBettypeId() {
		return bettypeId;
	}

	public void setBettypeId(Long bettypeId) {
		this.bettypeId = bettypeId;
	}

	public Long getEst() {
		return est;
	}

	public void setEst(Long est) {
		this.est = est;
	}

	public BetType getBettype() {
		return bettype;
	}
	
	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	public boolean isAdmin() {
		return admin;
	}

	public void setAdmin(boolean admin) {
		this.admin = admin;
	}
	
	public boolean isComenzado() {
		return comenzado;
	}

	public void setComenzado(boolean comenzado) {
		this.comenzado = comenzado;
	}
	
	public boolean isSinResolver() {
		if (option == null)
			return false;
		else if (option.isWinner()== null)
				return true;
		else return false;
	}
	
	public boolean isGanada() {
		if (option.isWinner()!= null)
			if (option.isWinner())
				return true;
		return false;
	}

	
	public Format getFormat(){
		return DateFormat.getDateTimeInstance(DateFormat.LONG, DateFormat.MEDIUM, locale);
	}

	void onActivate(Long bettypeId) throws InstanceNotFoundException{
		this.bettypeId = bettypeId;
		options = new ArrayList<Option>();
		
		optionsSet = eventService.findBetType(bettypeId).getOptions();
		
		if (!optionsSet.isEmpty())
			options.addAll(optionsSet);
		
		if(userSession==null)
			admin=false;
		else{
			userId = userSession.getUserProfileId();
			
			if (userSession.getLogin().compareTo("admin")==0)
				admin = true;
			else 
				admin=false;
		}
		
		bettype = eventService.findBetType(bettypeId);
		Calendar dateEvent = bettype.getEvent().getDate();
		Calendar today = Calendar.getInstance(locale);
		
		if(dateEvent.before(today)){
			comenzado = true;
		}
		else{ comenzado = false;}
		
	}

}