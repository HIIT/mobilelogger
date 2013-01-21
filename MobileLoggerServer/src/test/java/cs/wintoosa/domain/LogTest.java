/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.domain;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import org.junit.runner.RunWith;
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
public class LogTest {
    
    /**
     * Test of getLines method, of class Log.
     */
    @Test
    public void testGetLines() {
        System.out.println("getLines");
        Log instance = new Log();
        String expResult = "";
        String result = instance.getLines();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setLines method, of class Log.
     */
    @Test
    public void testSetLines() {
        System.out.println("setLines");
        String lines = "";
        Log instance = new Log();
        instance.setLines(lines);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getPhoneId method, of class Log.
     */
    @Test
    public void testGetPhoneId() {
        System.out.println("getPhoneId");
        Log instance = new Log();
        Long expResult = null;
        Long result = instance.getPhoneId();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setPhoneId method, of class Log.
     */
    @Test
    public void testSetPhoneId() {
        System.out.println("setPhoneId");
        Long phoneId = null;
        Log instance = new Log();
        instance.setPhoneId(phoneId);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }
}
