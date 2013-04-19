package cs.wintoosa.service;

import com.google.gson.Gson;
import cs.wintoosa.domain.*;
import cs.wintoosa.repository.log.LogRepository;
import cs.wintoosa.repository.phone.PhoneRepository;
import cs.wintoosa.repository.session.SessionRepository;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;
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
    private LogRepository logRepositoryImpl;
    @Autowired
    private SessionRepository sessionRepositoryImpl;
    @Autowired
    private PhoneRepository phoneRepositoryImpl;
    @PersistenceContext
    EntityManager em;
    
    private static final Logger logger = Logger.getLogger(LogService.class.getName());

    @Override
    @Transactional
    public boolean saveLog(Log log) {
        if (log == null || log.getPhoneId() == null) {
            return false;
        }
        
        List<SessionLog> sessions = sessionRepositoryImpl.findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(log.getPhoneId(), log.getTimestamp(), log.getTimestamp());
        assert(sessions.size() <= 1);
        for (SessionLog session : sessions) {
            log.setSessionLog(session);
        }
        log = logRepositoryImpl.save(log);
        ;
        logger.info("Saved log:\n\t " + new Gson().toJson(log));
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
        return logRepositoryImpl.findAll(cls);
    }
    
    @Override
    @Transactional(readOnly = true)
    public <T extends Log> List<T> getAllBySessionId(Class<T> cls, SessionLog session) {
        return logRepositoryImpl.findBySessionLog(cls, session);
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
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        phone.getSessions().add(sessionLog);
        phoneRepositoryImpl.saveAndFlush(phone);
        
        List<Log> logs = logRepositoryImpl.findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(phoneId, sessionLog.getSessionStart(), sessionLog.getSessionEnd());
        for (Log log : logs) {
            log.setSessionLog(sessionLog);
        }
        sessionLog.getLogs().addAll(logs);
        
        logRepositoryImpl.save(logs);
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        return sessionLog;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Phone> getAllPhones() {

        return phoneRepositoryImpl.findAll();
    }

    @Override
    @Transactional(readOnly = true)
    public List<Log> getAllBySessionId(SessionLog session) {
        return logRepositoryImpl.findBySessionLog(session);
    }
    
    @Override
    @Transactional()
    public List<TextLog> getTextLogByType(String type){
        return logRepositoryImpl.findTextLogByType(type);
    }
}
