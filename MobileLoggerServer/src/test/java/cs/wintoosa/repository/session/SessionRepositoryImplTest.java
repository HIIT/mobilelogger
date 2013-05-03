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
        String phoneId = "";
        List<Sessionlog> result = sessionRepositoryImpl.findByPhoneId(phoneId);
        assertEquals(0, result.size());
    }

    /**
     * Test of findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan method, of class SessionRepository.
     */
    @Test
    public void testFindByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan() {
        System.out.println("findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan");
        String phoneId = "";
        Long timestamp = 0l;
        Long timestamp2 = 1l;
        List<Sessionlog> expResult = null;
        List<Sessionlog> result = sessionRepositoryImpl.findByPhoneIdAndSessionStartLessThanAndSessionEndGreaterThan(phoneId, timestamp, timestamp2);
        assertEquals(0, result.size());
        // TODO review the generated test code and remove the default call to fail.
    }
}
