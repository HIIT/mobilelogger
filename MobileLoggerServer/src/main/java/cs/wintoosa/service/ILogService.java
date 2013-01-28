package cs.wintoosa.service;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;

/**
 *
 * @author jonimake
 */
public interface ILogService {
    
    /**
     * Saves the given log for the given phone
     * @param log to be saved
     * @param phone for which the log belongs to, it has to have 
     * @return true if successful, false if not
     */
    public boolean saveLog(Log log, Phone phone);
    
}
