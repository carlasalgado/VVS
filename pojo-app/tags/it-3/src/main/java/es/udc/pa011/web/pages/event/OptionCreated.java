/**
 * 
 */
package es.udc.pa011.web.pages.event;

public class OptionCreated {
	private Long optionId;
	
	public Long getOptionId() {
		return optionId;
	}

	public void setOptionId(Long optionId) {
		this.optionId = optionId;
	}

	Long onPassivate() {
		return optionId;
	}
	
	void onActivate(Long optionId) {
		this.optionId = optionId;
	}
}