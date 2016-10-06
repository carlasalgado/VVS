package es.udc.pa011.model.bet;

import java.util.List;
import es.udc.pojo.modelutil.dao.GenericDao;

public interface BetDao extends GenericDao<Bet, Long>{

	public List<Bet> findByUserProfileId(Long userProfileId, int startIndex,
	    	int count);
}
