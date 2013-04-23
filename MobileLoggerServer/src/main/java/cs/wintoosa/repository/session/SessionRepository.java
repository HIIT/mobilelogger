package cs.wintoosa.repository.session;

import cs.wintoosa.domain.SessionLog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 * Contains method interfaces that the framework (Spring) automatically implements
 * @see <a href="http://static.springsource.org/spring-data/jpa/docs/1.3.0.RELEASE/reference/html/">Spring data JPA 1.3.0 online reference</a>
 * @author jonimake
 */
public interface SessionRepository extends JpaRepository<SessionLog, Long>, SessionRepositoryCustom {
    
    public List<SessionLog> findByPhoneId(String phoneId);
    public List<SessionLog> findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(String phoneId, Long timestamp, Long timestamp2);
    
}
