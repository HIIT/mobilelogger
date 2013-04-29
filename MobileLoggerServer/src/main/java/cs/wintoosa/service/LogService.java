package cs.wintoosa.service;

import com.google.gson.Gson;
import cs.wintoosa.domain.*;
import cs.wintoosa.repository.log.LogRepository;
import cs.wintoosa.repository.phone.PhoneRepository;
import cs.wintoosa.repository.session.SessionRepository;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;
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
    
    private static final Logger logger = Logger.getLogger(LogService.class.getName());

    @Override
    @Transactional
    public boolean saveLog(Abstractlog log) {
        if (log == null || log.getPhoneId() == null) {
            return false;
        }
        List<Sessionlog> sessions = sessionRepositoryImpl.findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(log.getPhoneId(), log.getTimestamp(), log.getTimestamp());
        assert(sessions.size() <= 1);
        for (Sessionlog session : sessions) {
            log.setSessionlog(session);
        }
        log = logRepositoryImpl.save(log);
        logger.info("Saved log:\n\t " + log.getId());
        return true;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Abstractlog> getAll(Class cls) throws IllegalArgumentException {
        return logRepositoryImpl.findAll(cls);
    }
    
    @Override
    @Transactional(readOnly = true)
    public <T extends Abstractlog> List<T> getAllBySessionId(Class<T> cls, Sessionlog session) {
        return logRepositoryImpl.findBySessionlog(cls, session);
    }
    
    @Override
    @Transactional(readOnly = true)
    public List<Abstractlog> getAll() {
        List<Abstractlog> logs = logRepositoryImpl.findAll();
        return logs;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Sessionlog> getAllSessions() {
        List<Sessionlog> sessionLogs = sessionRepositoryImpl.findAll();
        return sessionLogs;
    }

    @Override
    @Transactional(readOnly = true)
    public Sessionlog getSessionById(long sessionId) {
        Sessionlog session = sessionRepositoryImpl.findOne(sessionId);
        return session;
    }

    @Override
    @Transactional(readOnly = true)
    public List<Sessionlog> getSessionByPhoneId(String phoneId) {
        List<Sessionlog> sessions = sessionRepositoryImpl.findByPhoneId(phoneId);
        return sessions;
    }

    @Override
    @Transactional
    public Sessionlog saveSessionLog(Sessionlog sessionLog) {

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
        
        List<Abstractlog> logs = logRepositoryImpl.findByPhoneIdAndTimestampBetweenAndSessionlogIsNull(phoneId, sessionLog.getSessionStart(), sessionLog.getSessionEnd());
        for (Abstractlog log : logs) {
            log.setSessionlog(sessionLog);
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
    public List<Abstractlog> getAllBySessionId(Sessionlog session) {
        return logRepositoryImpl.findBySessionlog(session);
    }
    
    @Override
    @Transactional()
    public List<Text> getTextLogByType(String type){
        return logRepositoryImpl.findTextByType(type);
    }

    @Override
    @Transactional(readOnly=true)
    public Map<String, List<String>> getCsv(Sessionlog session) {
        LinkedHashMap<String, List<String>> map = new LinkedHashMap();
        
        
        return map;
    }

    @Override
    
    @Transactional(readOnly=true)
    public Map<String, List<String>> getCsv() {
        throw new UnsupportedOperationException("Not supported yet.");
    }
}
