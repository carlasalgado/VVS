package es.udc.pa011.model.betservice;

@SuppressWarnings("serial")
public class InvalidDateException extends Exception {

	public InvalidDateException(String message) {
        super(message);
    }
}
