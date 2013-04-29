package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Sessionlog;
import cs.wintoosa.domain.Text;
import java.util.List;

/**
 * Add custom methods here
 * @author jonimake
 */
public interface LogRepositoryCustom {
    
    /**
     * Returns all entries for session from table T
     * @param <T> the type of the class
     * @param cls the class, has to be subclass of Abstractlog.class
     * @param session the session of which logs to retrieve
     * @return the logs of type T of session
     * @throws IllegalArgumentException if the class<T> table doesn't exist in DB
     */
    public <T extends Abstractlog> List<T> findBySessionlog(Class<T> cls, Sessionlog session) throws IllegalArgumentException;
    
    /**
     * Returns all logs of type T
     * @param <T> the type of the class
     * @param cls the class, has to be subclass of Abstractlog.class
     * @return the logs of type T
     * @throws IllegalArgumentException 
     */
    public <T extends Abstractlog> List<T> findAll(Class<T> cls) throws IllegalArgumentException;
    
    /**
     * Returns a list of TextLogs for the given type
     * @param type type of the TextLog
     * @return a list of TextLogs
     */
    public List<Text> findTextByType(String type);
}
