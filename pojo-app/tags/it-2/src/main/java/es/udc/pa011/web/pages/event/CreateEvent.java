package es.udc.pa011.web.pages.event;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.DateBeforeTodayException;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_USERS)

public class CreateEvent {
	
	private Event event;
	private boolean admin;
	
	@Property
	private String name;
	
	@Property
	private Long category;
	
	@Property
	private String date;
	
	@SessionState(create=false)
	private UserSession userSession;
	
	@Inject
	private EventService eventService;
	
	@Inject
	private UserService userService;
	
	@Inject
	private Locale locale;
	
	@InjectPage
	private EventCreated eventCreated;
	
	public boolean isAdmin() {
		return admin;
	}

	public void setAdmin(boolean admin) {
		this.admin = admin;
	}

	public Calendar stringToCalendar(String date) throws ParseException{
		Calendar cal = Calendar.getInstance();
		SimpleDateFormat sdf = new SimpleDateFormat("dd-mm-yyyy HH:mm:ss", locale);
		cal.setTime(sdf.parse(date));
		
		return cal;
	}
	
	void onActivate() throws InstanceNotFoundException{
		if (userSession == null)
			admin = false;
		else{
			Long userId = userSession.getUserProfileId();
			String login = userService.findUserProfile(userId).getLoginName();
			
			if(login.compareTo("admin")==0)
				admin = true;
			else admin = false;
		}
			
	}
	
	Object onSuccess() throws InstanceNotFoundException, DateBeforeTodayException, ParseException{
		event = eventService.createEvent(name, category, stringToCalendar(date));		
		
		return eventCreated;

    }
}
