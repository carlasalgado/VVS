package es.udc.pa011.web.components;

import org.apache.tapestry5.annotations.Import;
import org.apache.tapestry5.annotations.Parameter;
import org.apache.tapestry5.annotations.Property;
import org.apache.tapestry5.annotations.SessionState;
import org.apache.tapestry5.ioc.annotations.Inject;
import org.apache.tapestry5.services.Cookies;

import es.udc.pa011.model.userservice.UserService;
import es.udc.pa011.web.pages.Index;
import es.udc.pa011.web.services.AuthenticationPolicy;
import es.udc.pa011.web.services.AuthenticationPolicyType;
import es.udc.pa011.web.util.CookiesManager;
import es.udc.pa011.web.util.UserSession;
import es.udc.pojo.modelutil.exceptions.InstanceNotFoundException;

@Import(library = {"tapestry5/bootstrap/js/collapse.js", "tapestry5/bootstrap/js/dropdown.js"},
        stylesheet="tapestry5/bootstrap/css/bootstrap-theme.css")
public class Layout {
	
//    private Long userId;
//    private String login;
//    private boolean admin;
//    private boolean user;


    @Property
    @Parameter(required = true, defaultPrefix = "message")
    private String title;
    
    @Parameter(defaultPrefix = "literal")
    private Boolean showTitleInBody;

    @Property
    @SessionState(create=false)
    private UserSession userSession;
    
    @Inject
    private Cookies cookies;
    
    @Inject
    private UserService userService;
    
//    public boolean isUser() {
//		return user;
//	}
//
//	public void setUser(boolean user) {
//		this.user = user;
//	}
//
//	public boolean isAdmin() {
//		return admin;
//	}
//
//	public void setAdmin(boolean admin) {
//		this.admin = admin;
//	}

	public boolean getShowTitleInBody() {
    	if (showTitleInBody == null) {
    		return true;
    	} else {
    		return showTitleInBody;
    	}
    	
    }
    
    @AuthenticationPolicy(AuthenticationPolicyType.AUTHENTICATED_USERS)
    Object onActionFromLogout() {
        userSession = null;
        CookiesManager.removeCookies(cookies);
        return Index.class;
    }
    
//    void onActivate() throws InstanceNotFoundException{
//    	if(userSession!=null){
//			userId = userSession.getUserProfileId();
//			login = userService.findUserProfile(userId).getLoginName();
//			
//			if (login.compareTo("admin")==0){
//				user = false;
//				admin = true;
//			}
//			else{
//				user = true;
//				admin=false;
//			}
//		}
//    }

}
