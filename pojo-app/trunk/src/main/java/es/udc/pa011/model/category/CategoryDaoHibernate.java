package es.udc.pa011.model.category;

import java.util.List;

import org.springframework.stereotype.Repository;

import es.udc.pojo.modelutil.dao.GenericDaoHibernate;

@Repository("categoryDao")
public class CategoryDaoHibernate extends GenericDaoHibernate<Category, Long> implements CategoryDao {
	@SuppressWarnings("unchecked")
	public List<Category> findAllCategories() {
		return getSession().createQuery("SELECT c FROM Category c " +
		            "ORDER BY c.name").list();
	}
}
