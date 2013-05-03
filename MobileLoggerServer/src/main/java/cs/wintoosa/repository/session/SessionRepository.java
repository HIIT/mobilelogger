package cs.wintoosa.repository.session;

import cs.wintoosa.domain.Sessionlog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

/**
 * Contains method interfaces that the framework (Spring) automatically implements
 * @see <a href="http://static.springsource.org/spring-data/jpa/docs/1.3.0.RELEASE/reference/html/">Spring data JPA 1.3.0 online reference</a>
 * @author jonimake
 */
public interface SessionRepository extends JpaRepository<Sessionlog, Long>, SessionRepositoryCustom {
    
    public List<Sessionlog> findByPhoneId(String phoneId);
    public List<Sessionlog> findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(String phoneId, Long timestamp, Long timestamp2);
    
}
