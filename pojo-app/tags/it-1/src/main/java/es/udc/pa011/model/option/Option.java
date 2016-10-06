package es.udc.pa011.model.option;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

import es.udc.pa011.model.bettype.BetType;

@Entity
@Table(name="Opcion")
public class Option {
	private Long OptionId;
	private String option;
	private double share;
	private Boolean winner;
	private BetType bettype;

	public Option(){}
	
	public Option(String option, double share){
		this.option = option;
		this.share = share;
		this.winner = null;
		this.bettype = null;		
	}
	
	public Option(String option, double share, BetType bettype){
		this.option = option;
		this.share = share;
		this.winner = null;
		this.bettype = bettype;		
	}

	@Column(name="idOpcion")
    @SequenceGenerator(name="OptionIdGenerator", sequenceName="OptionSeq")    
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO, generator="OptionIdGenerator")
	public Long getOptionId() {
		return OptionId;
	}

	public void setOptionId(Long optionId) {
		OptionId = optionId;
	}

	@Column(name="opcion")
	public String getOption() {
		return option;
	}

	public void setOption(String option) {
		this.option = option;
	}

	@Column(name="cuota")
	public double getShare() {
		return share;
	}

	public void setShare(double share) {
		this.share = share;
	}

	@Column(name="ganadora")
	public Boolean isWinner() {
		return winner;
	}

	public void setWinner(Boolean winner) {
		this.winner = winner;
	}
	
	@ManyToOne(optional=false, fetch=FetchType.LAZY)
    @JoinColumn(name="idTipoApuesta")
	public BetType getBettype() {
		return bettype;
	}

	public void setBettype(BetType bettype) {
		this.bettype = bettype;
	}

	@Override
	public String toString() {
		return "Option [OptionId=" + OptionId + ", option=" + option
				+ ", share=" + share + ", winner=" + winner + ", bettype="
				+ bettype + "]";
	}
}
