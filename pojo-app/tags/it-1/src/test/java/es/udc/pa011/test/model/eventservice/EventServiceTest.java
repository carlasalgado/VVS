package es.udc.pa011.test.model.eventservice;

import static es.udc.pa011.model.util.GlobalNames.SPRING_CONFIG_FILE;
import static es.udc.pa011.test.util.GlobalNames.SPRING_CONFIG_TEST_FILE;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.transaction.annotation.Transactional;

import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.category.CategoryDao;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.event.EventDao;
import es.udc.pa011.model.eventservice.DateBeforeTodayException;
import es.udc.pa011.model.eventservice.EventBlock;
import es.udc.pa011.model.eventservice.EventService;
import es.udc.pa011.model.option.Option;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = { SPRING_CONFIG_FILE, SPRING_CONFIG_TEST_FILE })
@Transactional
public class EventServiceTest {
	private final long NON_EXISTENT_CATEGORY_ID = -1;
    private final long NON_EXISTENT_EVENT_ID = -1;
    private final long NON_EXISTENT_BETTYPE_ID = -1;
    private final long NON_EXISTENT_OPTION_ID = -1;

    @Autowired
    private EventService eventService;
    
    @Autowired
    private EventDao eventDao;
    
    @Autowired
    private CategoryDao categoryDao;
        
    //********** CREATE AND FIND EVENT **********
    @Test
    public void testCreateEventAndFindEvent()
        throws InstanceNotFoundException, DateBeforeTodayException {

    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2016, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute
    	
        Event event = eventService.createEvent("Nadal - Federer", idTenis, startDate);
        Event event2 = eventService.findEvent(event.getEventId());

        assertEquals(event, event2);
    }
    
  //*********** CREATE A EVENT WITH DATE BEFORE TODAY **********
    @Test(expected = DateBeforeTodayException.class)
    public void testDateBeforeToday() throws InstanceNotFoundException, DateBeforeTodayException {     	
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2015, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute

    	eventService.createEvent("Nadal - Federer", idTenis, startDate);
    }
    
    //*********** CREATE A EVENT WITH NON EXISTENT CATEGORY **********
    @Test(expected = InstanceNotFoundException.class)
    public void testNonExistentCategory() throws InstanceNotFoundException, DateBeforeTodayException {     	
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2016, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute

    	eventService.createEvent("Nadal - Federer", NON_EXISTENT_CATEGORY_ID, startDate);
    }
    
    //*********** NON EXISTENT EVENT **********
    @Test(expected = InstanceNotFoundException.class)
    public void testFindNonExistentEvent() throws InstanceNotFoundException {
        eventService.findEvent(NON_EXISTENT_EVENT_ID);
    }
    
    //********** FIND EVENTS BY KEYWORDS COMPLETE AND CATEGORY - ADMIN **********
    @Test
    public void testFindEventsByKeywordsAndCategoryAdmin() throws InstanceNotFoundException, DateBeforeTodayException{
    	
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDateBefore = Calendar.getInstance();
    	Calendar startDateAfter = Calendar.getInstance();
    	startDateBefore.set(2016, 1, 01, 16, 30); // Create a date 01 - January - 2016
    	startDateAfter.set(2016, 8, 20, 16, 30); // Create a date 20 - August - 2016

    	/*Create three events that should not be found*/
    	eventService.createEvent("BarÃ§a - Valencia", idFutbol, startDateAfter); //KeyWords don't match
    	eventService.createEvent("Deportivo - Real Madrid", idTenis, startDateAfter); //Category donÂ´t match
    	eventService.createEvent("BarÃ§a - Valencia", idTenis, startDateAfter); //KeyWords and category don't match
    	
    	/* Create the events that should be found*/
    	int numberOfEvents = 2;
        List<Event> expectedEvents = new ArrayList<Event>();

        //Empty list
        assertTrue (eventService.findEventByKeyWordsAndCategory("Deportivo - Real Madrid", idFutbol, "admin", 0, 2).getEvents().size()==0);
        
        Event evento = new Event("Deportivo - Real Madrid", categoryFutbol, startDateBefore);
        eventDao.save(evento);
        expectedEvents.add(evento); //Date before actual date
        expectedEvents.add(eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDateAfter)); //Date after actual date

    	EventBlock eventBlock;
        int count = 2;
        int startIndex = 0;
        short resultIndex = 0;
        
