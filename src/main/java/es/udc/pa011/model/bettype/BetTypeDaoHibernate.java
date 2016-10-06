package es.udc.pa011.model.bettype;

import org.springframework.stereotype.Repository;

import es.udc.pojo.modelutil.dao.GenericDaoHibernate;

@Repository("bettypeDao")
public class BetTypeDaoHibernate extends GenericDaoHibernate<BetType, Long> implements BetTypeDao{
}
