/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository.session;

import cs.wintoosa.AbstractTest;
import cs.wintoosa.domain.Sessionlog;
import java.util.List;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import org.springframework.beans.factory.annotation.Autowired;

/**
 *
 * @author jonimake
 */
public class SessionRepositoryImplTest extends AbstractTest{
    @Autowired
    SessionRepository sessionRepositoryImpl;
    /**
     * Test of findByPhoneId method, of class SessionRepository.
     */
    @Test
    public void testFindByPhoneId() {
        System.out.println("findByPhoneId");
        
        String phoneId = "123456asd";
        Sessionlog sessionlog = new Sessionlog();
        sessionlog.setPhoneId(phoneId);
        sessionlog.setSessionStart(2l);
        sessionlog.setSessionEnd(5l);
        sessionRepositoryImpl.saveAndFlush(sessionlog);
        
        List<Sessionlog> result = sessionRepositoryImpl.findByPhoneId(phoneId);
        assertEquals(1, result.size());
    }

    /**
     * Test of findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan method, of class SessionRepository.
     */
    @Test
    public void testFindByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan() {
        System.out.println("findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan");
        
        String phoneId = "123456";
        Sessionlog sessionlog = new Sessionlog();

        sessionlog.setPhoneId(phoneId);
        sessionlog.setSessionStart(2l);
        sessionlog.setSessionEnd(5l);
        sessionRepositoryImpl.saveAndFlush(sessionlog);
        
        Long timestamp = 3l;
        List<Sessionlog> result = sessionRepositoryImpl.findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(phoneId, timestamp, timestamp);
        assertEquals(1, result.size());
    }
}
