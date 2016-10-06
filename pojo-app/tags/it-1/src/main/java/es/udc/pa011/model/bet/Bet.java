package es.udc.pa011.model.bet;

import java.util.Calendar;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import es.udc.pa011.model.option.Option;
import es.udc.pa011.model.userprofile.UserProfile;

@Entity
@Table(name="Apuesta")
public class Bet{
	private Long betId;
	private Calendar date;
	private double amount;
	private UserProfile userProfile;
	private Option option;
	
	public Bet(){}
	
	public Bet(Calendar date, double amount, UserProfile userProfile, Option option){
		this.date = date;
		this.amount = amount;
		this.userProfile = userProfile;
		this.option = option;
	}
	
	@Column(name="idApuesta")
    @SequenceGenerator(name="BetIdGenerator", sequenceName="BetSeq")    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO, generator="BetIdGenerator")
	public Long getBetTypeId() {
		return betId;
	}
	public void setBetTypeId(Long betId) {
		this.betId = betId;
	}
	
	@Column(name="fecha")
    @Temporal(TemporalType.TIMESTAMP)
    public Calendar getDate() {
        return date;
    }

    public void setDate(Calendar date) {
        this.date = date;
    }
	
	@Column(name="importe")
	public double getQuestion() {
		return amount;
	}
	public void setQuestion(double amount) {
		this.amount = amount;
	}
	
	@ManyToOne(optional=false, fetch=FetchType.LAZY)
    @JoinColumn(name="idUsuario")
	public UserProfile getUserProfile() {
		return userProfile;
	}
	public void setUserProfile(UserProfile userProfile) {
		this.userProfile = userProfile;
	}

	@OneToOne(optional=false, fetch=FetchType.LAZY)
	@JoinColumn(name="idOpcion")
	public Option getOption() {
		return option;
	}
	public void setOption(Option option) {
		this.option = option;
	}

	@Override
	public String toString() {
		return "Bet [betId=" + betId + ", date=" + date +", amount" + amount
				+ ", userProfile=" + userProfile + ", option" + option + "]";
	}	
}

