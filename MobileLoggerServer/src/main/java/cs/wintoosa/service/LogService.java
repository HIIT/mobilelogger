package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import cs.wintoosa.repository.*;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import org.eclipse.persistence.exceptions.QueryException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

/**
 *
 * @author jonimake
 */
@Service
public class LogService implements ILogService {

    @Autowired
    private ILogRepository logRepositoryImpl;
    
    @Autowired
    private ISessionRepository sessionRepositoryImpl;
    
    @PersistenceContext
    EntityManager em;

    public void setLogRepositoryImpl(ILogRepository logRepositoryImpl) {
        this.logRepositoryImpl = logRepositoryImpl;
    }

    public ILogRepository getLogRepositoryImpl() {
        return logRepositoryImpl;
    }

    @Override
    @Transactional
    public boolean saveLog(Log log) {
        if (log == null || log.getPhoneId() == null) {
            return false;
        }
        log = logRepositoryImpl.save(log);
        return true;
    }
    /**
     * Todo: Put this in a repository class
     * @param cls the class of the log entry
     * @return 
     */
    @Override
    @Transactional(readOnly = true)
    public List<Log> getAll(Class cls) {
        try {
            return em.createNativeQuery("SELECT * FROM " + cls.getSimpleName().toUpperCase(), cls).getResultList();
        } catch (QueryException e) {
            System.out.println("Failed query!");
            System.out.println(e.getQuery().getSQLString());
            System.out.println(e.getMessage());
        }
        catch (Exception e) {
            System.out.println(e.toString());
        }
        return null;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Log> getAll() {
        List<Log> logs = logRepositoryImpl.findAll();
        return logs;
    }
    
    @Override
    @Transactional(readOnly = true)
    public List<SessionLog> getAllSessions() {
        return sessionRepositoryImpl.findAll();
    }
    
    @Override
    @Transactional(readOnly = true)
    public SessionLog getSessionById(long sessionId) {
        return sessionRepositoryImpl.findSessionById(sessionId);
    }
    
    @Override
    @Transactional(readOnly = true)
    public List<SessionLog> getSessionByPhoneId(String phoneId){
        return sessionRepositoryImpl.findSessionByPhoneId(phoneId);
    }
    
    @Override
    @Transactional
    public SessionLog saveLog(SessionLog sessionLog){
        return sessionRepositoryImpl.saveAndFlush(sessionLog);
    }
    
// <editor-fold defaultstate="collapsed">
  public void setSessionRepositoryImpl(ISessionRepository sessionRepositoryImpl) {
        this.sessionRepositoryImpl = sessionRepositoryImpl;
    }

    public ISessionRepository getSessionRepositoryImpl() {
        return sessionRepositoryImpl;
    }
// </editor-fold>

    
}
