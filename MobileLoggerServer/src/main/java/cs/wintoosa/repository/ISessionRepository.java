package cs.wintoosa.repository;

import cs.wintoosa.domain.SessionLog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface ISessionRepository extends JpaRepository<SessionLog, Long>{//, ISessionRepositoryCustom {
    
    public SessionLog findSessionById(long id);
    
    public List<SessionLog> findSessionByPhoneId(String phoneId);
}
