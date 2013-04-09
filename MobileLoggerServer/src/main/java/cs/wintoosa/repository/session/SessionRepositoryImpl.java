package cs.wintoosa.repository.session;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;

/**
 *
 * @author jonimake
 */
@Repository
public class SessionRepositoryImpl implements ISessionRepositoryCustom{
    @PersistenceContext
    EntityManager em;
}
