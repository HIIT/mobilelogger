package cs.wintoosa.repository.phone;

import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface IPhoneRepository extends JpaRepository<Phone, String>{
}
