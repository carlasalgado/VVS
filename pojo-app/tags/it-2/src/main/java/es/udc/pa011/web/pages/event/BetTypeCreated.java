package es.udc.pa011.web.pages.event;

public class BetTypeCreated {
	private Long betTypeId;
	
	public Long getBetTypeId() {
		return betTypeId;
	}

	public void setBetTypeId(Long betTypeId) {
		this.betTypeId = betTypeId;
	}

	Long onPassivate() {
		return betTypeId;
	}
	
	void onActivate(Long betTypeId) {
		this.betTypeId = betTypeId;
	}
}
