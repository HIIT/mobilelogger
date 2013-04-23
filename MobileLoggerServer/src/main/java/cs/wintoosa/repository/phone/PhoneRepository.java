package cs.wintoosa.repository.phone;

import cs.wintoosa.domain.Phone;
import org.springframework.data.jpa.repository.JpaRepository;

/**
 * Contains method interfaces that the framework (Spring) automatically implements
 * @see <a href="http://static.springsource.org/spring-data/jpa/docs/1.3.0.RELEASE/reference/html/">Spring data JPA 1.3.0 online reference</a>
 * @author jonimake
 */
public interface PhoneRepository extends JpaRepository<Phone, String>, PhoneRepositoryCustom{
}