        // ***** FIND WITH KEYWORDS COMPLETE AND CATEGORY *****
        String completeKeyWords = "Deportivo - Real Madrid";

        do {

        	eventBlock = eventService.findEventByKeyWordsAndCategory(
            		completeKeyWords, idFutbol, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
        //****************************************************
        
        eventBlock = null;
        startIndex = 0;
        resultIndex = 0;
        // ***** FIND WITH PARCIAL KEYWORDS AND CATEGORY *****
        String parcialKeyWords = "Depor Madrid";

        do {

            eventBlock = eventService.findEventByKeyWordsAndCategory(
            		parcialKeyWords, idFutbol, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
        //****************************************************
        
        eventBlock = null;
        startIndex = 0;
        resultIndex = 0;
        //***** FIND WITH MESSY KEYWORDS AND CATEGORY *****
        String messyKeyWords = "madrid depor";

        do {

        	eventBlock = eventService.findEventByKeyWordsAndCategory(
            		messyKeyWords, idFutbol, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
        //****************************************************
    }
    
    //********** FIND EVENTS BY KEYWORDS - ADMIN **********
    @Test
    public void testFindEventsByKeywordsAdmin() throws InstanceNotFoundException, DateBeforeTodayException{
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDateBefore = Calendar.getInstance();
    	Calendar startDateAfter = Calendar.getInstance();
    	startDateBefore.set(2016, 1, 01, 16, 30); // Create a date 01 - January - 2016
    	startDateAfter.set(2016, 8, 20, 16, 30); // Create a date 20 - August - 2016

    	
    	/*Create two events that should not be found*/
    	eventService.createEvent("BarÃ§a - Valencia", idFutbol, startDateAfter); //KeyWords don't match
    	eventService.createEvent("BarÃ§a - Valencia", idTenis, startDateAfter); //KeyWords and category don't match
    	
    	/* Create three events that should be found*/
    	int numberOfEvents = 3;
        List<Event> expectedEvents = new ArrayList<Event>();
        
        Event evento = new Event("Deportivo - Real Madrid", categoryFutbol, startDateBefore);
        eventDao.save(evento);
        expectedEvents.add(evento); //Date before actual date       
        expectedEvents.add(eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDateAfter)); //Date after actual date
        expectedEvents.add(eventService.createEvent("Deportivo - Real Madrid", idTenis, startDateAfter)); //Category changed

        EventBlock eventBlock;
        int count = 2;
        int startIndex = 0;
        short resultIndex = 0;
        
        String completeKeyWords = "Deportivo - Real Madrid";

        do {

        	eventBlock = eventService.findEventByKeyWordsAndCategory(
            		completeKeyWords, null, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
    }
    
    //********** FIND EVENTS BY CATEGORY - ADMIN **********
    @Test
    public void testFindEventsByCategoryAdmin() throws InstanceNotFoundException, DateBeforeTodayException{
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDateBefore = Calendar.getInstance();
    	Calendar startDateAfter = Calendar.getInstance();
    	startDateBefore.set(2016, 1, 01, 16, 30); // Create a date 01 - January - 2016
    	startDateAfter.set(2016, 8, 20, 16, 30); // Create a date 20 - August - 2016

    	/*Create three events that should not be found*/
    	eventService.createEvent("Deportivo - Real Madrid", idTenis, startDateAfter); //Category donÂ´t match
    	eventService.createEvent("BarÃ§a - Valencia", idTenis, startDateAfter); //KeyWords and category don't match
    	
    	/* Create two events that should be found*/
    	int numberOfEvents = 2;
        List<Event> expectedEvents = new ArrayList<Event>();
        
        Event evento = new Event("Deportivo - Real Madrid", categoryFutbol, startDateBefore);
        eventDao.save(evento);
        expectedEvents.add(evento); //Date before actual date 
    	expectedEvents.add(eventService.createEvent("BarÃ§a - Valencia", idFutbol, startDateAfter));    	
    
    	/* Check operations with correct date range and order. */
        EventBlock eventBlock;
        int count = 2;
        int startIndex = 0;
        short resultIndex = 0;
        
        do {

            eventBlock = eventService.findEventByKeyWordsAndCategory(
            		null, idFutbol, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == 
                	expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
    }
    
  //********** FIND EVENTS - ADMIN **********
    @Test
    public void testFindEventsAdmin() throws InstanceNotFoundException, DateBeforeTodayException{
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDateBefore = Calendar.getInstance();
    	Calendar startDateAfter = Calendar.getInstance();
    	startDateBefore.set(2016, 1, 01, 16, 30); // Create a date 01 - January - 2016
    	startDateAfter.set(2016, 8, 20, 16, 30); // Create a date 20 - August - 2016

    	/*Create four events that should be found*/
    	int numberOfEvents = 4;
        List<Event> expectedEvents = new ArrayList<Event>();
        
        Event evento = new Event("Deportivo - Real Madrid", categoryFutbol, startDateBefore);
        eventDao.save(evento);
        expectedEvents.add(evento); //Date before actual date 
        expectedEvents.add(eventService.createEvent("BarÃ§a - Valencia", idFutbol, startDateAfter));
    	expectedEvents.add(eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDateAfter)); //Date after actual date
    	expectedEvents.add(eventService.createEvent("Nadal - Federer", idTenis, startDateAfter));

    	/* Check operations with correct date range and order. */
        EventBlock eventBlock;
        int count = 2;
        int startIndex = 0;
        int resultIndex = 0;

        do {

        	eventBlock = eventService.findEventByKeyWordsAndCategory(
            		null, null, "admin", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == 
                	expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
    }
    
  //********** FIND EVENTS - USER **********
    @Test
    public void testFindEventsUser() throws InstanceNotFoundException, DateBeforeTodayException{
    	Category categoryTenis = new Category("tenis");
    	categoryDao.save(categoryTenis);
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idTenis = categoryTenis.getCategoryId();
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDateBefore = Calendar.getInstance();
    	Calendar startDateAfter = Calendar.getInstance();
    	startDateBefore.set(2016, 1, 01, 16, 30); // Create a date 01 - January - 2016
    	startDateAfter.set(2016, 8, 20, 16, 30); // Create a date 20 - August - 2016

    	/*Create three events that should be found*/
    	Event evento = new Event("Deportivo - Real Madrid", categoryFutbol, startDateBefore);
        eventDao.save(evento);

        /*Create three events that should be found*/
    	int numberOfEvents = 3;
    	List<Event> expectedEvents = new ArrayList<Event>();
    	
    	expectedEvents.add(eventService.createEvent("BarÃ§a - Valencia", idFutbol, startDateAfter));
    	expectedEvents.add(eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDateAfter)); //Date after actual date
    	expectedEvents.add(eventService.createEvent("Nadal - Federer", idTenis, startDateAfter));

    	/* Check operations with correct date range and order. */
        EventBlock eventBlock;
        int count = 2;
        int startIndex = 0;
        int resultIndex = 0;

        do {

        	eventBlock = eventService.findEventByKeyWordsAndCategory(
            		null, null, "user", startIndex, count);
            
            assertTrue(eventBlock.getEvents().size() <= count);
            
            for (Event event : eventBlock.getEvents()) {
                assertTrue(event == 
                	expectedEvents.get(resultIndex++));
            }
            startIndex += count;

        } while (eventBlock.getExistMoreEvents());

        assertTrue(numberOfEvents == startIndex - count + eventBlock.getEvents().size());
    }
    
  //********** CREATE AND FIND BETTYPES BY EVENT AND OPTIONS BY BETTYPE **********
    @Test
    public void testCreateAndFindBetTypesAndOptions()
        throws InstanceNotFoundException, DateBeforeTodayException{

    	//CREATE EVENT
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2016, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute
    	
        Event event = eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDate);
        
        //CREATE BETTYPE AND OPTION 
        List<Option> options = new ArrayList<Option>();
        Option optionWinnerDeportivo = new Option ("Deportivo", 1.5);
        options.add(optionWinnerDeportivo);
        
        BetType betTypeWinner = 
        		eventService.createBetType(event.getEventId(), "¿Quien será el ganador?", options);
        BetType betTypeWinnerFind = 
        		eventService.findBetType(betTypeWinner.getBetTypeId());
        
        assertEquals(betTypeWinner, betTypeWinnerFind);
           
        Option optionWinnerDeportivoFind = eventService.findOption(optionWinnerDeportivo.getOptionId());
        assertEquals(optionWinnerDeportivo, optionWinnerDeportivoFind);

        //FIND BETTYPES        
        assertTrue(event.getBetTypes().contains(betTypeWinner));
        
        //FIND OPTIONS
        assertTrue(betTypeWinner.getOptions().contains(optionWinnerDeportivo));
    
    }
    
    //*********** CREATE A BETTYPE WITH NON EXISTENT EVENT **********
    @Test(expected = InstanceNotFoundException.class)
    public void testBetTypeNonExistentEvent() throws InstanceNotFoundException {     	
    	eventService.createBetType(NON_EXISTENT_EVENT_ID, "pregunta", null);
    }
    
    //*********** NON EXISTENT BETTYPE **********
    @Test(expected = InstanceNotFoundException.class)
    public void testFindNonExistentBetType() throws InstanceNotFoundException {
        eventService.findEvent(NON_EXISTENT_BETTYPE_ID);
    }
    
    //*********** CREATE A OPTION WITH NON EXISTENT BETTYPE **********
    @Test(expected = InstanceNotFoundException.class)
    public void testOptionNonExistentBetType() throws InstanceNotFoundException {     	
    	eventService.createBetType(NON_EXISTENT_BETTYPE_ID, "pregunta", null);
    }
    
    //*********** NON EXISTENT OPTION **********
    @Test(expected = InstanceNotFoundException.class)
    public void testFindNonExistentOption() throws InstanceNotFoundException {
        eventService.findEvent(NON_EXISTENT_OPTION_ID);
    }
     
    //*********** WINNER OPTIONS **********
    @Test
    public void testWinnerOptions() throws InstanceNotFoundException, DateBeforeTodayException {
    	//CREATE CATEGORY
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	//CREATE EVENT
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2016, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute
        Event event = eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDate);
        
        //CREATE BETTYPE
        List<Option> options = new ArrayList<Option>();
        Option optionWinnerDeportivo = new Option ("Deportivo", 1.5);
        Option optionWinnerRealMadrid = new Option ("Real Madrid", 1);
        options.add(optionWinnerDeportivo);
        options.add(optionWinnerRealMadrid);
        
        BetType betType = 
        		eventService.createBetType(event.getEventId(), "¿Quien será el ganador?", options); 
    
        List<Long> winnerOptions = new ArrayList<Long>();
        winnerOptions.add(optionWinnerDeportivo.getOptionId());
        
        eventService.winnerOptions(winnerOptions, betType.getBetTypeId());
        
        assertTrue(optionWinnerDeportivo.isWinner());
        assertFalse(optionWinnerRealMadrid.isWinner());
    }
    
    //*********** WINNER OPTIONS LIST EMPTY **********
    @Test
    public void testWinnerOptionsEmpty() throws InstanceNotFoundException, DateBeforeTodayException{
    	//CREATE CATEGORY
    	Category categoryFutbol = new Category("futbol");
    	categoryDao.save(categoryFutbol);
    	Long idFutbol = categoryFutbol.getCategoryId();
    	
    	//CREATE EVENT
    	Calendar startDate = Calendar.getInstance();
    	startDate.set(2016, 05, 20, 16, 30); // Create a date whit year, month, day, hour and minute
        Event event = eventService.createEvent("Deportivo - Real Madrid", idFutbol, startDate);
        
        //CREATE BETTYPE
        List<Option> options = new ArrayList<Option>();
        Option optionWinnerDeportivo = new Option ("Deportivo", 1.5);
        Option optionWinnerRealMadrid = new Option ("Real Madrid", 1);
        options.add(optionWinnerDeportivo);
        options.add(optionWinnerRealMadrid);
                
        BetType betType = 
        		eventService.createBetType(event.getEventId(), "¿Quien será el ganador?", options); 
    
        List<Long> winnerOptions = new ArrayList<Long>();
        
        eventService.winnerOptions(winnerOptions, betType.getBetTypeId());
        
        assertFalse(optionWinnerDeportivo.isWinner());
        assertFalse(optionWinnerRealMadrid.isWinner());
    }
    
    //*********** WINNER OPTIONS NON EXISTENT BETTYPE **********
    @Test(expected = InstanceNotFoundException.class)
    public void testWinnerOptionsNonExistentBetType() throws InstanceNotFoundException {
        eventService.winnerOptions(null, NON_EXISTENT_BETTYPE_ID);;
    }
    
    
}
