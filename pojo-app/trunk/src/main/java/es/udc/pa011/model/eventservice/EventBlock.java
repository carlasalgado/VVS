package es.udc.pa011.model.eventservice;

import java.util.List;

import es.udc.pa011.model.event.Event;

public class EventBlock {
	private List<Event> events;
    private boolean existMoreEvents;

    public EventBlock(List<Event> events, boolean existMoreEvents) {
        
        this.events = events;
        this.existMoreEvents = existMoreEvents;

    }
    
    public List<Event> getEvents() {
        return events;
    }
    
    public boolean getExistMoreEvents() {
        return existMoreEvents;
    }

}
