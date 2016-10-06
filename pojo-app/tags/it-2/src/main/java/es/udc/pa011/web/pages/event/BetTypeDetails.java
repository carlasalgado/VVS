/**
 * 
 */
package es.udc.pa011.web.pages.event;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.eventservice.ManyWinOptionsException;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class BetTypeDetails {
	private String login;
	private Long userId;
	//private Long optionId = null;
	private Long bettypeId;
	private Set<Option> optionsSet = new HashSet<Option>();
	private List<Option> options = new ArrayList<Option>();
	//private List<Long> optionsId = new ArrayList<Long>();
	private Option option;
	private BetType bettype;
	private boolean admin=false;
	private boolean guestOrUser=false;
	private boolean start;
	private boolean finish;
	private Long est;

	@Inject
	private UserService userService;
	
	@Inject
	private EventService eventService;
	
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

	public boolean isGuestOrUser() {
		return guestOrUser;
	}

	public void setGuestOrUser(boolean guestOrUser) {
		this.guestOrUser = guestOrUser;
	}

	public boolean isStart() {
		return start;
	}

	public void setStart(boolean start) {
		this.start = start;
	}
	
	public boolean isFinish() {
		return finish;
	}

	public void setFinish(boolean finish) {
		this.finish = finish;
	}

	void onActivate(Long bettypeId) throws InstanceNotFoundException{
		this.bettypeId = bettypeId;
		//this.optionId = optionId;
		
		optionsSet = eventService.findBetType(bettypeId).getOptions();
		
		if (!optionsSet.isEmpty())
			options.addAll(optionsSet);
		
		if(userSession==null)
			guestOrUser=true;
		else{
			userId = userSession.getUserProfileId();
			login = userService.findUserProfile(userId).getLoginName();
			
			if (login.compareTo("admin")==0)
				admin = true;
			else 
				guestOrUser=true;
		}
		
		BetType bettype = eventService.findBetType(bettypeId);
		Calendar dateEvent = bettype.getEvent().getDate();
		Calendar today = Calendar.getInstance();
		
		if(dateEvent.before(today)){
			start = true;
			finish=false;
		}
		else{ start= false; finish= true;}
		
	}

}