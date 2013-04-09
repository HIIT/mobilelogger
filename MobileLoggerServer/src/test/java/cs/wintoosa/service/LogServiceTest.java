/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.GpsLog;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.Phone;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.repository.log.ILogRepository;
import cs.wintoosa.repository.phone.IPhoneRepository;
import java.util.List;
import static org.junit.Assert.*;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

/**
 *
 * @author jonimake
 */
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
        GpsLog log = new GpsLog();
        log.setTimestamp(Long.MIN_VALUE);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
    }
    
    @Test
    public void testFailGetAllNull() {
        System.out.println("testFailGetAllNull");
        List<Log> all = logService.getAll(null);
        assertEquals(null, all);
    }
    @Test(expected=IllegalArgumentException.class)
    public void testFailGetAllString() {
        System.out.println("testFailGetAllString");
        logService.getAll(String.class);
    }
    
    @Test
    public void testGetAll() {
        GpsLog log = new GpsLog();
        log.setTimestamp(Long.MIN_VALUE);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
        List<Log> all = logService.getAll();
        assertEquals(log.getPhoneId(), all.get(0).getPhoneId());
    }
    
    @Test
    public void testSaveSession(){
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId(1234567890123455l+"");
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog = logService.saveSessionLog(sessionLog);
        assertTrue("log should be saved to db and returned",sessionLog != null);
        assertTrue("didn't have id", sessionLog.getId() != null);
        assertTrue("phone should be in the sessionlog now", sessionLog.getPhoneId() != null);
        assertTrue("phone list of sessions shouldn't be null or empty", logService.getSessionByPhoneId(sessionLog.getPhoneId()) != null);
        
        
        Phone phone = phoneRepository.findOne(1234567890123455l+"");
        assertEquals("phone's list of sessions was incorrect", 1, phone.getSessions().size());
    }
    
    @Test
    public void testGetSessionById() {
        System.out.println("testGetSessionById");
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId(12345678901234555l+"");
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog = logService.saveSessionLog(sessionLog);
        
        SessionLog sessionById = logService.getSessionById(sessionLog.getId());
        
        assertEquals(sessionLog.getPhoneId(), sessionById.getPhoneId());
        
        
    }
}
