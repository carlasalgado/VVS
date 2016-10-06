/**
 * 
 */
package es.udc.pa011.web.pages.event;

import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_ADMIN)
public class ListCheck {
	
	private Long bettypeId;

	public Long getBettypeId() {
		return bettypeId;
	}

	public void setBettypeId(Long bettypeId) {
		this.bettypeId = bettypeId;
	}
	
	Long onPassivate() {
		return bettypeId;
	}
	
	void onActivate(Long bettypeId) throws InstanceNotFoundException {
		this.bettypeId = bettypeId;
	}
}