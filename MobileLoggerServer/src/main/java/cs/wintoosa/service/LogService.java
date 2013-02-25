package cs.wintoosa.service;

import cs.wintoosa.domain.*;
import cs.wintoosa.repository.*;
import java.util.LinkedList;
import java.util.List;
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
    private IGpsLogRepository gpsLogRepository;
    
    @Autowired
    private IAccLogRepository accLogRepository;
    @Autowired
    private IBTLogRepository btLogRepository;
    @Autowired
    private ICompLogRepository compLogRepository;
    @Autowired
    private ILightLogRepository lightLogRepository;
    @Autowired
    private INetLogRepository netLogRepository;
    @Autowired
    private IOrientationLogRepository orientationLogRepository;
    @Autowired
    private IProximityLogRepository proximityLogRepository;
    @Autowired
    private ISoundLogRepository soundLogRepository;
    @Autowired
    private IWifiLogRepository wifiLogRepository;
    @Autowired
    private IPhoneRepository phoneRepository;

    @Override
    @Transactional
    public boolean saveLog(GpsLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = gpsLogRepository.save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(AccLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getAccLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(BTLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getBtLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(CompLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getCompLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(LightLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getLightLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(NetLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getNetLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(OrientationLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getOrientationLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(ProximityLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getProximityLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(SoundLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getSoundLogRepository().save(log);
        return true;
    }
    
    @Override
    @Transactional
    public boolean saveLog(WifiLog log) {
        if(log == null || log.getPhoneId() == null)
            return false;
        log = getWifiLogRepository().save(log);
        return true;
    }
    
    public IGpsLogRepository getGpsLogRepository() {
        return gpsLogRepository;
    }

    public void setGpsLogRepository(IGpsLogRepository logRepository) {
        this.gpsLogRepository = logRepository;
    }

    public IPhoneRepository getPhoneRepository() {
        return phoneRepository;
    }

    public void setPhoneRepository(IPhoneRepository phoneRepository) {
        this.phoneRepository = phoneRepository;
    }

    @Override
    @Transactional(readOnly=true)
    public List<Log> getAll() {
        List<Log> logs = new LinkedList<Log>();
        logs.addAll(gpsLogRepository.findAll());
        logs.addAll(btLogRepository.findAll());
        logs.addAll(accLogRepository.findAll());
        logs.addAll(compLogRepository.findAll());
        logs.addAll(lightLogRepository.findAll());
        logs.addAll(netLogRepository.findAll());
        logs.addAll(orientationLogRepository.findAll());
        logs.addAll(proximityLogRepository.findAll());
        logs.addAll(soundLogRepository.findAll());
        logs.addAll(wifiLogRepository.findAll());
        return logs;
    }

    public IAccLogRepository getAccLogRepository() {
        return accLogRepository;
    }

    public void setAccLogRepository(IAccLogRepository accLogRepository) {
        this.accLogRepository = accLogRepository;
    }

    public IBTLogRepository getBtLogRepository() {
        return btLogRepository;
    }

    public void setBtLogRepository(IBTLogRepository btLogRepository) {
        this.btLogRepository = btLogRepository;
    }

    public ICompLogRepository getCompLogRepository() {
        return compLogRepository;
    }

    public void setCompLogRepository(ICompLogRepository compLogRepository) {
        this.compLogRepository = compLogRepository;
    }

    public ILightLogRepository getLightLogRepository() {
        return lightLogRepository;
    }

    public void setLightLogRepository(ILightLogRepository lightLogRepository) {
        this.lightLogRepository = lightLogRepository;
    }

    public INetLogRepository getNetLogRepository() {
        return netLogRepository;
    }

    public void setNetLogRepository(INetLogRepository netLogRepository) {
        this.netLogRepository = netLogRepository;
    }

    public IOrientationLogRepository getOrientationLogRepository() {
        return orientationLogRepository;
    }

    public void setOrientationLogRepository(IOrientationLogRepository orientationLogRepository) {
        this.orientationLogRepository = orientationLogRepository;
    }

    public IProximityLogRepository getProximityLogRepository() {
        return proximityLogRepository;
    }

    public void setProximityLogRepository(IProximityLogRepository proximityLogRepository) {
        this.proximityLogRepository = proximityLogRepository;
    }

    public ISoundLogRepository getSoundLogRepository() {
        return soundLogRepository;
    }

    public void setSoundLogRepository(ISoundLogRepository soundLogRepository) {
        this.soundLogRepository = soundLogRepository;
    }

    public IWifiLogRepository getWifiLogRepository() {
        return wifiLogRepository;
    }

    public void setWifiLogRepository(IWifiLogRepository wifiLogRepository) {
        this.wifiLogRepository = wifiLogRepository;
    }

    @Override
    public List<GpsLog> getGpsLogs() {
        
        return gpsLogRepository.findAll();
    }

    @Override
    public List<CompLog> getCompassLogs() {
        return compLogRepository.findAll();
    }

    @Override
    public List<AccLog> getAccelLogs() {
        return accLogRepository.findAll();
    }

    @Override
    public List<OrientationLog> getGyroLogs() {
        return orientationLogRepository.findAll();
    }

    @Override
    public List<NetLog> getNetworkLogs() {
        return netLogRepository.findAll();
    }
}
