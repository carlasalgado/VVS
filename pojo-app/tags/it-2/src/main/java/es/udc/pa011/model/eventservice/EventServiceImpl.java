package es.udc.pa011.model.eventservice;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.Set;

import org.springframework.transaction.annotation.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.bettype.BetTypeDao;
import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.category.CategoryDao;
import es.udc.pa011.model.event.EventDao;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.option.OptionDao;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@Service("eventService")
@Transactional
public class EventServiceImpl implements EventService{
	@Autowired
	private EventDao eventDao;
	@Autowired
	private CategoryDao categoryDao;
	@Autowired 
	private BetTypeDao betTypeDao;
	@Autowired 
	private OptionDao optionDao;

	@Override
	public Event createEvent(String name, Long categoryId, Calendar date)
			throws InstanceNotFoundException, DateBeforeTodayException {

		Calendar today = Calendar.getInstance();
		if (date.before(today))
			throw new DateBeforeTodayException();
		
		Category category = categoryDao.find(categoryId);
				
		Event event = new Event(name, category, date);
		
		eventDao.save(event);
		
		return event;
	}

	@Transactional(readOnly = true)
	public Event findEvent(Long eventId) 
			throws InstanceNotFoundException {
		return eventDao.find(eventId);
	}
	
    public Category findCategory(Long categoryId) throws InstanceNotFoundException{
    	return categoryDao.find(categoryId);
    }


	@Transactional(readOnly = true)
	public EventBlock findEventByKeyWordsAndCategory(String keyWords, Boolean isAdmin,
			Long categoryId, int startIndex, int count)
			throws InstanceNotFoundException {
		
		List<Event> events;

		if (isAdmin)
			events = eventDao.findByKeyWordsAndCategory(keyWords, Boolean.TRUE, categoryId, startIndex, count + 1);
		else	
			events = eventDao.findByKeyWordsAndCategory(keyWords, Boolean.FALSE, categoryId, startIndex, count + 1);
	
		Boolean existMoreEvents = events.size() == (count + 1);
		
		if (existMoreEvents) {
			events.remove(events.size() - 1);
		}
		
		return new EventBlock(events, existMoreEvents);

	}
	

    public BetType createBetType(Long eventId, BetType betType) 
    		throws InstanceNotFoundException{

    	Event event = eventDao.find(eventId);
    	
    	BetType bettype = new BetType(betType.getQuestion(), event, betType.isMultWinOptions(), betType.getOptions());
    	
    	Set<BetType> betTypes = event.getBetTypes();

    	betTypes.add(bettype);
		event.setBetTypes(betTypes);
		
		betTypeDao.save(bettype);
		
		Set<Option> optionsBettype = bettype.getOptions();
		Set<Option> options = betType.getOptions();
		
		if ((options != null) && (!options.isEmpty())){
				for (Option option : options){
					option.setBettype(bettype);					
					optionDao.save(option);
					optionsBettype.add(option);
				}
				
			}

		return bettype;
    }
    
    @Transactional(readOnly = true)
    public BetType findBetType(Long betTypeId) throws InstanceNotFoundException{
    	return betTypeDao.find(betTypeId);
    }


    public Option createOption(String nameOption, double share, Long betTypeId)
    		throws InstanceNotFoundException{
    	BetType bettype = betTypeDao.find(betTypeId);
    	
    	Option option = new Option(nameOption, share, bettype);
		optionDao.save(option);

    	Set<Option> options = bettype.getOptions();


    	options.add(option);
    	bettype.setOptions(options);

    	return option;
    			
    }
    
    @Transactional(readOnly = true)
    public Option findOption(Long optionId) throws InstanceNotFoundException{
    	return optionDao.find(optionId);
    }
    
    public void winnerOptions(List<Long> winOptionsId, Long betTypeId) 
    		throws InstanceNotFoundException, ManyWinOptionsException{
    	BetType bettype = betTypeDao.find(betTypeId);
    	
    	List<Option> winOptions = new ArrayList<Option>();
    	if (!bettype.isMultWinOptions() && (winOptionsId.size() > 1))
    		throw new ManyWinOptionsException();
    		
    	Option[] options = bettype.getOptions().toArray(new Option[bettype.getOptions().size()]);
    	
    	for (Long winOptionId: winOptionsId)
    		winOptions.add(optionDao.find(winOptionId));
    	
    	for (Option option : options){
    		if(winOptions.contains(option))
    			option.setWinner(true);
    		else 
    			option.setWinner(false);    		
    	}
    }
    
    public void winnerOption(Long optionId, Long betTypeId, boolean winOrLose) throws InstanceNotFoundException, ManyWinOptionsException{
    	BetType bettype = betTypeDao.find(betTypeId);
    	if (!bettype.isMultWinOptions())
    		for (Option opt:bettype.getOptions())
    			if (opt.isWinner())
    				throw new ManyWinOptionsException();
    	
    	Option o = optionDao.find(optionId);
    	o.setWinner(winOrLose);
    }

}