package es.udc.pa011.model.event;

import java.util.Calendar;
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
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import es.udc.pa011.model.category.Category;
import es.udc.pa011.model.bettype.BetType;

@Entity
@Table(name="Evento")
public class Event {
	private Long eventId;
	private String name;
	private Category category;
	private Calendar date;
	private Set<BetType> betTypes;
	
	public Event(){}
	
	public Event(String name, Category category, Calendar date) {
		this.name = name;
		this.category = category;
		this.date = date;
		this.betTypes = new HashSet<BetType>();
	}
	
	@Column(name="idEvento")
    @SequenceGenerator(name="EventIdGenerator", sequenceName="EventSeq")    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO, generator="EventIdGenerator")
	public Long getEventId() {
		return eventId;
	}
	
	public void setEventId(Long eventId) {
		this.eventId = eventId;
	}
	
	@Column(name="nombre")
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	
	@Column(name="fecha")
    @Temporal(TemporalType.TIMESTAMP)
	public Calendar getDate() {
		return date;
	}
	public void setDate(Calendar date) {
		this.date = date;
	}

	@ManyToOne(optional=false, fetch=FetchType.LAZY)
    @JoinColumn(name="idCategoria")
	public Category getCategory() {
		return category;
	}
	public void setCategory(Category category) {
		this.category = category;
	}
	
	@OneToMany(mappedBy = "event")
	public Set<BetType> getBetTypes() {
		return betTypes;
	}
	public void setBetTypes(Set<BetType> betTypes) {
		this.betTypes = betTypes;
	}
	
	@Override
	public String toString() {
		return "Event [EventId=" + eventId + ", name=" + name
				+ ", category=" + category + ", date=" + date + "]";
	}
		
}

