/**
 * 
 */
package es.udc.pa011.web.pages.bet;

public class BetCreated {
	private Long betId;
	
	public Long getBetId() {
		return betId;
	}

	public void setBetId(Long betId) {
		this.betId = betId;
	}

	Long onPassivate() {
		return betId;
	}
	
	void onActivate(Long betId) {
		this.betId = betId;
	}
}