package cs.wintoosa.repository;

import cs.wintoosa.domain.AccLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface IAccLogRepository extends JpaRepository<AccLog, Long>, IAccLogRepositoryCustom{
    
}
