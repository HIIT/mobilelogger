package cs.wintoosa.repository;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;

/**
 *
 * @author jonimake
 */
@Repository
public class TextLogRepository implements ITextLogRepositoryCustom {
    
    @PersistenceContext
    EntityManager em;
}
