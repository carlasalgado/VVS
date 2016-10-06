package es.udc.pa011.test.model.betservice;

import static es.udc.pa011.model.util.GlobalNames.SPRING_CONFIG_FILE;
import static es.udc.pa011.test.util.GlobalNames.SPRING_CONFIG_TEST_FILE;
import static org.junit.Assert.assertEquals;
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

import es.udc.pa011.model.bet.Bet;
import es.udc.pa011.model.betservice.BetBlock;
import es.udc.pa011.model.betservice.BetService;
import es.udc.pa011.model.betservice.InputValidationException;
import es.udc.pa011.model.bettype.BetType;
import es.udc.pa011.model.bettype.BetTypeDao;
import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.category.CategoryDao;
import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.event.EventDao;
import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.option.OptionDao;
import es.udc.pa011.model.userprofile.UserProfile;
import es.udc.pa011.model.userprofile.UserProfileDao;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = { SPRING_CONFIG_FILE, SPRING_CONFIG_TEST_FILE })
@Transactional
public class BetServiceTest {

    @Autowired
    private BetService betService;
    
    @Autowired
    private UserProfileDao userProfileDao;
    
    @Autowired
    private BetTypeDao betTypeDao;
    
    @Autowired
    private EventDao eventDao;
    
    @Autowired
    private CategoryDao categoryDao;

    @Autowired
    private OptionDao optionDao;
    
    /* CREATE AND FIND BET */
    @Test
    public void testCreateAndFindBetByUserId() throws InstanceNotFoundException, InputValidationException{
    	
    	Calendar date = Calendar.getInstance();
    	date.set(2016, 1, 01, 16, 30);
    	
    	UserProfile userProfile = new UserProfile("loginName", "encryptedPassword", "firstName", "lastName", "email"); 	
    	userProfileDao.save(userProfile);
    	userProfile = userProfileDao.findByLoginName(userProfile.getLoginName());
    	
    	UserProfile userProfile2 = new UserProfile("loginName2", "encryptedPassword", "firstName", "lastName", "email"); 	
    	userProfileDao.save(userProfile2);
    	userProfile2 = userProfileDao.findByLoginName(userProfile2.getLoginName());
    	
    	Category category = new Category("category");
    	categoryDao.save(category);
    	
    	Event event = new Event("name", category, date);
    	eventDao.save(event);
    	
    	BetType betType = new BetType("question", event);
    	betTypeDao.save(betType);
    	
    	Option option = new Option("option", 1.5, betType);
    	optionDao.save(option);

    	/*Create three events that should be found*/
    	int numberOfBets = 3;
    	List<Bet> expectedBets = new ArrayList<Bet>();
    	
    	betService.createBet(date, 2.2, userProfile2.getUserProfileId(), option.getOptionId());
    	expectedBets.add(betService.createBet(date, 1.5, userProfile.getUserProfileId(), option.getOptionId()));
    	expectedBets.add(betService.createBet(date, 2.1, userProfile.getUserProfileId(), option.getOptionId()));
    	betService.createBet(date, 1.3, userProfile2.getUserProfileId(), option.getOptionId());
    	expectedBets.add(betService.createBet(date, 1.7, userProfile.getUserProfileId(), option.getOptionId()));

    	
    	/* Check operations with correct date range and order. */
        BetBlock betBlock;
        int count = 2;
        int startIndex = 0;
        int resultIndex = 0;

        do {

        	betBlock = betService.findBetByUserProfileId(userProfile.getUserProfileId(), startIndex, count);
            
            assertTrue(betBlock.getBets().size() <= count);
            
            for (Bet bet : betBlock.getBets()) {
                assertTrue(bet == expectedBets.get(resultIndex++));
            }
            startIndex += count;

        } while (betBlock.getExistMoreBets());

        assertTrue(numberOfBets == startIndex - count + betBlock.getBets().size());
    }
    
