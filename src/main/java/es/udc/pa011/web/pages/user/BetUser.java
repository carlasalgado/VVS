/**
 * 
 */
package es.udc.pa011.web.pages.user;

import java.text.DateFormat;
import java.text.Format;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Set;

import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bet.Bet;
import es.udc.pa011.model.betservice.BetBlock;
import es.udc.pa011.model.betservice.BetService;
import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class BetUser {

	private final static int BETS_PER_PAGE = 3;
	
	public enum Estate {
		PENDIENTE, GANADA, PERDIDA
	}
		
	private Estate estate;
	@Property
	private Bet bet;
	@Property
	private Option option;
	@Property
	private Event event;
	@Property
	private BetType bettype;
	@Property
	private double winAmount=0;
	
	private Long userId;
	private int startIndex = 0;
	private BetBlock betBlock;
	
	@Inject
	private Locale locale;

	@Inject
	private BetService betService;
	
	@Inject
	private UserService userService;
	
	
	@SessionState(create=false)	
	private UserSession userSession;
	
	public int getStartIndex() {
		return startIndex;
	}

	public void setStartIndex(int startindex) {
		this.startIndex = startindex;
	}

	public BetBlock getBetBlock() {
		return betBlock;
	}

	public void setBetBlock(BetBlock betBlock) {
		this.betBlock = betBlock;
	}
	
	public List<Option> getOptions() {
		Set<Option> opt = bet.getOption().getBettype().getOptions();
		List<Option> optionsWin = new ArrayList<Option>();
		
		if (!opt.isEmpty()){
			for (Option o : opt){
				if (o.isWinner()==null)
					continue;
				else if (o.isWinner())
					optionsWin.add(o);
			}
		}
		
		return optionsWin;
	}
	
	public Estate getEstate(){
		if (getOptions() == null || getOptions().isEmpty()){
			winAmount = 0;
			return Estate.PENDIENTE;
		}
		else if (getOptions().contains(bet.getOption())){
			winAmount = bet.getAmount() * bet.getOption().getShare();
			return Estate.GANADA;
		}
		else{
			winAmount = 0;
			return Estate.PERDIDA;
		}
	}	
	
	public void setEstate(Estate estate) {
		this.estate = estate;
	}
	public List<Bet> getBets() {
		return betBlock.getBets();
	}

	public static int getEventsPerPage() {
		return BETS_PER_PAGE;
	}
	
	public Format getFormat(){
		return DateFormat.getDateTimeInstance(DateFormat.LONG, DateFormat.MEDIUM, locale);
	}
	
	public Object[] getPreviousLinkContext() {
		
		if (startIndex-BETS_PER_PAGE >= 0) {
			return new Object[] {startIndex-BETS_PER_PAGE};
		} else {
			return null;
		}
		
	}
	
	public Object[] getNextLinkContext() {
		
		if (betBlock.getExistMoreBets()) {
			return new Object[] {startIndex+BETS_PER_PAGE};
		} else {
			return null;
		}
	}
	
	void onActivate(int startIndex) throws InstanceNotFoundException {
		userId = userSession.getUserProfileId();
		this.startIndex = startIndex;
		betBlock = betService.findBetByUserProfileId(userId, startIndex, BETS_PER_PAGE);
	}
	
	Object[] onPassivate() {
		return new Object[] {startIndex};
	}
	
}