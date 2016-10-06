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

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_USERS)
public class CreateOption {
	
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
	private OptionCreated optionCreated;
	
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
		return bettypeId;
	}
	
	void onActivate(Long bettypeId){
		this.bettypeId = bettypeId;
	}
	
	Object onSuccess() throws InstanceNotFoundException{
		bettype = eventService.findBetType(bettypeId);
		
		eventService.createOption(name, share, bettypeId);
		
		return optionCreated;

    }

}
