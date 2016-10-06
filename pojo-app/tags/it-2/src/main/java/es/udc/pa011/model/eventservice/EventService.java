package es.udc.pa011.model.eventservice;

import java.util.Calendar;
import java.util.List;

import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;
import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.option.Option;

public interface EventService {

    public Event createEvent(String name, Long categoryId, Calendar date) 
    		throws InstanceNotFoundException, DateBeforeTodayException;

    public Event findEvent(Long eventId) throws InstanceNotFoundException;
    
    public Category findCategory(Long categoryId) throws InstanceNotFoundException;
    
    public EventBlock findEventByKeyWordsAndCategory(
    		String keyWords, Boolean isAdmin, Long categoryId,
            int startIndex, int count) throws InstanceNotFoundException;
    
    public BetType createBetType(Long eventId, BetType betType) 
    		throws InstanceNotFoundException;

    public BetType findBetType(Long betTypeId) throws InstanceNotFoundException;

    public Option createOption(String option, double share, Long betTypeId)
    		throws InstanceNotFoundException;
    
    public Option findOption(Long optionId) throws InstanceNotFoundException;
    
    public void winnerOptions(List<Long> options, Long betTypeId) 
    		throws InstanceNotFoundException, ManyWinOptionsException;
    
    public void winnerOption(Long optionId, Long betTypeId, boolean winOrLose) throws InstanceNotFoundException, ManyWinOptionsException;
}
