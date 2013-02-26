package cs.wintoosa.repository;

import cs.wintoosa.domain.Phone;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 *
 * @author jonimake
 */
@Deprecated
public interface IPhoneRepository extends JpaRepository<Phone, Long>, IPhoneRepositoryCustom{
    
}
