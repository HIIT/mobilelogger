package cs.wintoosa.repository.session;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

/**
 *
 * @author jonimake
 */
public class SessionRepositoryImpl implements ISessionRepositoryCustom{
    @PersistenceContext
    EntityManager em;
}
