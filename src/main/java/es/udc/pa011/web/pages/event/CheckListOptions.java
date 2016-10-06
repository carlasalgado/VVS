
package es.udc.pa011.web.pages.event;

import java.text.DateFormat;
import java.text.Format;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Set;

import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.OptionValueEncoder;
import es.udc.pa011.web.util.UserSession;

import org.apache.tapestry5.PersistenceConstants;
import org.apache.tapestry5.SelectModel;
import org.apache.tapestry5.ValueEncoder;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Persist;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;
import org.apache.tapestry5.services.SelectModelFactory;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.eventservice.ManyWinOptionsException;
import es.udc.pa011.model.option.Option;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_ADMIN)

public class CheckListOptions {
	@Property
	private Long bettypeId;
	
	@Property
    private SelectModel optionModel;
	
	@Inject
	private SelectModelFactory selectModelFactory;
	
	private BetType bettype;
	
	@Property
	private boolean checkList = true;
	
	@Property
	private Option option;

	@Property
	private List<Option> selectedOptions;
	
	@Persist
	@Property
    private Long radioSelectedValue;

	@Inject
	private EventService eventService;
	
	@Inject
	private Locale locale;
	
	@InjectPage
	private ListCheck listCheck;

	@SessionState(create=false)	
	UserSession userSession;
	
	public BetType getBettype(){
		return bettype;
	}

	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	public OptionValueEncoder getEncoder() {
        return new OptionValueEncoder(eventService);
    }
	
	public List<Option> getOptions() throws InstanceNotFoundException {
		Set<Option> optionsSet = eventService.findBetType(bettypeId).getOptions();
		List<Option> options = new ArrayList<Option>();

		if (!optionsSet.isEmpty()){
			for (Option o : optionsSet){
				if (o.isWinner()==null)
					options.add(o);
			}
		}
		
		return options;
	}
	
	public Format getFormat(){
		return DateFormat.getDateTimeInstance(DateFormat.LONG, DateFormat.MEDIUM, locale);
	}
	
	void onActivate(Long bettypeId) throws InstanceNotFoundException{
		this.bettypeId=bettypeId;	
		bettype = eventService.findBetType(bettypeId);
		
		if(bettype.isMultWinOptions())
			checkList = true;
		else checkList = false;
		
		optionModel = selectModelFactory.create(getOptions(), "option");
		
	}
	
	Object onSuccess() throws InstanceNotFoundException {
		
		List<Long> selectedOptionsId = new ArrayList<Long>();
		BetType bettype =  eventService.findBetType(bettypeId);
		
		if(bettype.isMultWinOptions())
			for (Option o : selectedOptions)
				selectedOptionsId.add(o.getOptionId());
		else 
			selectedOptionsId.add(radioSelectedValue);

		try {
			eventService.winnerOptions(selectedOptionsId, bettypeId);

		} catch (InstanceNotFoundException | ManyWinOptionsException e) {
		}
		
		listCheck.setBettypeId(bettypeId);
		return listCheck;
	}
}