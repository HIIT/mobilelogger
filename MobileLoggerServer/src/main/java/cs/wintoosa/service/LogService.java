package cs.wintoosa.service;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.repository.ILogRepository;
import cs.wintoosa.repository.IPhoneRepository;
import cs.wintoosa.repository.LogRepository;
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
    private ILogRepository logRepository;
    
    @Autowired
    private IPhoneRepository phoneRepository;

    @Override
    @Transactional
    public boolean saveLog(Log log, Phone phone) {
        if(log == null || phone == null || phone.getImei() == null)
            return false;
        log = logRepository.save(log);
        phone.addLog(log);
        phoneRepository.save(phone);
        return true;
    }

    public ILogRepository getLogRepository() {
        return logRepository;
    }

    public void setLogRepository(ILogRepository logRepository) {
        this.logRepository = logRepository;
    }

    public IPhoneRepository getPhoneRepository() {
        return phoneRepository;
    }

    public void setPhoneRepository(IPhoneRepository phoneRepository) {
        this.phoneRepository = phoneRepository;
    }
}
