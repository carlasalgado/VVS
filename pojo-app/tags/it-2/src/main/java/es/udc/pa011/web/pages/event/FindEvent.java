package es.udc.pa011.web.pages.event;

import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;

import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class FindEvent {
	
	private Long userId;
	
	@Property
	private String keywords;
	
	@Property
	private Long category;
	
    @SessionState(create=false)	
	UserSession userSession;
	
    @Inject
	UserService userService;
	
	@InjectPage
	private EventsFound eventsFound;
	
	Object onSuccess() throws InstanceNotFoundException{
		eventsFound.setKeywords(keywords);
		eventsFound.setCategory(category);
		
		if (userSession == null)
			eventsFound.setLogin("guest");		//la sesion de ususario se establece como invitado
		else {		
			userId = userSession.getUserProfileId();
			eventsFound.setLogin(userService.findUserProfile(userId).getLoginName());
		}
		return eventsFound;
	}
}
