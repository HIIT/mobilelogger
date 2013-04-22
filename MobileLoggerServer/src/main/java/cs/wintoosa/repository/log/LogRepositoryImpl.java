/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.domain.TextLog;
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
    public <T extends Log> List<T> findBySessionLog(Class<T> cls, SessionLog session) throws IllegalArgumentException{
        if (cls == null) {
            return null;
        }
        List<T> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c WHERE c.sessionLog.id = "+session.getId()+" order by c.timestamp asc", cls).getResultList();
        return resultList;
    }
    
    @Transactional(readOnly=true)
    @Override
    public <T extends Log> List<T> findAll(Class<T> cls) throws IllegalArgumentException {
        if (cls == null) {
            return null;
        }
        return em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c", cls).getResultList();
    }
    
    @Transactional(readOnly=true)
    @Override
    public List<TextLog> findTextLogByType(String type){
        return em.createQuery("SELECT c FROM " + TextLog.class.getSimpleName() + " c WHERE c.type = '" + type+"'", TextLog.class).getResultList();
    }
}
