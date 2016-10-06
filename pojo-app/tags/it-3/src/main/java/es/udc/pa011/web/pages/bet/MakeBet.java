/**
 * 
 */
package es.udc.pa011.web.pages.bet;

import java.util.Locale;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.betservice.BetService;
import es.udc.pa011.model.betservice.InputValidationException;
import es.udc.pa011.model.betservice.InvalidDateException;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.pages.user.Login;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_USERS)
public class MakeBet {
	
	private Long userId;
	private Long optionId;
	private Option option;
	
	@Property
	private double amount;	
	
	@SessionState(create=false)
	private UserSession userSession;
	
	@Inject
	private BetService betService;
	
	@Inject
	private EventService eventService;
	
	@Inject
	private Locale locale;
	
	@InjectPage
	private BetCreated betCreated;
	
	@InjectPage
	private Login pageLogin;
	
	public Option getOption() {
		return option;
	}
	
	public void setOption(Option option) {
		this.option = option;
	}	
	
	public Long getOptionId() {
		return optionId;
	}

	public void setOptionId(Long optionId) {
		this.optionId = optionId;
	}

	void onActivate(Long optionId) throws InstanceNotFoundException {
		this.optionId = optionId;
		this.option = eventService.findOption(optionId);
	}
	
	Object onSuccess() throws InstanceNotFoundException, InputValidationException, InvalidDateException{
		if(userSession==null)
			return pageLogin;
		else {
			userId = userSession.getUserProfileId();
			betService.createBet(amount, userId, optionId);
			return betCreated;
		}
    }
}