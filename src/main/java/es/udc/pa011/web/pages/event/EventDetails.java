/**
 * 
 */
package es.udc.pa011.web.pages.event;

import java.text.DateFormat;
import java.text.Format;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

import org.apache.tapestry5.annotations.Component;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Persist;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.corelib.components.Submit;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class EventDetails {
	
	private Long eventId;
	
	/*Variables auxiliares*/
	private Set<BetType> bettypesSet = new HashSet<BetType>();
	private List<BetType> bettypes;
	
	private String login;
	private Long userId;
	private BetType bettype;
	private Event event;
	
	/*Variables de control*/
	private boolean admin;
	private boolean start;
	
	@Inject
	private UserService userService;
	
	@Inject
	private EventService eventService;
	
	@Inject
	private Locale locale;
	
	@InjectPage
	private CreateBetType createBetType;
	
	@SessionState(create=false)	
	UserSession userSession;
	
	public List<BetType> getBettypes() {
		return bettypes;
	}
	
	public BetType getBettype() {
		return bettype;
	}
	
	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	public void setEvent(Event event) {
		this.event = event;
	}
	
	public Event getEvent() {
		return event;
	}
	
	public void setEventId(Long eventId) {
		this.eventId = eventId;
	}
	
	public Long getEventId() {
		return eventId;
	}
	
	public boolean isAdmin() {
		return admin;
	}

	public void setAdmin(boolean admin) {
		this.admin = admin;
	}

	public boolean isStart() {
		return start;
	}

	public void setStart(boolean start) {
		this.start = start;
	}
	
	public Format getFormat(){
		return DateFormat.getDateTimeInstance(DateFormat.LONG, DateFormat.MEDIUM, locale);
	}
	
	void onActivate(Long eventId) throws InstanceNotFoundException{
		this.eventId = eventId;
		event = eventService.findEvent(eventId);
		bettypes = new ArrayList<BetType>();
		bettypesSet = event.getBetTypes();
		
		if (!bettypesSet.isEmpty())
			bettypes.addAll(bettypesSet);
		
		if(userSession!=null){
			userId = userSession.getUserProfileId();
			
			if (userSession.getLogin().compareTo("admin")==0)
				setAdmin(true);
			else setAdmin(false);
		}
		
		Calendar today = Calendar.getInstance();
		if (event.getDate().after(today))
			setStart(true);
		else setStart(false);
	}

	Object onSuccess() throws InstanceNotFoundException{
		createBetType.setEventId(eventId);
		return createBetType;
	}	
}