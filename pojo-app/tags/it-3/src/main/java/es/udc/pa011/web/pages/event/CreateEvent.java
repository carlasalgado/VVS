package es.udc.pa011.web.pages.event;

import java.util.Date;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.ParsePosition;
import java.util.Calendar;
import java.util.List;
import java.util.Locale;

import org.apache.tapestry5.SelectModel;
import org.apache.tapestry5.annotations.Component;
import org.apache.tapestry5.annotations.InjectPage;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.corelib.components.Form;
import org.apache.tapestry5.corelib.components.TextField;
import org.apache.tapestry5.ioc.Messages;
import org.apache.tapestry5.ioc.annotations.Inject;
import org.apache.tapestry5.services.SelectModelFactory;

import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.eventservice.DateBeforeTodayException;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.LongCategoryValueEncoder;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_ADMIN)

public class CreateEvent {
	
	private Event event;
	private boolean admin;
	
	@Property
    private SelectModel categoryModel;
	
	@Inject
	private SelectModelFactory selectModelFactory;
	
	@Property
	private Category category;

	@Property
	private String name;
	
	@Property
	private String date;
	
	@Property
	private String time;
	
	@Component(id="date")
	private TextField dateField;
	
	@Component(id="time")
	private TextField timeField;
	
	@Component
	private Form createEventForm;
	
	@SessionState(create=false)
	private UserSession userSession;
	
	@Inject
	private EventService eventService;
	
	@Inject
	private UserService userService;
	
	@Inject
	private Locale locale;
	
	@Inject
	private Messages messages;
	
	@InjectPage
	private EventCreated eventCreated;
	
	private Date dateAsDate;
	private Date timeAsDate;
	private Calendar dateTime;
	
	public boolean isAdmin() {
		return admin;
	}

	public void setAdmin(boolean admin) {
		this.admin = admin;
	}
	
	void onValidateFromCreateEventForm() {
		
		if (!createEventForm.isValid()) {
			return;
		}
		
		dateAsDate = validateDate(dateField, date);
		timeAsDate = validateTime(timeField, time);
		
		dateTime = dateTime(dateAsDate, timeAsDate);
		validateDateTime(dateTime, dateField, date);
		
	}
	
	private Calendar dateTime(Date date, Date time){
		Calendar d = Calendar.getInstance(locale);
		Calendar t = Calendar.getInstance(locale);
		d.setTime(date);
		t.setTime(time);
		
		t.set(Calendar.DAY_OF_MONTH, d.get(Calendar.DAY_OF_MONTH));
		t.set(Calendar.YEAR, d.get(Calendar.YEAR));
		t.set(Calendar.MONTH, d.get(Calendar.MONTH));

		return t;
	}
	
	public LongCategoryValueEncoder getEncoder() {
        return new LongCategoryValueEncoder(eventService);
    }
	
	void onPrepareForRender() {
        List<Category> categories = eventService.getCategories();
        categoryModel = selectModelFactory.create(categories, "name");
    }
	
	private Date validateDate(TextField dateField, String dateAsString) {
		
		ParsePosition position = new ParsePosition(0);
		Date date = DateFormat.getDateInstance(DateFormat.SHORT, locale).
			parse(dateAsString, position);
		
		if (position.getIndex() != dateAsString.length()) {
			createEventForm.recordError(dateField,
				messages.format("error-incorrectDateFormat", dateAsString));
		}

		return date;
		
	}
	
	private Date validateTime(TextField timeField, String timeAsString) {
		
		ParsePosition position = new ParsePosition(0);
		Date date = DateFormat.getTimeInstance(DateFormat.SHORT, locale).
			parse(timeAsString, position);
		
		if (position.getIndex() != timeAsString.length()) {
			createEventForm.recordError(timeField,
				messages.format("error-incorrectTimeFormat", timeAsString));
		}

		return date;
		
	}
	
	private void validateDateTime(Calendar dateTime, TextField dataeField, String dateAsString) {
		
		Calendar today = Calendar.getInstance(locale);
		
		if (dateTime.before(today)) {
			createEventForm.recordError(dateField,
				messages.format("error-incorrectDateTime", dateAsString));
		}
	}
	
	private String dateToString(Date date) {
		return DateFormat.getDateInstance(DateFormat.SHORT, locale).
			format(date);
	}
	
	private String timeToString(Date date) {
		return DateFormat.getTimeInstance(DateFormat.SHORT, locale).
			format(date);
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
		
		date = dateToString(Calendar.getInstance(locale).getTime());
		time = timeToString(Calendar.getInstance(locale).getTime());

	}
	
	Object onSuccess() throws InstanceNotFoundException, DateBeforeTodayException, ParseException{
		
		event = eventService.createEvent(name, category.getCategoryId(), 
				dateTime(dateAsDate, timeAsDate));		

		return eventCreated;

    }
}
