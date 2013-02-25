/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.repository.IGpsLogRepository;
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
    @Mock private IGpsLogRepository logRepository;
    @InjectMocks private LogService logService;
    
    @Test
    public void testFailingSave() {
        GpsLog log = new GpsLog();
        assertFalse("Supposed to fail when phoneId not set for log", logService.saveLog(log));
    }
    
    /*@Test
    public void testSavePlainLog() {
        //sanity checks for mocking
        long logCount = logRepository.count();
        Log log = new Log();
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
        
        assertEquals(logCount, logRepository.count());
        
    }*/
    
    @Test
    public void testSaveGpsLog() {
        //sanity checks for mocking
        long logCount = logRepository.count();
        GpsLog log = new GpsLog();
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
        
        assertEquals(logCount, logRepository.count());
        
    }
    
}
