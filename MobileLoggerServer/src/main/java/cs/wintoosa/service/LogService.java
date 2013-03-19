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
    
    @Autowired
    private IPhoneRepository phoneRepositoryImpl;
    
    @PersistenceContext
    EntityManager em;

    @Override
    @Transactional
    public boolean saveLog(Log log) {
        if (log == null || log.getPhoneId() == null) {
            return false;
        }
        Phone phone = phoneRepositoryImpl.findByPhoneId(log.getPhoneId());
        if(phone == null) {
            phone = new Phone();
            phone.setPhoneId(log.getPhoneId());
            phone = phoneRepositoryImpl.saveAndFlush(phone);
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
            List<Log> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c", cls).getResultList();
            System.out.println("resultList.size() = " + resultList.size());
            return resultList;
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
        List<SessionLog> sessionLogs = sessionRepositoryImpl.findAll();
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
        System.out.println("getSessionByPhoneId");
        List<SessionLog> sessions = sessionRepositoryImpl.findSessionByPhoneId(phoneId);
        System.out.println("sessions.size() = " + sessions.size());
        return sessions;
    }
    
    /**
     * Saves the given session log into db
     * @param sessionLog
     * @return the saved SessionLog or null if save failed
     */
    @Override
    @Transactional
    public SessionLog saveLog(SessionLog sessionLog){
        
        String phoneId = sessionLog.getPhoneId();
        
        if(phoneId== null)
            return null;
        
        //first try to find the phone with the ID in the sessionLog
        Phone phone = phoneRepositoryImpl.findByPhoneId(phoneId);
        if(phone == null){ //if it doesn't exist in the DB, create one
            phone = new Phone();
            phone.setPhoneId(phoneId);
            phone = phoneRepositoryImpl.save(phone);
        }
        //set the phone into sessionlog
        sessionLog.setPhone(phone);
        sessionLog = sessionRepositoryImpl.save(sessionLog); 
        
        //now add the saved sessionlog into phone session list
        phone.getSessions().add(sessionLog);
        phoneRepositoryImpl.save(phone);
        
        return sessionLog;
    }

    @Override
    @Transactional(readOnly= true)
    public List<String> getAllPhoneIds() {
        
        List<String> phoneIds = em.createQuery("SELECT DISTINCT PHONEID FROM SESSIONLOG").getResultList();
        return phoneIds;
    }

    
}
