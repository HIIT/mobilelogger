package cs.wintoosa.repository;

import cs.wintoosa.domain.GpsLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface IGpsLogRepository extends JpaRepository<GpsLog, Long>, IGpsLogRepositoryCustom{
    
}
