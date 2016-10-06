package es.udc.pa011.web.pages.event;

public class EventCreated {

	private Long eventId;
	
	public Long getEvenId() {
		return eventId;
	}
	
	public void setEventId(Long eventId) {
		this.eventId = eventId;
	}
	
	Long onPassivate() {
		return eventId;
	}
	
	void onActivate(Long eventId) {
		this.eventId = eventId;
	}
}
