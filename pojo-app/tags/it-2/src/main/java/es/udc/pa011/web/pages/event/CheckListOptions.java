
package es.udc.pa011.web.pages.event;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

//import es.udc.pa011.web.util.OptionSelectModel;
//import es.udc.pa011.web.util.OptionValueEncode;
import es.udc.pa011.web.util.UserSession;

import org.apache.tapestry5.PersistenceConstants;
import org.apache.tapestry5.SelectModel;
import org.apache.tapestry5.ValueEncoder;
import org.apache.tapestry5.annotations.Persist;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.eventservice.ManyWinOptionsException;
import es.udc.pa011.model.option.Option;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class CheckListOptions {
//	private List<Option> options = new ArrayList<Option>();
//	private Set<Option> optionsSet = new HashSet<Option>();
//	private BetType bettype;
//	private List<Long> selectedOptionsId = new ArrayList<Long>();
//	private Long bettypeId;
//	
//	@SessionState(create=false)	
//	UserSession userSession;
//
//	@Property
//	@Persist(PersistenceConstants.FLASH)
//	private List<Option> selectedOptions;
//	  	 	
//	@Property(write = false)
//	private SelectModel optionModel;
//	 
//	@Property(write = false)
//	private OptionValueEncode optionEncoder;
//	
//	@Inject
//	private EventService eventService;
//	
//	 public List<Option> getOptions() {
//		return options;
//	}
//
//	void onActivate(Long bettypeId) throws InstanceNotFoundException{
//		this.bettypeId=bettypeId;
//		bettype = eventService.findBetType(bettypeId);
//		optionsSet = bettype.getOptions();
//		
//		if (!optionsSet.isEmpty())
//			options.addAll(optionsSet);	
//		optionModel = new OptionSelectModel(options);
//		optionEncoder = new OptionValueEncode(options);
//	}
//	
//	void onSuccess() throws InstanceNotFoundException, ManyWinOptionsException{
//		for (Option o : selectedOptions)
//			selectedOptionsId.add(o.getOptionId());
//		
//		eventService.winnerOptions(selectedOptionsId, bettypeId);
//	}
}