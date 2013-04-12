package cs.wintoosa.repository.phone;

import cs.wintoosa.domain.Phone;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
public interface PhoneRepository extends JpaRepository<Phone, String>, PhoneRepositoryCustom{
}
