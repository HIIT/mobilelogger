/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.log;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Gps;
import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.repository.log.LogRepository;
import cs.wintoosa.repository.session.SessionRepository;
import java.util.List;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

/**
 *
 * @author jonimake
 */
public class LogRepositoryImplTest extends AbstractTest{
    
    private String phoneId = "1234567890123455";
    
    @Autowired
    LogRepository logRepositoryImpl;
    
    @Autowired
    SessionRepository sessionRepositoryImpl;
    
    @Test
    public void findByPhoneIdAndTimestampBetweenAndSessionLogIsNull() {
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        Gps save = logRepositoryImpl.save(log);
        System.out.println("nullcheck\n\n" + save.getSessionLog() + "\n\nnullcheck");
        assertEquals(1, logRepositoryImpl.findByPhoneIdAndTimestampBetweenAndSessionLogIsNull(phoneId, 0l, 2l).size());
    }
    
    @Test
    public void findByPhoneIdAndTimestampBetween() {
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setPhoneId(phoneId);
        
        logRepositoryImpl.saveAndFlush(log);
        
        assertEquals(1, logRepositoryImpl.findAll().size());
        
        assertEquals(1, logRepositoryImpl.findByPhoneIdAndTimestampBetween(phoneId, 0l, 2l).size());
    }
    
    @Test
    public void testFindBySessionId() {
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setSessionLog(sessionLog);
        log.setPhoneId(phoneId);
        
        logRepositoryImpl.saveAndFlush(log);
        List<Gps> findBySessionLog = logRepositoryImpl.findBySessionLog(Gps.class, sessionLog);
        assertEquals(1, findBySessionLog.size());
    }
    
    @Test
    public void testFindByNullClass() {
        SessionLog sessionLog = new SessionLog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setSessionLog(sessionLog);
        log.setPhoneId(phoneId);
        
        logRepositoryImpl.saveAndFlush(log);
        List<Gps> findBySessionLog = logRepositoryImpl.findBySessionLog(null, sessionLog);
        assertNull(findBySessionLog);
    }
}
