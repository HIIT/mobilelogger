package cs.wintoosa.repository;

import cs.wintoosa.domain.SessionLog;
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
