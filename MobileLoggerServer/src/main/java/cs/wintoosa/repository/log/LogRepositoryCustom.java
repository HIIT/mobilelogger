package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.domain.TextLog;
import java.util.List;
import org.springframework.transaction.annotation.Transactional;

/**
 * Add custom methods here
 * @author jonimake
 */
public interface LogRepositoryCustom {
    
    public <T extends Log> List<T> findBySessionLog(Class<T> cls, SessionLog session) throws IllegalArgumentException;
    
    public <T extends Log> List<T> findAll(Class<T> cls) throws IllegalArgumentException;
    
    public List<TextLog> findTextLogByType(String type);
}
