package cs.wintoosa.repository.log;

import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Sessionlog;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

/**
 * Contains method interfaces that the framework (Spring) automatically implements
 * @see <a href="http://static.springsource.org/spring-data/jpa/docs/1.3.0.RELEASE/reference/html/">Spring data JPA 1.3.0 online reference</a>
 * @author jonimake
 */
public interface LogRepository extends JpaRepository<Abstractlog, Long>, LogRepositoryCustom{
    
    /**
     * Returns all Logs and its subclasses which have the given phoneId
     * and that the timestamp of it falls between the two given timestamps.
     * @param phoneId
     * @param sessionStart
     * @param sessionEnd
     * @return a list of Abstractlog objects
     */
    public List<Abstractlog> findByPhoneIdAndTimestampBetween(String phoneId, Long sessionStart, Long sessionEnd);

    /**
     * Returns all Logs and its subclasses which have the given phoneId, null Sessionlog
     * and that the timestamp of it falls between the two given timestamps.
     * @param phoneId the phoneId
     * @param sessionStart timestamp range start
     * @param sessionEnd stimestamp range end
     * @return a list of Abstractlog objects
     */
    @Query("select l from Abstractlog l where l.phoneId = :phoneId and l.timestamp between :sessionStart and :sessionEnd and l.sessionlog is null")
    public List<Abstractlog> findByPhoneIdAndTimestampBetweenAndSessionlogIsNull(
            @Param("phoneId")String phoneId,
            @Param("sessionStart")Long sessionStart, 
            @Param("sessionEnd")Long sessionEnd);
    
    /**
     * Finds logs for Sessionlog object
     * @param sessionlog 
     * @return a list of Abstractlog objects which belong to the Sessionlog
     */
    public List<Abstractlog> findBySessionlog(Sessionlog sessionlog);
}
