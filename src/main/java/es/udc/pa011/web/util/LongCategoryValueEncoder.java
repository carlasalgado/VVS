package es.udc.pa011.web.util;

import org.apache.tapestry5.ValueEncoder;

import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class LongCategoryValueEncoder implements ValueEncoder<Category>{
	
    private EventService eventService;
    
    public LongCategoryValueEncoder(EventService eventService) {
        this.eventService = eventService;
    }

    @Override
    public String toClient(Category c) {
    	if (c == null)
    		return String.valueOf(-1);
    	else return String.valueOf(c.getCategoryId());
    }
    
    @Override
    public Category toValue(String id) { 
       try {
    	   return eventService.findCategory(Long.parseLong(id));
		} catch (NumberFormatException | InstanceNotFoundException e) {
			return null;
		}
    }
}
