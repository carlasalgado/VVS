package es.udc.pa011.model.betservice;

import es.udc.pa011.model.bet.Bet;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public interface BetService {

	public Bet createBet(double amount, Long userProfile, Long option)
			throws InstanceNotFoundException, InputValidationException,
			       InvalidDateException;

	public BetBlock findBetByUserProfileId(Long userProfileId, int startIndex, int count)
			throws InstanceNotFoundException;
	
	public Bet findBet(Long betId) throws InstanceNotFoundException;

}
