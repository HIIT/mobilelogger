/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.service;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Gps;
import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Sessionlog;
import java.util.List;
import static org.junit.Assert.*;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

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
    
    
   // @Autowired
   // private IPhoneRepository phoneRepository;
    
    @Test
    public void testFailingSave() {
        Gps log = new Gps();
        boolean saveLog = logService.saveLog(log);
        assertFalse("Supposed to fail when phoneId not set for log", saveLog );
    }
    
    @Test
    public void testSaveLog() {
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        sessionLog = logService.saveSessionLog(sessionLog);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        assertTrue(logService.saveLog(log));
    }
    
    @Test
    public void testSaveGpsLog() {
        Gps log = new Gps();
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
        List<Abstractlog> all = logService.getAll(null);
        assertEquals(null, all);
    }
    
    @Test(expected=IllegalArgumentException.class)
    public void testFailGetAllString() {
        System.out.println("testFailGetAllString");
        logService.getAll(String.class);
    }
    
    @Test
    public void testGetAll() {
        Gps log = new Gps();
        log.setTimestamp(Long.MIN_VALUE);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(123456789012345l+"");
        assertTrue(logService.saveLog(log));
        List<Abstractlog> all = logService.getAll();
        assertEquals(1, all.size());
        
        assertEquals(log.getPhoneId(), all.get(0).getPhoneId());
    }
    
    @Test
    public void testSaveSession(){
        //Phone phone = phoneRepository.findOne(1234567890123455l+"");
        //int sizeBefore = phone.getSessions().size();
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(1234567890123455l+"");
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog = logService.saveSessionLog(sessionLog);
        assertTrue("log should be saved to db and returned",sessionLog != null);
        assertTrue("didn't have id", sessionLog.getId() != null);
        assertTrue("phone should be in the sessionlog now", sessionLog.getPhoneId() != null);
        assertTrue("phone list of sessions shouldn't be null or empty", logService.getSessionByPhoneId(sessionLog.getPhoneId()) != null);
        
        
        //phone = phoneRepository.findOne(1234567890123455l+"");
       // assertEquals("phone's list of sessions was incorrect", sizeBefore+1, phone.getSessions().size());
    }
    
    @Test
    public void testGetSessionById() {
        System.out.println("testGetSessionById");
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(12345678901234555l+"");
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog = logService.saveSessionLog(sessionLog);
        
        Sessionlog sessionById = logService.getSessionById(sessionLog.getId());
        
        assertEquals(sessionLog.getPhoneId(), sessionById.getPhoneId());
        
        
    }
    
    @Test
    public void testSaveLogsAndSessions() {
        
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        log.setSessionlog(null);
        
        logService.saveLog(log);
        assertEquals("log didn't save when it should've", 1, logService.getAll().size());
        
        
        sessionLog = logService.saveSessionLog(sessionLog);
        assertEquals("session didn't save", 1, logService.getSessionByPhoneId(phoneId).size());
        
        
        assertEquals("session didn't contain logs when it should have", 1, sessionLog.getLogs().size());
    }
    
    @Test
    public void testGetAllPhones() {
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        sessionLog = logService.saveSessionLog(sessionLog);
        
       assertEquals(phoneId,logService.getAllPhones().get(0).getId());
    }
    
    @Test
    public void testGetAllSessions() {
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        sessionLog = logService.saveSessionLog(sessionLog);
        
        assertEquals(1, logService.getAllSessions().size());
    }
    
    @Test
    public void testGetAllBySessionId() {
        
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        log.setSessionlog(null);
        
        logService.saveLog(log);
        
        
        sessionLog = logService.saveSessionLog(sessionLog);
        List<Gps> list = logService.getAllBySessionId(Gps.class, sessionLog);
        assertEquals(1, list.size());
        assertEquals(50.0f,(float)list.get(0).getLon(), 0f);
        
    }
    
    @Test
    public void getAllBySessionId(){
        String phoneId = "1234567890123455";
        
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionStart(0l);
        sessionLog.setSessionEnd(2l);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        log.setSessionlog(null);
        
        logService.saveLog(log);
        sessionLog = logService.saveSessionLog(sessionLog);
        
        List<Abstractlog> logs = logService.getAllBySessionId(sessionLog);
        
        assertEquals(1, logs.size());
    }
}
