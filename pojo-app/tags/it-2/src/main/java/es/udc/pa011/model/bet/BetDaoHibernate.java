package es.udc.pa011.model.bet;

import java.util.List;

import org.springframework.stereotype.Repository;

import es.udc.pojo.modelutil.dao.GenericDaoHibernate;

@Repository("betDao")
public class BetDaoHibernate extends GenericDaoHibernate<Bet, Long> implements BetDao {

	@SuppressWarnings("unchecked")
    public List<Bet> findByUserProfileId(Long userProfileId, int startIndex,
        int count) {

        return getSession().createQuery(
        	"SELECT b FROM Bet b WHERE b.userProfile.userProfileId = :userProfileId " +
        	"ORDER BY b.date DESC")
         	.setParameter("userProfileId", userProfileId)
           	.setFirstResult(startIndex)
           	.setMaxResults(count)
           	.list();
    }
}
