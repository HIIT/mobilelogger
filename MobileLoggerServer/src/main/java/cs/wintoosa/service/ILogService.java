package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import java.util.List;
import java.util.Map;

/**
 * Interface for saving and getting log entities 
 * @author jonimake
 */
public interface ILogService {
    
    /**
     * Saves the given log for the given phone
     * @param log to be saved
     * @return true if successful, false if not
     */
    public boolean saveLog(Log log);
    
    /**
     * Gets all logs from the database
     * @return a list of logs
     */
    public List<Log> getAll();
    
    /**
     * Todo: Put this in a repository class
     * @param cls the class of the log entry
     * @return
     */
    public List<Log> getAll(Class cls);
    
    /**
     * Gets a list of T type logs for SessionLog
     * @param <T> type of the log entry
     * @param cls class of the log entry
     * @param session the session for which the logs should be got
     * @return a list of type T logs for session 
     */
    public <T extends Log> List<T> getAllBySessionId(Class<T> cls, SessionLog session);
    
    /**
     * Gets a list of all Log objects for given SessionLog
     * @param session SessionLog
     * @return a list of Log objects
     */
    public List<Log> getAllBySessionId(SessionLog session);

    /**
     * Finds a SessionLog for given id
     * @param sessionId the id of the SessionLog
     * @return a SessionLog
     */
    public SessionLog getSessionById(long sessionId);

    /**
     * Saves the given session log into db. If there isn't a Phone in the DB
     * for the phoneId that the SessionLog holds, a new one is generated and saved
     * into the database.
     * @param sessionLog
     * @return the saved SessionLog or null if save failed
     */
    public SessionLog saveSessionLog(SessionLog log);

    /**
     * Returns a list of sessions by phoneId
     * @param phoneId the phoneId
     * @return a list of SessionLog objects
     */
    public List<SessionLog> getSessionByPhoneId(String phoneId);
    
    /**
     * @return a list of all SessionLog in DB
     */
    public List<SessionLog> getAllSessions();

    /**
     * @return a list of all Phone objects
     */
    public List<Phone> getAllPhones();
    
    /**
     * 
     * @param type
     * @return 
     */
    public List<Text> getTextLogByType(String type);

    /**
     * Returns a Map suitable for csv conversion of the given session
     * @param session
     * @return 
     */
    public Map<String, List<String>> getCsv(SessionLog session);
    
    /**
     * Returns a Map suitable for csv conversion of all logs
     * @return 
     */
    public Map<String, List<String>> getCsv();
    
}
