package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.SessionLog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

/**
 * Contains method interfaces that the framework (Spring) automatically implements
 * @see <a href="http://static.springsource.org/spring-data/jpa/docs/1.3.0.RELEASE/reference/html/">Spring data JPA 1.3.0 online reference</a>
 * @author jonimake
 */
public interface LogRepository extends JpaRepository<Log, Long>, LogRepositoryCustom{
    
    /**
     * Returns all Logs and its subclasses which have the given phoneId
     * and that the timestamp of it falls between the two given timestamps.
     * @param phoneId
     * @param sessionStart
     * @param sessionEnd
     * @return a list of Log objects
     */
    public List<Log> findByPhoneIdAndTimestampBetween(String phoneId, Long sessionStart, Long sessionEnd);

    /**
     * Returns all Logs and its subclasses which have the given phoneId, null SessionLog
     * and that the timestamp of it falls between the two given timestamps.
     * @param phoneId the phoneId
     * @param sessionStart timestamp range start
     * @param sessionEnd stimestamp range end
     * @return a list of Log objects
     */
    @Query("select l from Log l where l.phoneId = :phoneId and l.timestamp between :sessionStart and :sessionEnd and l.sessionLog is null")
    public List<Log> findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(
            @Param("phoneId")String phoneId,
            @Param("sessionStart")Long sessionStart, 
            @Param("sessionEnd")Long sessionEnd);
    
    /**
     * Finds logs for SessionLog object
     * @param session 
     * @return a list of Log objects which belong to the SessionLog
     */
    public List<Log> findBySessionLog(SessionLog session);
}
