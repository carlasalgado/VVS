package es.udc.pa011.model.option;

import org.springframework.stereotype.Repository;

import es.udc.pojo.modelutil.dao.GenericDaoHibernate;

@Repository("optionDao")
public class OptionDaoHibernate extends GenericDaoHibernate<Option, Long> implements OptionDao{

}
