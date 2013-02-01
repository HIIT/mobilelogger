package cs.wintoosa.service;

import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
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
    
}
