package es.udc.pa011.web.pages.event;

import java.text.DateFormat;
import java.text.Format;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.EventBlock;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;


public class EventsFound {
	private final static int EVENTS_PER_PAGE = 3;
	
	private String keywords;
	private Long category;
	private String login;
	
	private int startIndex = 0;
	private EventBlock eventBlock;
	private Event event;
	
	@Inject
	private Locale locale;

	@Inject
	private EventService eventService;

	
	public String getLogin() {
		return login;
	}

	public void setLogin(String login) {
		this.login = login;
	}

	public Long getCategory() {
		return category;
	}

	public void setCategory(Long category) {
		this.category = category;
	}
	
	public String getKeywords() {
		return keywords;
	}

	public void setKeywords(String keywords) {
		this.keywords = keywords;
	}

	public Event getEvent() {
		return event;
	}

	public void setEvent(Event event) {
		this.event = event;
	}

	public List<Event> getEvents() {
		return eventBlock.getEvents();
	}
	
	public Format getFormat(){
		return DateFormat.getDateTimeInstance(DateFormat.LONG, DateFormat.MEDIUM, locale);
	}
	
	public Object[] getPreviousLinkContext() {
		
		if (startIndex-EVENTS_PER_PAGE >= 0) {
			return new Object[] {keywords, category, login, startIndex-EVENTS_PER_PAGE};
		} else {
			return null;
		}
		
	}
	
	public Object[] getNextLinkContext() {
		
		if (eventBlock.getExistMoreEvents()) {
			return new Object[] {keywords, category, login, startIndex+EVENTS_PER_PAGE};
		} else {
			return null;
		}
		
	}
	
	void onActivate(String keywords, Long category, String login, int startIndex) throws InstanceNotFoundException {
		this.keywords = keywords;
		this.category = category;
		this.login = login;
		this.startIndex = startIndex;
				
		if (login.compareTo("admin")==0)
			eventBlock = eventService.findEventByKeyWordsAndCategory(keywords, true, category,
				startIndex, EVENTS_PER_PAGE);
		else
			eventBlock = eventService.findEventByKeyWordsAndCategory(keywords, false, category,
				startIndex, EVENTS_PER_PAGE);
	}
	
	Object[] onPassivate() {
		return new Object[] {keywords, category, login, startIndex};
	}
}
