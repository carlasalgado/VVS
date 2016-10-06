package es.udc.pa011.model.event;

import java.util.List;

import es.udc.pojo.modelutil.dao.GenericDao;

public interface EventDao extends GenericDao<Event, Long>{
	
	public List<Event> findByKeyWordsAndCategory(String keyWords, Boolean isAdmin,
			Long categoryId, int startIndex, int count);
}
