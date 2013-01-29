package cs.wintoosa.repository;

import cs.wintoosa.domain.Phone;
import org.junit.Test;
import static org.junit.Assert.*;
import org.springframework.beans.factory.annotation.Autowired;
import cs.wintoosa.AbstractTest;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 *
 * @author jonimake
 */
public class PhoneRepositoryTest extends AbstractTest{
    
    @Autowired
    IPhoneRepository phoneRepository;
    
    @Test
    public void testSave() {
        long count = phoneRepository.count();
        Phone phone = new Phone();
        phone.setImei(1234l);
        
        phoneRepository.save(phone);
        
        assertEquals("Phone didn't save to DB", count+1, phoneRepository.count());
        
    }
    
}
