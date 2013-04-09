package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import cs.wintoosa.repository.log.ILogRepository;
import cs.wintoosa.repository.phone.IPhoneRepository;
import cs.wintoosa.repository.session.ISessionRepository;
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
        List<SessionLog> sessions = sessionRepositoryImpl.findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(log.getPhoneId(), log.getTimestamp(), log.getTimestamp());
        for (SessionLog session : sessions) {
            log.setSessionLog(session);
        }
        log = logRepositoryImpl.save(log);
        return true;
    }

    /**
     * Todo: Put this in a repository class
     *
     * @param cls the class of the log entry
     * @return
     */
    @Override
    @Transactional(readOnly = true)
    public List<Log> getAll(Class cls) throws IllegalArgumentException {
        if (cls == null) {
            return null;
        }
        List<Log> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c", cls).getResultList();
        return resultList;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Log> getAllBySessionId(Class cls) {
        if (cls == null) {
            return null;
        }
        List<Log> resultList = em.createQuery("SELECT c FROM " + cls.getSimpleName() + " c, WHERE ", cls).getResultList();
        return resultList;
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
        SessionLog session = sessionRepositoryImpl.findOne(sessionId);
        return session;
    }

    @Override
    @Transactional(readOnly = true)
    public List<SessionLog> getSessionByPhoneId(String phoneId) {
        List<SessionLog> sessions = sessionRepositoryImpl.findByPhoneId(phoneId);
        return sessions;
    }

    /**
     * Saves the given session log into db
     *
     * @param sessionLog
     * @return the saved SessionLog or null if save failed
     */
    @Override
    @Transactional
    public SessionLog saveSessionLog(SessionLog sessionLog) {

        String phoneId = sessionLog.getPhoneId();
        Phone phone = phoneRepositoryImpl.findOne(phoneId);
        if (phone == null) {
            phone = new Phone();
            phone.setId(phoneId);
            phone = phoneRepositoryImpl.saveAndFlush(phone);
        }
        List<Log> logs = logRepositoryImpl.findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(phoneId, sessionLog.getSessionStart(), sessionLog.getSessionEnd());

        /*sessionLog.setPhone(phone);*/
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        phone.getSessions().add(sessionLog);
        phone = phoneRepositoryImpl.saveAndFlush(phone);
        
        for (Log log : logs) {
            log.setSessionLog(sessionLog);
        }
        logRepositoryImpl.save(logs);
        return sessionLog;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Phone> getAllPhones() {

        return phoneRepositoryImpl.findAll();
    }
}
