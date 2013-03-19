/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.repository.ILogRepository;
import cs.wintoosa.repository.IPhoneRepository;
import cs.wintoosa.repository.ISessionRepository;
import org.junit.Test;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.*;
import org.mockito.runners.MockitoJUnitRunner;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 *
 * @author jonimake
 */
@RunWith(SpringJUnit4ClassRunner.class)
public class LogServiceTest extends AbstractTest{
    /*
    @Mock private ILogRepository logRepository;
    @Mock private ISessionRepository sessionRepository;
    @Mock private IPhoneRepository phoneRepository;
    @InjectMocks private LogService logService;
    */
    @Autowired
    private ILogService logService;
    
    @Autowired
    private ILogRepository logRepository;
    
    @Autowired
    private IPhoneRepository phoneRepository;
    
    @Test
    public void testFailingSave() {
        GpsLog log = new GpsLog();
        boolean saveLog = logService.saveLog(log);
        assertFalse("Supposed to fail when phoneId not set for log", saveLog );
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
        GpsLog log = new GpsLog();
        log.setTimestamp(Long.MIN_VALUE);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
        
        
    }
    
    @Test
    public void testSaveSession(){
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId(123456789012345l+"");
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        SessionLog saveLog = logService.saveSessionLog(sessionLog);
        assertTrue("log should be saved to db and returned",saveLog != null);
        assertTrue("phone should be in the log now", saveLog.getPhone() != null);
        assertTrue("phone list of sessions shouldn't be null or empty", saveLog.getPhone().getSessions() != null);
        Phone phone = phoneRepository.findByPhoneId(123456789012345l+"");
        assertEquals("phone's list of sessions was incorrect", 1, phone.getSessions().size());
    }
}
