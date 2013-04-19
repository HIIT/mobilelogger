package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import java.util.List;

/**
 *
 * @author jonimake
 */
public interface ILogService {
    
    /**
     * Saves the given log for the given phone
     * @param log to be saved
     * @return true if successful, false if not
     */
    
    public boolean saveLog(Log log);
    
    public List<Log> getAll();
    
    public List<Log> getAll(Class cls);
    
    public <T extends Log> List<T> getAllBySessionId(Class<T> cls, SessionLog session);
    
    public List<Log> getAllBySessionId(SessionLog session);

    public SessionLog getSessionById(long sessionId);

    public SessionLog saveSessionLog(SessionLog log);
    
    public List<SessionLog> getSessionByPhoneId(String phoneId);
    
    public List<SessionLog> getAllSessions();

    public List<Phone> getAllPhones();

}
