/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.log;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Gps;
import cs.wintoosa.domain.Sessionlog;
import cs.wintoosa.repository.session.SessionRepository;
import java.util.List;
import static org.junit.Assert.*;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

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
        System.out.println("nullcheck\n\n" + save.getSessionlog() + "\n\nnullcheck");
        assertEquals(1, logRepositoryImpl.findByPhoneIdAndTimestampBetweenAndSessionlogIsNull(phoneId, 0l, 2l).size());
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
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setSessionlog(sessionLog);
        log.setPhoneId(phoneId);
        
        logRepositoryImpl.saveAndFlush(log);
        List<Gps> findBySessionLog = logRepositoryImpl.findBySessionlog(Gps.class, sessionLog);
        assertEquals(1, findBySessionLog.size());
    }
    
    @Test
    public void testFindByNullClass() {
        Sessionlog sessionLog = new Sessionlog();
        sessionLog.setPhoneId(phoneId);
        sessionLog.setSessionEnd(Long.MAX_VALUE);
        sessionLog.setSessionStart(Long.MIN_VALUE);
        sessionLog = sessionRepositoryImpl.saveAndFlush(sessionLog);
        
        Gps log = new Gps();
        log.setTimestamp(1l);
        log.setLon(50.0f);
        log.setLat(51.0f);
        log.setAlt(51.0f);
        log.setSessionlog(sessionLog);
        log.setPhoneId(phoneId);
        
        logRepositoryImpl.saveAndFlush(log);
        List<Gps> findBySessionLog = logRepositoryImpl.findBySessionlog(null, sessionLog);
        assertNull(findBySessionLog);
    }
}
