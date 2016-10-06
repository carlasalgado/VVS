package es.udc.pa011.web.util;

import org.apache.tapestry5.ValueEncoder;

import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class OptionValueEncoder implements ValueEncoder<Option>{
	 private EventService eventService;
	    
	    public OptionValueEncoder(EventService eventService) {
	        this.eventService = eventService;
	    }

	    @Override
	    public String toClient(Option o) {
	    	return String.valueOf(o.getOptionId());
	    }
	    
	    @Override
	    public Option toValue(String id) { 
	       try {
	    	   return eventService.findOption(Long.parseLong(id));
			} catch (NumberFormatException | InstanceNotFoundException e) {
				return null;
			}
	    }
}
