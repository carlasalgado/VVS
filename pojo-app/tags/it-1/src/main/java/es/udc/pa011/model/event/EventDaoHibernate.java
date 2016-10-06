package es.udc.pa011.model.event;

import java.util.Calendar;
import java.util.List;

import org.hibernate.Criteria;
import org.hibernate.criterion.MatchMode;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;
import org.springframework.stereotype.Repository;

import es.udc.pojo.modelutil.dao.GenericDaoHibernate;

@Repository("eventDao")
public class EventDaoHibernate extends GenericDaoHibernate<Event, Long> implements EventDao {

	@SuppressWarnings("unchecked")
	public List<Event> findByKeyWordsAndCategory(String keyWords,
			Long categoryId, int startIndex, int count) {
		
		String[] words = keyWords!= null ? keyWords.split(" ") : null; // trocea la cadena introducida y la almacena en las posiciones del array
    	Criteria criteria = getSession().createCriteria(Event.class);
    	
    	if(words != null) {
            for(String word : words) {
                if(word != null && word.length() > 0) {
                    criteria.add(Restrictions.ilike("name", word, MatchMode.ANYWHERE));
                }
            }
        }
    	
    	if(categoryId != null)
        	criteria.add(Restrictions.naturalId().set("category.categoryId", categoryId));	//restringe la busqueda a la categoria indicada
    	
    	criteria.add(Restrictions.gt("date", Calendar.getInstance())); //comprueba que date sea mayor que la fecha actual
    	
    	criteria.addOrder(Order.asc("date")); // ordena por fecha
    	
    	return criteria.setFirstResult(startIndex).setMaxResults(count).list(); //obtiene count resultados a partir de startIndex
	}

	@SuppressWarnings("unchecked")
	public List<Event> findByKeyWordsAndCategoryAdmin(String keyWords, Long categoryId,
			int startIndex, int count){
		
		String[] words = keyWords!= null ? keyWords.split(" ") : null; // trocea la cadena introducida y la almacena en las posiciones del array
    	Criteria criteria = getSession().createCriteria(Event.class);
    	
    	if(words != null) {
            for(String word : words) {
                if(word != null && word.length() > 0) {
                    criteria.add(Restrictions.ilike("name", word, MatchMode.ANYWHERE));
                }
            }
        }
    	
    	if(categoryId != null)
        	criteria.add(Restrictions.naturalId().set("category.categoryId", categoryId));	//restringe la busqueda a la categoria indicada
    	    	
    	criteria.addOrder(Order.asc("date")); // ordena por fecha
    	
    	return criteria.setFirstResult(startIndex).setMaxResults(count).list(); //obtiene count resultados a partir de startIndex
	}
}
