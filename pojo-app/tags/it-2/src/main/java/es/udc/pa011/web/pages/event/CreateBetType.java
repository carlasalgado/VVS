package es.udc.pa011.web.pages.event;

import java.util.HashSet;
import java.util.Locale;
import java.util.Set;

import org.apache.tapestry5.annotations.Component;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.corelib.components.Form;
import org.apache.tapestry5.corelib.components.TextField;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_USERS)
public class CreateBetType {
	
	private Set<Option> options= new HashSet<Option>();	
	private Long eventId;
	private Event event;
	private BetType bettype;
	
	@Property
	private String question;
	
	@Property
	private int multWinOptions;	
	
	@SessionState(create=false)
	private UserSession userSession;
	
	@Inject
	private EventService eventService;
	
	@InjectPage
	private BetTypeCreated betTypeCreated;
	
	public Long getEventId(){
		return eventId;
	}
	
	public void setEventId(Long eventId){
		this.eventId = eventId;
	}
	
	public Event getEvent() {
		return event;
	}

	public void setEvent(Event event) {
		this.event = event;
	}

	public BetType getBettype() {
		return bettype;
	}

	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	Object onPassivate(){
		return eventId;
	}
	
	void onActivate(Long eventId){
		this.eventId = eventId;
	}
	
	Object onSuccess() throws InstanceNotFoundException{
		event = eventService.findEvent(eventId);
		
		if (multWinOptions <= 0)
			bettype = new BetType(question, event, false, options);
		else 
			bettype = new BetType(question, event, true, options);
		
		eventService.createBetType(eventId,bettype);
		
		betTypeCreated.setBetTypeId(bettype.getBetTypeId());
		
		return betTypeCreated;

    }
}
