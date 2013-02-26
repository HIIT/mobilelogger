package cs.wintoosa.repository;

import cs.wintoosa.domain.TextLog;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface ITextLogRepository extends JpaRepository<TextLog, Long>, ITextLogRepositoryCustom{
    
}
