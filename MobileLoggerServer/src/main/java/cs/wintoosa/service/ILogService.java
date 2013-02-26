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
    
    public boolean saveLog(GpsLog log);
    public boolean saveLog(AccLog log);
    public boolean saveLog(BTLog log);
    public boolean saveLog(CompLog log);
    public boolean saveLog(LightLog log);
    public boolean saveLog(NetLog log);
    public boolean saveLog(OrientationLog log);
    public boolean saveLog(ProximityLog log);
    public boolean saveLog(SoundLog log);
    public boolean saveLog(WifiLog log);
    
    public List<GpsLog> getGpsLogs();
    public List<CompLog> getCompassLogs();
    public List<AccLog> getAccelLogs();
    public List<OrientationLog> getGyroLogs();
    public List<NetLog> getNetworkLogs();

    public List<Log> getAll();
    
}
