package es.udc.pa011.web.pages.event;

import java.util.List;

import org.apache.tapestry5.SelectModel;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;
import org.apache.tapestry5.services.SelectModelFactory;

import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.util.LongCategoryValueEncoder;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

public class FindEvent {
	
	private Long userId;
	
	@Property
    private SelectModel categoryModel;
	
	@Inject
	private SelectModelFactory selectModelFactory;
	
	@Property
	private String keywords;
	
	@Property
	private Category category;
	
    @SessionState(create=false)	
	UserSession userSession;
	
    @Inject
	UserService userService;
    
    @Inject
	private EventService eventService;
	
	@InjectPage
	private EventsFound eventsFound;
	
	public LongCategoryValueEncoder getEncoder() {
        return new LongCategoryValueEncoder(eventService);
    }
	
	void onPrepareForRender() {
        List<Category> categories = eventService.getCategories();
        categoryModel = selectModelFactory.create(categories, "name");
    }
	
	Object onSuccess() throws InstanceNotFoundException{
		eventsFound.setKeywords(keywords);
		if (category==null)
			eventsFound.setCategory(null);
		else eventsFound.setCategory(category.getCategoryId());
		
		if (userSession == null)
			eventsFound.setLogin("guest");		//la sesion de ususario se establece como invitado
		else {		
			userId = userSession.getUserProfileId();
			eventsFound.setLogin(userService.findUserProfile(userId).getLoginName());
		}
		return eventsFound;
	}
}
