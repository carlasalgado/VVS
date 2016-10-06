package es.udc.pa011.model.betservice;

import java.util.Calendar;

import es.udc.pa011.model.bet.Bet;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public interface BetService {

	public Bet createBet(Calendar date, double amount, Long userProfile, Long option)
			throws InstanceNotFoundException, InputValidationException;

	public BetBlock findBetByUserProfileId(Long userProfileId, int startIndex, int count)
			throws InstanceNotFoundException;
}
