/**
 * 
 */
package es.udc.pa011.web.pages.event;

import java.util.ArrayList;
import java.util.List;

import org.apache.tapestry5.SelectModel;
import org.apache.tapestry5.ValueEncoder;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Persist;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.eventservice.ManyWinOptionsException;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class MarkLoser {
	
	private Long optionId;
	private Long betTypeId;
	
	@Inject
	private EventService eventService;	

	@SessionState(create=false)	
	private UserSession userSession;
	
	public Long getOptionId() {
		return optionId;
	}
	
	public void setOptionId(Long optionId) {
		this.optionId = optionId;
	}

	void onActivate(Long optionId) throws InstanceNotFoundException, ManyWinOptionsException{
		this.optionId = optionId;
		betTypeId = eventService.findOption(optionId).getBettype().getBetTypeId();

		eventService.winnerOption(optionId, betTypeId, false);
	}

}