package cs.wintoosa.repository;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface IPhoneRepository extends JpaRepository<Phone, Long>{
    public Phone findByPhoneId(String phoneId);
}
