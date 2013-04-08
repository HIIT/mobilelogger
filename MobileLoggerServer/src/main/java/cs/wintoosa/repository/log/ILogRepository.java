package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import java.util.Collection;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface ILogRepository extends JpaRepository<Log, Long>{
    public List<Log> findByPhoneIdAndTimestampBetween(String phoneId, Long sessionStart, Long sessionEnd);
    
    

    public Collection<? extends Log> findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(String phoneId, Long sessionStart, Long sessionEnd);
}
