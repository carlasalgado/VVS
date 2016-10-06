/**
 * 
 */
package es.udc.pa011.web.pages.user;

import java.util.ArrayList;
import java.util.List;
import java.util.Set;

import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bet.Bet;
import es.udc.pa011.model.betservice.BetService;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;


public class BetDetails {
	
	public enum Estate {
		PENDIENTE, GANADA, PERDIDA
	}
	
	private List<Option> options = new ArrayList<Option>();
	private Estate estate;
	private double winAmount=0;
	private Long betId;
	private Option option;
	
	@Inject
	private BetService betService;
	
	@SessionState(create=false)	
	private UserSession userSession;
	
	public List<Option> getOptions(){
		return options;
	}
	
	public Estate getEstate(){
		return estate;
	}	
	
	public void setEstate(Estate estate) {
		this.estate = estate;
	}

	public double getWinAmount(){
		return winAmount;
	}

	public void setWinAmount(double winAmount) {
		this.winAmount = winAmount;
	}

	public Option getOption() {
		return option;
	}

	public void setOption(Option option) {
		this.option = option;
	}

	public Long getBetId() {
		return betId;
	}

	public void setBetId(Long betId) {
		this.betId = betId;
	}

	void onActivate(Long betId) throws InstanceNotFoundException {
		this.betId = betId;
		
		Bet b = betService.findBet(betId);
		Set<Option> opt = b.getOption().getBettype().getOptions();
		if (!opt.isEmpty()){
			for (Option o : opt){
				if (o.isWinner()==null)
					continue;
				else if (o.isWinner())
					options.add(o);
			}
		}
		
		if (getOptions().isEmpty())
			estate = Estate.PENDIENTE;
		else if (getOptions().contains(betService.findBet(betId).getOption())){
			estate = Estate.GANADA;
			winAmount = b.getAmount() * b.getOption().getShare();
		}
		else estate = Estate.PERDIDA;
		
		
	}
	
}