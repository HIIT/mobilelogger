package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import cs.wintoosa.repository.*;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
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
        return em.createNativeQuery("SELECT * FROM " + cls.getSimpleName().toUpperCase(), cls).getResultList();
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
        List<SessionLog> sessionLogs = sessionRepositoryImpl.findAll();
        System.out.println("sessionLogs.size() = " + sessionLogs.size());
        return sessionLogs;
    }
    
    @Override
    @Transactional(readOnly = true)
    public SessionLog getSessionById(long sessionId) {
        SessionLog session = sessionRepositoryImpl.findSessionById(sessionId);
        session.setLogs(logRepositoryImpl.findByPhoneIdAndTimestampBetween(session.getPhoneId(), session.getSessionStart(), session.getSessionEnd()));
        return session;
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

    @Override
    @Transactional(readOnly= true)
    public List<String> getAllPhoneIds() {
        
        List<String> phoneIds = em.createNativeQuery("SELECT DISTINCT PHONEID FROM SESSIONLOG").getResultList();
        return phoneIds;
    }

    
}
