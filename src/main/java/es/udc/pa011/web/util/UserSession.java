package es.udc.pa011.web.util;

import java.util.ArrayList;
import java.util.List;

import es.udc.pa011.model.option.Option;

public class UserSession {
	private List<Option> opciones = new ArrayList<Option>();
	private Long userProfileId;
	private String firstName;
	private String login;

	public Long getUserProfileId() {
		return userProfileId;
	}

	public void setUserProfileId(Long userProfileId) {
		this.userProfileId = userProfileId;
	}

	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public List<Option> getOpciones() {
		return opciones;
	}

	public void setOpciones(List<Option> opciones) {
		this.opciones = opciones;
	}

	public String getLogin() {
		return login;
	}

	public void setLogin(String login) {
		this.login = login;
	}
	
	

}
