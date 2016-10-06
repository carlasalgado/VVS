package es.udc.pa011.web.pages.event;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.OptionValueEncoder;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_ADMIN)
public class CreateBetType {
	private Long eventId;
	private Event event;
	private BetType bettype;
	
	@Property
	private int radioSelectedValue;
	
	@Property
	private String question;
	
	@Property
	private int multWinOptions;	
	
	@Property
	private Option option;
	
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

	Long onPassivate(){
		return eventId;
	}
	
	void onActivate(Long eventId){
		if (this.eventId == null)
			this.eventId = eventId;
	}
	
	public List<Option> getOptions(){	
		return userSession.getOpciones();
	}
	
	public OptionValueEncoder getEncoder() {
        return new OptionValueEncoder(eventService);
    }
	
	Object onSuccess() throws InstanceNotFoundException{
		event = eventService.findEvent(eventId);
		Set<Option> opts = new HashSet<Option>();
		opts.addAll(userSession.getOpciones());
		
		BetType objectBetType;
		if (radioSelectedValue == 0)
			objectBetType = new BetType(question, event, false, opts);
		else 
			objectBetType = new BetType(question, event, true, opts);
		
		eventService.createBetType(eventId,objectBetType);
		userSession.getOpciones().clear();
		return betTypeCreated;
    }
}

