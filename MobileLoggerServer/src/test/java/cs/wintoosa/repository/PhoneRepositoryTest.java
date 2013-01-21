/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import cs.wintoosa.domain.Phone;
import javax.swing.Spring;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 *
 * @author jonimake
 */
@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations =
{
    "classpath:spring-context-test.xml",
    "classpath:spring-database-test.xml"
})
public class PhoneRepositoryTest {
    
    @Autowired
    IPhoneRepository phoneRepository;
    
    @Test
    public void testSave() {
        long count = phoneRepository.count();
        
        phoneRepository.save(new Phone());
        
        assertEquals("Phone didn't save to DB", count+1, phoneRepository.count());
        
    }
    
    @Test
    public void testGetById() {
        long count = phoneRepository.count();
        Phone save = phoneRepository.save(new Phone());
        
        assertEquals("Phone didn't save to DB", count+1, phoneRepository.count());
        assertNotNull(save.getId());

        assertEquals("Find by id fail", save.getId(), phoneRepository.findOne(save.getId()).getId());
    }
}
