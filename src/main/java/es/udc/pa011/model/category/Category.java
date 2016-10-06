package es.udc.pa011.model.category;

import javax.annotation.concurrent.Immutable;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Immutable
@Table(name="Categoria")
public class Category {
	private Long categoryId;
	private String name;
	
	public Category(){}
	
	public Category(String name) {
		this.name = name;
	}
	
	@Column(name="idCategoria")
    @SequenceGenerator(             // It only takes effect for
         name="CategoryIdGenerator", // databases providing identifier
         sequenceName="CategorySeq") // generators.
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO,
                    generator="CategoryIdGenerator")
	public Long getCategoryId() {
		return categoryId;
	}

	public void setCategoryId(Long categoryId) {
		this.categoryId = categoryId;
	}
	
	@Column(name="nombre")
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	@Override
	public String toString() {
		return "Category [CategoryId=" + categoryId + ", name=" + name + "]";
	}	
}
