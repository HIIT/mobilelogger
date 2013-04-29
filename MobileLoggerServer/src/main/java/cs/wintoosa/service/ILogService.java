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
    public boolean saveLog(Abstractlog log);
    
    /**
     * Gets all logs from the database
     * @return a list of logs
     */
    public List<Abstractlog> getAll();
    
    /**
     * Todo: Put this in a repository class
     * @param cls the class of the log entry
     * @return
     */
    public List<Abstractlog> getAll(Class cls);
    
    /**
     * Gets a list of T type logs for Sessionlog
     * @param <T> type of the log entry
     * @param cls class of the log entry
     * @param session the session for which the logs should be got
     * @return a list of type T logs for session 
     */
    public <T extends Abstractlog> List<T> getAllBySessionId(Class<T> cls, Sessionlog session);
    
    /**
     * Gets a list of all Abstractlog objects for given Sessionlog
     * @param session Sessionlog
     * @return a list of Abstractlog objects
     */
    public List<Abstractlog> getAllBySessionId(Sessionlog session);

    /**
     * Finds a Sessionlog for given id
     * @param sessionId the id of the Sessionlog
     * @return a Sessionlog
     */
    public Sessionlog getSessionById(long sessionId);

    /**
     * Saves the given session log into db. If there isn't a Phone in the DB
     * for the phoneId that the Sessionlog holds, a new one is generated and saved
     * into the database.
     * @param sessionLog
     * @return the saved Sessionlog or null if save failed
     */
    public Sessionlog saveSessionLog(Sessionlog log);

    /**
     * Returns a list of sessions by phoneId
     * @param phoneId the phoneId
     * @return a list of Sessionlog objects
     */
    public List<Sessionlog> getSessionByPhoneId(String phoneId);
    
    /**
     * @return a list of all Sessionlog in DB
     */
    public List<Sessionlog> getAllSessions();

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
    public Map<String, List<String>> getCsv(Sessionlog session);
    
    /**
     * Returns a Map suitable for csv conversion of all logs
     * @return 
     */
    public Map<String, List<String>> getCsv();
    
}
