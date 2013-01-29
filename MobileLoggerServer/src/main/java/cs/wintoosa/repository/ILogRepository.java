package cs.wintoosa.repository;

import cs.wintoosa.domain.Log;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface ILogRepository extends JpaRepository<Log, Long>, ILogRepositoryCustom{
    
}
