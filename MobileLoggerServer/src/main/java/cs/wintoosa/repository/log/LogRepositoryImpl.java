/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
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

    /**
     * Returns all entries for session from table T
     * @param <T> the type of the class
     * @param cls the class, has to be subclass of Log.class
     * @param session the session of which logs to retrieve
     * @return the logs of type T of session
     * @throws IllegalArgumentException if the class<T> table doesn't exist in DB
     */
    @Transactional(readOnly=true)
    @Override
    public <T extends Log> List<T> findBySessionLog(Class<T> cls, SessionLog session) throws IllegalArgumentException{
        if (cls == null) {
            return null;
        }
        List<T> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c WHERE c.sessionLog.id = "+session.getId()+" order by c.timestamp asc", cls).getResultList();
        return resultList;
    }
    
    /**
     * Returns all logs of type T
     * @param <T> the type of the class
     * @param cls the class, has to be subclass of Log.class
     * @return the logs of type T
     * @throws IllegalArgumentException 
     */
    @Transactional(readOnly=true)
    @Override
    public <T extends Log> List<T> findAll(Class<T> cls) throws IllegalArgumentException {
        if (cls == null) {
            return null;
        }
        return em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c", cls).getResultList();
    }
}
