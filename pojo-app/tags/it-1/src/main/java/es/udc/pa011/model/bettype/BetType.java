package es.udc.pa011.model.bettype;

import java.util.HashSet;
import java.util.Set;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

import es.udc.pa011.model.event.Event;
import es.udc.pa011.model.option.Option;

@Entity
@Table(name="TipoApuesta")
public class BetType {
	private Long betTypeId;
	private String question;
	private Event event;
	private Set<Option> options;

	
	public BetType(){}
	
	public BetType(String question, Event event){
		this.question = question;
		this.event = event;
		this.options = new HashSet<Option>();
	}
	
	@Column(name="idTipoApuesta")
    @SequenceGenerator(name="BetTypeIdGenerator", sequenceName="BetTypeSeq")    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO, generator="BetTypeIdGenerator")
	public Long getBetTypeId() {
		return betTypeId;
	}
	public void setBetTypeId(Long betTypeId) {
		this.betTypeId = betTypeId;
	}
	
	@Column(name="pregunta")
	public String getQuestion() {
		return question;
	}
	public void setQuestion(String question) {
		this.question = question;
	}
	
	@ManyToOne(optional=false, fetch=FetchType.LAZY)
    @JoinColumn(name="idEvento")
	public Event getEvent() {
		return event;
	}
	public void setEvent(Event event) {
		this.event = event;
	}

	@OneToMany(mappedBy = "bettype")
	public Set<Option> getOptions() {
		return options;
	}
	public void setOptions(Set<Option> options) {
		this.options = options;
	}

	@Override
	public String toString() {
		return "BetType [betTypeId=" + betTypeId + ", question=" + question
				+ ", event=" + event + "]";
	}	
}
