package es.udc.pa011.model.betservice;

import java.util.Calendar;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import es.udc.pa011.model.bet.Bet;
import es.udc.pa011.model.bet.BetDao;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.option.OptionDao;
import es.udc.pa011.model.userprofile.UserProfile;
import es.udc.pa011.model.userprofile.UserProfileDao;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@Service("betService")
@Transactional
public class BetServiceImpl implements BetService{
	@Autowired
	private BetDao betDao;
	@Autowired
	private UserProfileDao userProfileDao;
	@Autowired
	private OptionDao optionDao;
	
	public Bet createBet(Calendar date, double amount, Long userProfileId, Long optionId)
			throws InstanceNotFoundException, InputValidationException {
			
		if(amount <= 0)
			throw new InputValidationException("Invalid amount");
			
		UserProfile userProfile = userProfileDao.find(userProfileId);
			
		Option option = optionDao.find(optionId);
			
		Bet bet = new Bet(date, amount, userProfile, option);
			
		betDao.save(bet);
			
		return bet;
	}
	
	@Transactional(readOnly = true)
	public BetBlock findBetByUserProfileId(Long userProfileId, int startIndex, int count)
			throws InstanceNotFoundException{
		
		List<Bet> bets;
		
		bets = betDao.findByUserProfileId(userProfileId, startIndex, count + 1);
		
		Boolean existMoreBets = bets.size() == (count + 1);
		
		if (existMoreBets) {
			bets.remove(bets.size() - 1);
		}
		
		return new BetBlock(bets, existMoreBets);
	}
}
