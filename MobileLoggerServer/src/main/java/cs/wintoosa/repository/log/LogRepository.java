package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import java.util.Collection;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.transaction.annotation.Transactional;

/**
 *
 * @author jonimake
 */
public interface LogRepository extends JpaRepository<Log, Long>, LogRepositoryCustom{
    
    public List<Log> findByPhoneIdAndTimestampBetween(String phoneId, Long sessionStart, Long sessionEnd);

    @Query("select l from Log l where l.phoneId = :phoneId and l.timestamp between :sessionStart and :sessionEnd and l.sessionLog is null")
    public List<Log> findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(
            @Param("phoneId")String phoneId,
            @Param("sessionStart")Long sessionStart, 
            @Param("sessionEnd")Long sessionEnd);
    
    public List<Log> findBySessionLog(SessionLog session);
    
    
}
