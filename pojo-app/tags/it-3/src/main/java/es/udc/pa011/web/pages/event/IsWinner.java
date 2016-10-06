/**
 * 
 */
package es.udc.pa011.web.pages.event;

import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class IsWinner {
	private Option option;
	private Long optionId;
	private Boolean optionWinner;
	private boolean win;
	private boolean sinResolver;
	
	public boolean isWin() {
		return win;
	}

	public void setWin(boolean win) {
		this.win = win;
	}

	public boolean isSinResolver() {
		return sinResolver;
	}

	public void setSinResolver(boolean sinResolver) {
		this.sinResolver = sinResolver;
	}

	@Inject
	private EventService eventService;	
	
	@SessionState(create=false)	
	private UserSession userSession;
	
	void onActivate(Long optionId) throws InstanceNotFoundException{
		this.optionId = optionId;
		
		option = eventService.findOption(optionId);
		optionWinner = option.isWinner();
		
		if (optionWinner == null){
			sinResolver = true;
			win = false;
		}
		else if(optionWinner == true){
			sinResolver = false;
			win = true;
		}
		else if(optionWinner == false){
			sinResolver = false;
			win = false;
		}
	}

}