package es.udc.pa011.web.pages.event;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_ADMIN)
public class CreateOption {
	private Long eventId;
	private Long bettypeId;
	private BetType bettype;
	private Option option;
	
	@Property
	private String name;
	
	@Property
	private double share;	
	
	@SessionState(create=false)
	private UserSession userSession;
	
	@Inject
	private EventService eventService;
	
	@InjectPage
	private CreateBetType createBetType;
	
	
	public Long getEventId() {
		return eventId;
	}

	public void setEventId(Long eventId) {
		this.eventId = eventId;
	}

	public Long getBettypeId() {
		return bettypeId;
	}

	public void setBettypeId(Long bettypeId) {
		this.bettypeId = bettypeId;
	}

	public BetType getBettype() {
		return bettype;
	}

	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	public Option getOption() {
		return option;
	}

	public void setOption(Option option) {
		this.option = option;
	}

	Object onPassivate(){
		return eventId;
	}
	
	void onActivate(Long eventId){
		this.eventId = eventId;
	}
	
	Object onSuccess() throws InstanceNotFoundException{		
		Option o = new Option(name, share);
		userSession.getOpciones().add(o);
		
		createBetType.setEventId(eventId);
		return createBetType;

    }

}
