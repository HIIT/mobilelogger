package cs.wintoosa.repository.session;

import cs.wintoosa.domain.SessionLog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface SessionRepository extends JpaRepository<SessionLog, Long>, SessionRepositoryCustom {
    
    public List<SessionLog> findByPhoneId(String phoneId);
    public List<SessionLog> findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(String phoneId, Long timestamp, Long timestamp2);
    
}
