/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.repository.ILogRepository;
import cs.wintoosa.repository.IPhoneRepository;
import org.junit.Test;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.*;
import org.mockito.runners.MockitoJUnitRunner;

/**
 *
 * @author jonimake
 */
@RunWith(MockitoJUnitRunner.class)
public class LogServiceTest extends AbstractTest{
    
    @Mock private IPhoneRepository phoneRepository;
    @Mock private ILogRepository logRepository;
    @InjectMocks private LogService logService;
    
    @Test
    public void testFailingSave() {
        Log log = new Log();
        Phone phone = new Phone();
        assertFalse("Supposed to fail when IMEI not set for phone", logService.saveLog(log, phone));
    }
    
    @Test
    public void testSave() {
        //sanity checks for mocking
        long logCount = logRepository.count();
        long phoneCount = phoneRepository.count();
        
        Phone phone = new Phone();
        phone.setImei(123456789l);
        assertTrue(logService.saveLog(new Log("looog"), phone));
        
        assertEquals(logCount, logRepository.count());
        assertEquals(phoneCount, phoneRepository.count());
        
    }
    
}
