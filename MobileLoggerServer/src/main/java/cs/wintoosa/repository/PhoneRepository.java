package cs.wintoosa.repository;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;

/**
 *
 * @author jonimake
 */
@Repository
@Deprecated
public class PhoneRepository implements IPhoneRepositoryCustom{
    
    @PersistenceContext
    EntityManager em;
}
