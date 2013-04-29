/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Sessionlog;
import cs.wintoosa.domain.Text;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

/**
 *
 * @author jonimake
 */
@Repository
public class LogRepositoryImpl implements LogRepositoryCustom {
    
    @PersistenceContext
    EntityManager em;

    @Transactional(readOnly=true)
    @Override
    public <T extends Abstractlog> List<T> findBySessionlog(Class<T> cls, Sessionlog session) throws IllegalArgumentException{
        if (cls == null) {
            return null;
        }
        List<T> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c WHERE c.sessionlog.id = "+session.getId()+" order by c.timestamp asc", cls).getResultList();
        return resultList;
    }
    
    @Transactional(readOnly=true)
    @Override
    public <T extends Abstractlog> List<T> findAll(Class<T> cls) throws IllegalArgumentException {
        if (cls == null) {
            return null;
        }
        return em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c", cls).getResultList();
    }
    
    @Transactional(readOnly=true)
    @Override
    public List<Text> findTextByType(String type){
        return em.createQuery("SELECT c FROM " + Text.class.getSimpleName() + " c WHERE c.type = '" + type+"'", Text.class).getResultList();
    }
}
