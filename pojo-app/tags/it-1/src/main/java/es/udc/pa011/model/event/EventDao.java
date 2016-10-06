package es.udc.pa011.model.event;

import java.util.List;

import es.udc.pojo.modelutil.dao.GenericDao;

public interface EventDao extends GenericDao<Event, Long>{
	
	public List<Event> findByKeyWordsAndCategory(String keyWords, Long categoryId,
			int startIndex, int count);
	public List<Event> findByKeyWordsAndCategoryAdmin(String keyWords, Long categoryId,
			int startIndex, int count);
}