    /* CREATE A BET WHIT NON EXISTEN USERPROFILE */
    @Test(expected = InstanceNotFoundException.class)
    public void testNonExistenOption() throws InstanceNotFoundException, InputValidationException{
    	
    	final long NON_EXISTENT_OPTION_ID = -1;
    	
    	Calendar date = Calendar.getInstance();
    	date.set(2016, 1, 01, 16, 30);
    	
    	UserProfile userProfile = new UserProfile("loginName", "encryptedPassword", "firstName", "lastName", "email"); 	
    	userProfileDao.save(userProfile);
    	userProfile = userProfileDao.findByLoginName(userProfile.getLoginName());
    	
    	betService.createBet(date, 1.5, userProfile.getUserProfileId(), NON_EXISTENT_OPTION_ID);
    }
    
    /* CREATE A BET WHIT NON EXISTEN OPTION */
    @Test(expected = InstanceNotFoundException.class)
    public void testNonExistenProfile() throws InstanceNotFoundException, InputValidationException{
    	
    	final long NON_EXISTENT_USERPROFILE_ID = -1;
    	
    	Calendar date = Calendar.getInstance();
    	date.set(2016, 1, 01, 16, 30);
    	
    	Category category = new Category("category");
    	categoryDao.save(category);
    	
    	Event event = new Event("name", category, date);
    	eventDao.save(event);
    	
    	BetType betType = new BetType("question", event);
    	betTypeDao.save(betType);
    	
    	Option option = new Option("option", 1.5, betType);
    	optionDao.save(option);
    	
    	betService.createBet(date, 1.5, NON_EXISTENT_USERPROFILE_ID, option.getOptionId());
    }
    
    /* CREATE A BET WHIT NEGATIVE AMOUNT*/
    @Test(expected = InputValidationException.class)
    public void testNegativeAmount() throws InstanceNotFoundException, InputValidationException{
    	
    	final long NON_EXISTENT_USERPROFILE_ID = -1;
    	
    	Calendar date = Calendar.getInstance();
    	date.set(2016, 1, 01, 16, 30);
    	
    	Category category = new Category("category");
    	categoryDao.save(category);
    	
    	Event event = new Event("name", category, date);
    	eventDao.save(event);
    	
    	BetType betType = new BetType("question", event);
    	betTypeDao.save(betType);
    	
    	Option option = new Option("option", 1.5, betType);
    	optionDao.save(option);
    	
    	betService.createBet(date, -1.5, NON_EXISTENT_USERPROFILE_ID, option.getOptionId());
    }
    
    /* FIND NON EXISTENT BET */
    @Test
    public void testNonExistentBet() throws InstanceNotFoundException, InputValidationException{
    	
    	Calendar date = Calendar.getInstance();
    	date.set(2016, 1, 01, 16, 30);
    	
    	UserProfile userProfile = new UserProfile("loginName", "encryptedPassword", "firstName", "lastName", "email"); 	
    	userProfileDao.save(userProfile);
    	userProfile = userProfileDao.findByLoginName(userProfile.getLoginName());
    	
    	UserProfile userProfile2 = new UserProfile("loginName2", "encryptedPassword", "firstName", "lastName", "email"); 	
    	userProfileDao.save(userProfile2);
    	userProfile2 = userProfileDao.findByLoginName(userProfile2.getLoginName());
    	
    	Category category = new Category("category");
    	categoryDao.save(category);
    	
    	Event event = new Event("name", category, date);
    	eventDao.save(event);
    	
    	BetType betType = new BetType("question", event);
    	betTypeDao.save(betType);
    	
    	Option option = new Option("option", 1.5, betType);
    	optionDao.save(option);
    	
    	betService.createBet(date, 1.5, userProfile.getUserProfileId(), option.getOptionId());
    	betService.createBet(date, 2.1, userProfile.getUserProfileId(), option.getOptionId());
    	betService.createBet(date, 1.7, userProfile.getUserProfileId(), option.getOptionId());

    	
    	/* Check operations with correct date range and order. */
    	List<Bet> expectedBets = new ArrayList<Bet>();
        BetBlock betBlock;
        int count = 2;
        int startIndex = 0;

        do {

        	betBlock = betService.findBetByUserProfileId(userProfile2.getUserProfileId(), startIndex, count);
            
            assertTrue(betBlock.getBets().size() <= count);
         
            assertEquals(betBlock.getBets(), expectedBets);
            startIndex += count;

        } while (betBlock.getExistMoreBets());
    }
}
