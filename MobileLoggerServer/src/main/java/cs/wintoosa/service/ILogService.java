package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import java.awt.print.Pageable;
import java.lang.reflect.Type;
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

    public SessionLog getSessionById(long sessionId);

    public SessionLog saveLog(SessionLog log);
    
    public List<SessionLog> getSessionByPhoneId(String phoneId);
    
    public List<SessionLog> getAllSessions();

    public List<String> getAllPhoneIds();

}
